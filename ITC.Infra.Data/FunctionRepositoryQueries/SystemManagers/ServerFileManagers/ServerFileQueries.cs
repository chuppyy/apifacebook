#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using ITC.Domain.Interfaces.SystemManagers.ServerFiles;
using NCore.Helpers;
using NCore.Modals;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.ServerFileManagers;

/// <summary>
///     ServerFile queries
/// </summary>
public class ServerFileQueries : IServerFileQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public ServerFileQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<ServerFilePagingDto>> GetPaging(string search, Guid workManagerId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"SELECT SF.Id,
                                    SF.FileName,
                                    SF.IsLocal,
                                    SF.FilePath,
                                    SF.FileExtension,
                                    SF.FileType,
                                    SF.VideoType,
                                    SF.FileNameRoot
                                        FROM ServerFiles SF
                                    WHERE SF.IsDeleted = 0 AND SF.ManagementId = @managementId ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@managementId", workManagerId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<ServerFilePagingDto>(_connectionString,
                                                                        sBuilder,
                                                                        new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetPagingById" />
    public async Task<IEnumerable<ServerFilePagingDto>> GetPagingById(List<Guid> models)
    {
        var listOfJoin = new NCoreHelper().convert_list_to_string(models);
        var sBuilder   = new StringBuilder();
        sBuilder.Append(@$"SELECT SF.Id,
                                    SF.FileName,
                                    SF.IsLocal,
                                    SF.FilePath,
                                    SF.FileExtension,
                                    SF.FileType,
                                    SF.VideoType,
                                    SF.GroupFile,
                                    SF.FileNameRoot
                              FROM ServerFiles SF
                              WHERE SF.IsDeleted = 0 AND SF.Id IN {listOfJoin} ");
        return await SqlHelper.RunDapperQueryAsync<ServerFilePagingDto>(_connectionString,
                                                                        sBuilder);
    }

    /// <inheritdoc cref="DeleteAsync" />
    public async Task<int> DeleteAsync(List<Guid> model)
    {
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     SqlHelper.DeleteListAsync("ServerFiles",
                                                                               model));
    }

    /// <inheritdoc cref="GetTreeView" />
    public async Task<IEnumerable<TreeViewProjectModel>> GetTreeView(string vSearch, string userId)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(@"
                SELECT NG.Id, NG.FileName AS Text, NG.ParentId, NG.Position, NG.StatusId, NG.IsRoot  
                FROM ServerFiles NG
                WHERE NG.IsDeleted = 0 AND( NG.CreatedBy = @pUserId OR NG.IsRoot = 1) AND NG.IsFolder = 1 "));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, vSearch));
        sBuilder.Append("ORDER BY Position ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pUserId", userId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<TreeViewProjectModel>(_connectionString,
                                                                         sBuilder,
                                                                         new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="GetServerFileDetailPagingDto" />
    public async Task<IEnumerable<ServerFileDetailPagingDto>> GetServerFileDetailPagingDto(
        string vSearch, int pageSize, int pageNumber, int pLeft, int pRight, string authorId,
        int    groupContentTypeId)
    {
        var vSql = new StringBuilder();
        // vSql.Append(@"SELECT SF.Id,
        //                         SF.FileName,
        //                         ANU.FullName AS Author,
        //                         SF.FileSize,
        //                         SF.FileType,
        //                         SF.FilePath,
        //                         SF.LinkUrl,
        //                         SF.VideoType,
        //                         SF.IsFolder,
        //                         SF.IsLocal,
        //                         SF.StatusId,
        //                         SF.Modified
        //                         FROM ServerFiles SF
        //                             INNER JOIN AspNetUsers ANU ON SF.CreatedBy = ANU.Id
        //                         WHERE SF.IsDeleted = 0 AND SF.PLeft > @pLeft AND SF.PRight < @pRight ");
        vSql.Append(@"SELECT SF.Id,
                                SF.FilePath,
                                SF.LinkUrl,
                                SF.IsLocal,
                                SF.Modified
                                FROM ServerFiles SF
                                WHERE SF.IsDeleted = 0 ");
        if (!string.IsNullOrEmpty(authorId)) vSql.Append("AND SF.CreatedBy = @authorId ");
        if (groupContentTypeId > 0) vSql.Append("AND SF.FileType = @pFileType ");
        var sBuilder = new StringBuilder();
        sBuilder.Append(SqlHelper.GeneralSqlBuilder(vSql.ToString()));
        sBuilder.Append(SqlHelper.Search(new List<string>
        {
            "Name"
        }, vSearch));
        sBuilder.Append("ORDER BY Modified DESC ");
        sBuilder.Append(SqlHelper.Paging(pageNumber, pageSize));
        var dictionary = new Dictionary<string, object>
        {
            {
                "@pLeft", pLeft
            },
            {
                "@pRight", pRight
            },
            {
                "@authorId", authorId
            },
            {
                "@pFileType", groupContentTypeId
            }
        };
        return await SqlHelper.RunDapperQueryAsync<ServerFileDetailPagingDto>(_connectionString,
                                                                              sBuilder,
                                                                              new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="AddFile" />
    public async Task<int> AddFile(ServerFileSendSave model)
    {
        var sBuilder = new StringBuilder();
        sBuilder.Append(@"INSERT INTO ServerFiles (Id, Created, CreatedBy, IsDeleted, Modified, ModifiedBy, 
                                                        StatusId, FileExtension, FileName,FileNameRoot, FilePath, 
                                                        FileSize, FileType, VideoType, Folder, Description, IsFolder, 
                                                        IsLocal, LinkUrl, ParentId, PLeft, PRight, CloudId, CloudFolder, 
                                                        ManagementId, AvatarLink, Position, IsRoot, GroupFile)
            VALUES (@GuiId, @DateTime, @UserId, @False, @DateTime, @UserId, @StatusId, @FileExtension, @FileName, 
                    @FileNameRoot, @FilePath, @FileSize, @FileType, 3, @Folder, N'', @False, @True, N'', @ParentId, 
                    1, 2, N'', N'', @ManagementId, N'', @Position, @False, @GroupFile); ");
        var dictionary = new Dictionary<string, object>
        {
            {
                "@GuiId", model.Id
            },
            {
                "@DateTime", DateTime.Now
            },
            {
                "@False", false
            },
            {
                "@True", true
            },
            {
                "@UserId", model.UserId
            },
            {
                "@StatusId", model.StatusId
            },
            {
                "@FileExtension", model.FileExtension
            },
            {
                "@FileName", model.FileName
            },
            {
                "@FileNameRoot", model.FileNameRoot
            },
            {
                "@FilePath", model.FilePath
            },
            {
                "@FileSize", model.FileSize
            },
            {
                "@FileType", model.FileType
            },
            {
                "@Folder", model.Folder
            },
            {
                "@ParentId", model.ParentId
            },
            {
                "@ManagementId", model.ManagementId
            },
            {
                "@Position", model.Position
            },
            {
                "@GroupFile", model.GroupFile
            }
        };
        return await SqlHelper.RunDapperExecuteAsync(_connectionString,
                                                     sBuilder,
                                                     new DynamicParameters(dictionary));
    }

    /// <inheritdoc cref="ResizeImageType" />
    public async Task<IEnumerable<ResizeImageDto>> ResizeImageType(ResizeImageModal model)
    {
        var listOfIdsJoined = new NCoreHelper().convert_list_to_string(model.ListModels);
        var sBuilder        = new StringBuilder();
        sBuilder.Append(@"
                SELECT NG.Id, NG.FileName AS Name, NG.FileType, NG.IsLocal, NG.FilePath 
                FROM ServerFiles NG
                WHERE NG.IsDeleted = 0 AND NG.Id IN " + listOfIdsJoined + " ");
        sBuilder.Append("ORDER BY Position ");
        return await SqlHelper.RunDapperQueryAsync<ResizeImageDto>(_connectionString, sBuilder);
    }
}