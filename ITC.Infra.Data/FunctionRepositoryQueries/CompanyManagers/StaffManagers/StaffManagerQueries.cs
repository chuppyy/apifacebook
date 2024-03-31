#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;
using ITC.Domain.Core.NCoreLocal.Enum;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using ITC.Domain.ResponseDto;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.CompanyManagers.StaffManagers;

/// <summary>
///     Nhân viên queries
/// </summary>
public class StaffManagerQueries : IStaffManagerQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public StaffManagerQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<StaffManagerPagingDto>> GetPaging(StaffManagerPagingViewModel model,
                                                                    int                         groupTable,
                                                                    Guid                        managementId)
    {
        var sBuilderTemp = new StringBuilder();
        if (groupTable == GroupTableEnum.UserManager.Id)
            sBuilderTemp
                .Append(@"SELECT SM.Id, SM.Name, SM.Description, SM.Phone, SM.Address, SM.Email, ANU.UserName, 
                                    SM.StatusId, SM.CreatedBy AS OwnerId, SM.Created AS OwnerCreated,
                                    A.Name AS AuthorityManagerName ");
        sBuilderTemp.Append("FROM StaffManagers SM ");
        sBuilderTemp.Append("LEFT JOIN Authorities A ON A.Id = SM.AuthorityId ");
        // if (groupTable == GroupTableEnum.UserManager.Id)
        // {
        //     sBuilderTemp
        //         .Append(
        //                 $"INNER JOIN SubjectTypeManagers STM ON SM.UserTypeManagerId = STM.Id AND STM.TypeId = {GroupTableEnum.UserTypeManager.Id} ");
        //     
        // }

        sBuilderTemp.Append("INNER JOIN AspNetUsers ANU ON ANU.Id = SM.UserId ");
        sBuilderTemp.Append("WHERE SM.IsDeleted = 0 ");
        // sBuilderTemp.Append("WHERE SM.IsDeleted = 0  AND SM.ProjectId = @pProjectId ");

        if (groupTable == GroupTableEnum.UserManager.Id)
            sBuilderTemp.Append(string.Compare(model.UserTypeManagerId.ToString(), Guid.Empty.ToString(),
                                               StringComparison.Ordinal) != 0
                                    ? "AND SM.UserTypeManagerId = @pUserTypeManagerId "
                                    : "");

        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(sBuilderTemp.ToString()));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name",
            "Phone",
            "Address",
            "Email"
        }, model.Search));
        sBuilder.Append("ORDER BY OwnerCreated DESC ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        // Xử lý các biến cần truyền
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pUserTypeManagerId", model.UserTypeManagerId
            },
            {
                "@pProjectId", model.ProjectId
            },
            {
                "@pManagementId", managementId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<StaffManagerPagingDto>(_connectionString,
                                                                          sBuilder,
                                                                          new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetPagingUserTracing" />
    public async Task<IEnumerable<UserTracingPagingDto>> GetPagingUserTracing(PagingModel model)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(@"
                    SELECT SM.Id, SM.Name, SM.TimeConnectEnd, SM.TimeConnectStart, SM.ConnectionId, SM.IsOnline, ANU.UserName
                    FROM StaffManagers SM
                             INNER JOIN AspNetUsers ANU ON SM.UserId = ANU.Id
                    WHERE SM.IsDeleted = 0 "));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name",
            "UserName"
        }, model.Search));
        sBuilder.Append("ORDER BY TimeConnectStart DESC ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        return await SqlHelper.RunDapperQueryAsync<UserTracingPagingDto>(_connectionString,
                                                                         sBuilder);
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper
                                                         .DeleteListAsync("StaffManagers", model));
    }

    /// <inheritdoc cref="GetCombobox" />
    public async Task<IEnumerable<ComboboxModal>> GetCombobox(string vSearch)
    {
        var vSql = "SELECT SM.Id, SM.Name FROM StaffManagers SM WHERE SM.IsDeleted = 0 AND SM.IsStaff = 1";
        return await SqlHelper
                   .RunDapperQueryAsync<ComboboxModal>(_connectionString,
                                                       SqlHelper.GetComboboxAsync("StaffManagers",
                                                           vSearch,
                                                           vSql,
                                                           true));
    }

    /// <inheritdoc cref="GetComboboxAuthor" />
    public async Task<IEnumerable<ComboboxAuthorModal>> GetComboboxAuthor(
        string vSearch, int pageSize, int pageNumber)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper
                            .GeneralSqlBuilder(
                                "SELECT SM.Id, SM.Name, SM.UserId FROM StaffManagers SM WHERE SM.IsDeleted = 0 "));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, vSearch));
        sBuilder.Append("ORDER BY Id ");
        sBuilder.Append(SqlHelper.Paging(pageNumber, pageSize));
        return await SqlHelper.RunDapperQueryAsync<ComboboxAuthorModal>(_connectionString,
                                                                        sBuilder);
    }

    /// <inheritdoc cref="GetListCheckedProcessingStreamStaff" />
    public async Task<IEnumerable<StaffManagerCheckSelectViewModel>> GetListCheckedProcessingStreamStaff(
        string vSearch, Guid processingStreamId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"WITH dTemp AS ( ");
        if (string.Compare(processingStreamId.ToString(), Guid.Empty.ToString(), StringComparison.Ordinal) != 0)
        {
            sBuilder.Append(@"SELECT N'accepted' AS Checked, SM.Id, SM.Name
                    FROM ProcessingStreamStaffs PSS
                        INNER JOIN StaffManagers SM ON PSS.StaffManagerId = SM.Id AND SM.IsDeleted = 0
                    WHERE PSS.IsDeleted = 0 AND PSS.ProcessingStreamId = @pProcessingStreamId ");
            sBuilder.Append(@"UNION ");
        }

        sBuilder.Append(@"SELECT N'not_accepted' AS Checked, SM.Id, SM.Name 
                    FROM StaffManagers SM 
                    WHERE SM.IsDeleted = 0 AND SM.IsStaff = 1 ");
        if (string.Compare(processingStreamId.ToString(), Guid.Empty.ToString(), StringComparison.Ordinal) != 0)
            sBuilder.Append("AND SM.Id NOT IN "                                             +
                            "(SELECT PSS2.StaffManagerId FROM ProcessingStreamStaffs PSS2 " +
                            "WHERE PSS2.IsDeleted = 0 AND PSS2.ProcessingStreamId = @pProcessingStreamId) ");

        sBuilder.Append(") ");
        sBuilder.Append(@"SELECT * FROM dTemp WHERE Id != @pGuiEmpty ");
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, vSearch));
        // Xử lý các biến cần truyền
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pProcessingStreamId", processingStreamId
            },
            {
                "@pGuiEmpty", Guid.Empty
            }
        };
        return await SqlHelper.RunDapperQueryAsync<StaffManagerCheckSelectViewModel>(_connectionString,
                   sBuilder,
                   new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetListStaffByProcessingStreamId" />
    public async Task<IEnumerable<ComboboxModal>> GetListStaffByProcessingStreamId(
        string vSearch, Guid processingStreamId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"
                SELECT SM.Id, SM.Name
                FROM ProcessingStreamStaffs PSS
                    INNER JOIN StaffManagers SM ON PSS.StaffManagerId = SM.Id AND SM.IsDeleted = 0
                WHERE PSS.IsDeleted = 0 AND  PSS.ProcessingStreamId = @pProcessingStreamId ");
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, vSearch));
        // Xử lý các biến cần truyền
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pProcessingStreamId", processingStreamId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<ComboboxModal>(_connectionString,
                                                                  sBuilder,
                                                                  new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetStaffInSystem" />
    public async Task<IEnumerable<ComboboxModal>> GetStaffInSystem()
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append("SELECT SM.Id, SM.Name, SM.UserId FROM StaffManagers SM WHERE SM.IsDeleted = 0 ");
        return await SqlHelper.RunDapperQueryAsync<ComboboxModal>(_connectionString, sBuilder);
    }

    /// <inheritdoc cref="GetByUserId2" />
    public async Task<StaffManagerByUserDto> GetByUserId2(string userId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@$"SELECT SM.Id, SM.Name FROM StaffManagers SM WHERE SM.UserId = '{userId}' ");
        return await SqlHelper
                   .RunDapperQueryFirstOrDefaultAsync<StaffManagerByUserDto>(_connectionString, sBuilder);
    }

    /// <inheritdoc cref="GetSportStaffRegisterModel" />
    public async Task<IEnumerable<SportStaffRegisterModel>> GetSportStaffRegisterModel(Guid staffId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT SSM.Id, SR.LevelId, SR.Position, 
                                        SSM.Name AS SubjectDetailName, 
                                        STM.Name AS SubjectName, 
                                        SSM.SexId, 
                                        SSM.Name,
                                        SR.SubjectDetailId,
                                        SR.SubjectId
                                FROM SportRegisters SR
                                         INNER JOIN SportSubjectManagers SSM ON SSM.Id = SR.SubjectDetailId
                                         INNER JOIN SubjectTypeManagers STM ON STM.Id = SR.SubjectId
                                WHERE SR.StaffManagerId = @pProcessingStreamId AND SR.IsDeleted = 0 ");
        // Xử lý các biến cần truyền
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pProcessingStreamId", staffId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<SportStaffRegisterModel>(_connectionString,
                                                                            sBuilder,
                                                                            new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetStaffAttackModel" />
    public async Task<IEnumerable<StaffAttackModelPaging>> GetStaffAttackModel(Guid staffId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT SF.Id, SF.IsLocal, SF.FileType, SF.GroupFile, SF.FileName
                                FROM ServerFiles SF
                                INNER JOIN StaffAttackManagers SAM ON SAM.FileId = SF.Id
                                WHERE SF.IsDeleted = 0 AND SAM.StaffManagerId = @pProcessingStreamId 
                                ORDER BY SF.GroupFile ");
        // Xử lý các biến cần truyền
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pProcessingStreamId", staffId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<StaffAttackModelPaging>(_connectionString,
                                                                           sBuilder,
                                                                           new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="CountNumberOfRegister" />
    public async Task<List<KeyValuePair<Guid, int>>> CountNumberOfRegister(
        List<Guid> sportSubjectId, Guid managementId, Guid projectId)
    {
        var listOfJoin = new NCoreHelper().convert_list_to_string(sportSubjectId);
        var sBuilder   = new StringBuilder();
        sBuilder.Append(@"SELECT SR.SubjectDetailId AS [Key], COUNT(SR.Id) AS [Value]
                                FROM StaffManagers SM
                                INNER JOIN SportRegisters SR ON SR.StaffManagerId = SM.Id
                                WHERE SM.IsDeleted = 0 AND SM.ManagementId = @pManagementId AND SM.ProjectId = @pProjectId AND SR.IsDeleted = 0
                                AND SR.SubjectDetailId IN " + listOfJoin + " ");
        sBuilder.Append(@"GROUP BY SR.SubjectDetailId ");
        // Xử lý các biến cần truyền
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pManagementId", managementId
            },
            {
                "@pProjectId", projectId
            }
        };
        return (List<KeyValuePair<Guid, int>>)await SqlHelper.RunDapperQueryAsync<KeyValuePair<Guid, int>>(
                                                  _connectionString,
                                                  sBuilder,
                                                  new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetSportAttackModel" />
    public async Task<IEnumerable<Guid>> GetSportAttackModel(Guid staffId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(
            @"SELECT SM.FileId FROM StaffAttackManagers SM WHERE SM.IsDeleted = 0 AND SM.StaffManagerId = @pStaffId ");
        // Xử lý các biến cần truyền
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pStaffId", staffId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<Guid>(_connectionString,
                                                         sBuilder,
                                                         new DynamicParameters(dictionary));
    }

    public async Task<IEnumerable<UserModelDto>> GetUserCodeAsync()
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT [Name], [UserCode] FROM [StaffManagers] where IsDeleted=0");
        return await SqlHelper.RunDapperQueryAsync<UserModelDto>(_connectionString,
            sBuilder,
            new DynamicParameters());
    }

    public async Task<IEnumerable<ComboboxIdNameDto>> GetComboboxWebAsync()
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT [Id], [Name] FROM Webs");
        return await SqlHelper.RunDapperQueryAsync<ComboboxIdNameDto>(_connectionString,
            sBuilder);
    }

    public async Task<IEnumerable<WebsiteDto>> GetListInfoWebAsync(List<int> domainIds)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT [Id], [Name], [IdAnalytic] FROM Webs w");
        if (domainIds != null && domainIds.Any())
        {
            sBuilder.Append($" WHERE w.[Id] in ({domainIds}) ");
        }
        return await SqlHelper.RunDapperQueryAsync<WebsiteDto>(_connectionString, sBuilder);
    }
}