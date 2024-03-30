#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryDetailManager;
using ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryManager;
using ITC.Domain.Interfaces.SaleProductManagers.ImageLibraryManager;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.SaleProductManagerss.ImageLibraryManagers;

/// <summary>
///     queries slide
/// </summary>
public class ImageLibraryManagerQueries : IImageLibraryManagerQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public ImageLibraryManagerQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<ImageLibraryManagerPagingDto>> GetPaging(PagingModel model)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(@"SELECT RM.Id,
                                                                   RM.Name,
                                                                   RM.Content,
                                                                   RM.StatusId,
                                                                   RM.Position,
                                                                   RM.CreatedBy AS OwnerId,
                                                                   SF.FilePath,
                                                                   SF.IsLocal
                                                            FROM ImageLibraryManagers RM
                                                                     LEFT JOIN ServerFiles SF ON SF.Id = RM.AvatarId
                                                            WHERE RM.IsDeleted = 0 AND RM.ProjectId = @pProjectId "));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, model.Search));
        sBuilder.Append(model.StatusId > 0 ? "AND StatusId = @pStatusId " : " ");
        sBuilder.Append("ORDER BY Position ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pStatusId", model.StatusId
            },
            {
                "@pProjectId", model.ProjectId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<ImageLibraryManagerPagingDto>(_connectionString,
                   sBuilder,
                   new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetPagingDetail" />
    public async Task<IEnumerable<ImageLibraryDetailManagerPagingDto>> GetPagingDetail(
        ImageLibraryDetailPagingModel model)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(@"
                SELECT ILDM.Id,
                       ILDM.Name,
                       ILDM.Content,
                       ILDM.Created,
                       SF.FilePath,
                       SF.LinkUrl,
                       SF.IsLocal
                FROM ImageLibraryDetailManagers ILDM
                         INNER JOIN ImageLibraryManagers ILM ON ILM.Id = ILDM.ImageLibraryManagerId AND ILM.IsDeleted = 0
                         LEFT JOIN ServerFiles SF ON SF.Id = ILDM.AvatarId
                WHERE ILDM.IsDeleted = 0 AND ILM.ProjectId = @pProjectId AND ILM.Id = @imageId "));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, model.Search));
        sBuilder.Append("ORDER BY Created ");
        sBuilder.Append(SqlHelper.Paging(model.PageNumber, model.PageSize));
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pProjectId", model.ProjectId
            },
            {
                "@imageId", model.ImageId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<ImageLibraryDetailManagerPagingDto>(
                   _connectionString, sBuilder, new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model, int flag)
    {
        var tableName = flag == 1 ? "ImageLibraryManagers" : "ImageLibraryDetailManagers";
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync(tableName, model));
    }
}