#region

using ITC.Application.AppService.SystemManagers.ServerFileManagers;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;
using ITC.Domain.Core.ModelShare.PublishManagers;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyR2Project.Utils;
using NCore.Enums;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

#endregion

namespace ITC.Service.API.Controllers.SystemManagers;

/// <summary>
///     Sever-File
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class ServerFileController : ApiController
{
    #region Fields

    private readonly IServerFileAppService _serverFileAppService;
    private readonly IConfiguration _configuration;

    #endregion

    #region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="serverFileAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public ServerFileController(IServerFileAppService                    serverFileAppService,
                                INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler                         mediator,
                                IConfiguration configuration
                                ) :
        base(notifications, mediator)
    {
        _serverFileAppService = serverFileAppService;
        _configuration = configuration;
    }

    #endregion

    /// <summary>
    ///     Upload file
    /// </summary>
    /// <param name="files">Tên file và dữ liệu upload</param>
    /// <returns></returns>
    [HttpPost("upload-server-file")]
    [RequestFormLimits(MultipartBodyLengthLimit = DocHelper.SizeLimit)]
    [RequestSizeLimit(DocHelper.SizeLimit)]
    public async Task<IActionResult> UploadFile(IFormCollection files)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, "");
        }

        // Lấy danh sách các file truyền lên
        var lFile = files.Files.ToList();

        // Gán dữ liệu các thông tin đính kèm vào model trước khi gửi đi xử lý
        var model = new UploadFileEventModel
        {
            Link        = "",
            Name        = files["name"],
            Description = files["description"],
            ParentId    = files["ParentId"],
            FileType    = Convert.ToInt32(files["fileType"]),
            VideoType   = Convert.ToInt32(files["videoType"]),
            IsLocal     = true,
            FileModels  = lFile
        };

        var idFile = await _serverFileAppService.UploadFile(model);

        var info = await _serverFileAppService.ViewFile(idFile);
        if (info != null)
            return Ok(new AvatarModal
            {
                Id      = info.Id,
                IsLocal = info.IsLocal,
                Name    = info.Name,
                Link    = info.IsLocal ? info.FilePath : info.LinkUrl
            });

        return Ok(new AvatarModal());
    }
    
    /// <summary>
    ///     Upload file
    /// </summary>
    /// <param name="files">Tên file và dữ liệu upload</param>
    /// <returns></returns>
    [HttpPost("upload-file-old")]
    [RequestFormLimits(MultipartBodyLengthLimit = DocHelper.SizeLimit)]
    [RequestSizeLimit(DocHelper.SizeLimit)]
    public async Task<IActionResult> UploadServerFile(IFormCollection files)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, "");
        }

        // Lấy danh sách các file truyền lên
        var lFile = files.Files.ToList();

        // Gán dữ liệu các thông tin đính kèm vào model trước khi gửi đi xử lý
        var model = new UploadFileEventModel
        {
            Link        = "",
            Name        = files["name"],
            Description = files["description"],
            ParentId    = files["ParentId"],
            FileType    = Convert.ToInt32(files["fileType"]),
            VideoType   = Convert.ToInt32(files["videoType"]),
            IsLocal     = true,
            FileModels  = lFile
        };

        var idFile = await _serverFileAppService.UploadServerFile(model);
        return Ok(new UploadFileDto()
        {
            Link = idFile
        });
    }

    [HttpPost("upload-file")] // Đảm bảo route đúng
    [RequestFormLimits(MultipartBodyLengthLimit = DocHelper.SizeLimit)]
    [RequestSizeLimit(DocHelper.SizeLimit)]
    public async Task<IActionResult> UploadServerFileNews(IFormCollection files)
    {
        if (!ModelState.IsValid)
        {
            // NotifyModelStateErrors(); // Hàm này của bên bạn
            return BadRequest(new { status = false, message = "Invalid Data" });
        }

        // 1. Kiểm tra tham số isR2 từ Client gửi lên
        bool isR2 = true;
        if (files.ContainsKey("isR2"))
        {
            bool.TryParse(files["isR2"], out isR2);
        }

        // 2. Lấy danh sách file
        var lFile = files.Files.ToList();
        if (lFile.Count == 0) return BadRequest("Không có file nào được gửi lên.");

        // --- LOGIC MỚI: NẾU LÀ R2 ---
        if (isR2)
        {
            // Lấy cấu hình R2 từ appsettings (hoặc inject IConfiguration vào Controller để lấy)
            // Giả sử bạn đã Inject _configuration vào Constructor
            var r2Config = _configuration.GetSection("CloudflareR2").Get<R2Config>();

            var uploadedLinks = new List<string>();

            foreach (var file in lFile)
            {
                if (file.Length > 0)
                {
                    using var stream = file.OpenReadStream();
                    // Upload lên R2 vào thư mục "uploads" (hoặc thư mục tùy chọn)
                    
                    var r2Link = await R2Uploader.UploadFromFormFile(stream, file.FileName, file.ContentType, r2Config);

                    if (!string.IsNullOrEmpty(r2Link))
                    {
                        uploadedLinks.Add(r2Link);
                    }
                }
            }

            // Trả về link file đầu tiên (hoặc list tùy logic bên bạn)
            // Giữ nguyên format trả về là UploadFileDto để Client không bị lỗi
            return Ok(new UploadFileDto()
            {
                Link = uploadedLinks.FirstOrDefault() ?? ""
            });
        }

        // --- LOGIC CŨ: NẾU KHÔNG PHẢI R2 (Giữ nguyên) ---
        var model = new UploadFileEventModel
        {
            Link = "",
            Name = files["name"],
            Description = files["description"],
            ParentId = files["ParentId"],
            FileType = Convert.ToInt32(files["fileType"]),
            VideoType = Convert.ToInt32(files["videoType"]),
            IsLocal = true,
            FileModels = lFile
        };

        var idFile = await _serverFileAppService.UploadServerFile(model);
        return Ok(new UploadFileDto()
        {
            Link = idFile
        });
    }




    /// <summary>
    ///     Upload file đính kèm
    /// </summary>
    /// <param name="files">Tên file và dữ liệu upload</param>
    /// <returns></returns>
    [HttpPost("upload-file-attack")]
    [RequestFormLimits(MultipartBodyLengthLimit = DocHelper.SizeLimit)]
    [RequestSizeLimit(DocHelper.SizeLimit)]
    public async Task<IActionResult> UploadFileAttack(IFormCollection files)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, "");
        }

        // Lấy danh sách các file truyền lên
        var lFile = files.Files.ToList();

        // Gán dữ liệu các thông tin đính kèm vào model trước khi gửi đi xử lý
        var model = new UploadFileEventModel
        {
            Link        = "",
            Name        = "",
            Description = "",
            ParentId    = "",
            FileType    = FileTypeEnumeration.File.Id,
            VideoType   = 0,
            IsLocal     = true,
            FileModels  = lFile
        };

        var result = await _serverFileAppService.UploadFileAttack(model);
        return NResponseCommand(result.ResultCommand, result);
    }

    /// <summary>
    ///     Upload file đính kèm nhiều người dùng
    /// </summary>
    /// <param name="files">Tên file và dữ liệu upload</param>
    /// <returns></returns>
    [HttpPost("upload-file-attack-peoples")]
    [RequestFormLimits(MultipartBodyLengthLimit = DocHelper.SizeLimit)]
    [RequestSizeLimit(DocHelper.SizeLimit)]
    public async Task<IActionResult> UploadFileAttackPeoples(IFormCollection files)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, "");
        }

        // Lấy danh sách các file truyền lên
        var lFile = files.Files.ToList();

        // Gán dữ liệu các thông tin đính kèm vào model trước khi gửi đi xử lý
        int.TryParse(files["fileType"], out var iFileType);
        int.TryParse(files["fileGroup"], out var iGroupFile);
        int.TryParse(files["isSaveSubmitDocument"], out var iSaveSubmitDocument);
        int.TryParse(files["isSaveWithManagement"], out var iSaveWithManagement);

        Guid.TryParse(files["managementId"], out _);
        var model = new UploadFileEventModel
        {
            Link                 = "",
            Name                 = "",
            Description          = "",
            ParentId             = "",
            FileType             = iFileType == 1 ? FileTypeEnumeration.Image.Id : FileTypeEnumeration.File.Id,
            VideoType            = 0,
            IsLocal              = true,
            FileModels           = lFile,
            GroupFile            = iGroupFile,
            IsSaveSubmitDocument = iSaveSubmitDocument,
            IsSaveWithManagement = iSaveWithManagement
        };

        var result = await _serverFileAppService.UploadFileAttack(model);
        return NResponseCommand(result.ResultCommand, result);
    }

    /// <summary>
    ///     Danh sách server-file
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-paging")]
    public async Task<JsonResponse<IEnumerable<ServerFilePagingDto>>> GetPaging(string search, Guid workManagerId)
    {
        return await Task.Run(() => new
                                  OkResponse<IEnumerable<ServerFilePagingDto>>("",
                                                                               _serverFileAppService
                                                                                   .GetPaging(search, workManagerId)
                                                                                   .Result));
    }

    /// <summary>
    ///     Danh sách server-file theo ID
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-paging-by-id")]
    public async Task<JsonResponse<IEnumerable<ServerFilePagingDto>>> GetPagingById(string attackId)
    {
        return await Task.Run(() => new
                                  OkResponse<IEnumerable<ServerFilePagingDto>>("",
                                                                               _serverFileAppService
                                                                                   .GetPagingById(attackId).Result));
    }

    /// <summary>
    ///     Xóa file dữ liệu
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(await _serverFileAppService.Delete(model), model);
    }

    /// <summary>
    ///     Cập nhật đệ quy dữ liệu
    /// </summary>
    /// <returns></returns>
    [HttpGet("call-de-quy")]
#pragma warning disable CS1998
    public async Task<IActionResult> CallDeQuy()
#pragma warning restore CS1998
    {
        return NResponseCommand(await _serverFileAppService.CallDeQuy(), true);
    }

    /// <summary>
    ///     Cập nhật fileName
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update-file-name")]
    public IActionResult Edit([FromBody] UpdateFileNameModal model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_serverFileAppService.UpdateFileName(model), model);
    }

    /// <summary>
    ///     [Treeview] Trả về tree-view
    /// </summary>
    /// <returns></returns>
    // [CustomAuthorize(ModuleIdentity.MenuManager, TypeAudit.View)]
    [HttpGet("get-treeview")]
    [CustomAuthorize(ModuleIdentity.ServerFileManager, TypeAudit.View)]
    public async Task<JsonResponse<IEnumerable<TreeViewProjectModel>>> GetTreeViewRoot(string userId)
    {
        return new OkResponse<IEnumerable<TreeViewProjectModel>>(
            "", await _serverFileAppService
                .GetTreeView(new TreeViewPagingModelLibrary
                {
                    ModuleIdentity =
                        ModuleIdentity.ServerFileManager,
                    VSearch = "",
                    UserId  = userId
                }));
    }

    #region =============================================FOLDER=========================================================

    /// <summary>
    ///     Thêm mới Folder
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("folder-create")]
    [CustomAuthorize(ModuleIdentity.ServerFileManager, TypeAudit.Add)]
    public IActionResult Add([FromBody] FolderServerFileEvent model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_serverFileAppService.FolderAdd(model), model);
    }

    /// <summary>
    ///     Sửa Folder
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("folder-update")]
    [CustomAuthorize(ModuleIdentity.ServerFileManager, TypeAudit.Edit)]
    public IActionResult Edit([FromBody] FolderServerFileEvent model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_serverFileAppService.FolderUpdate(model), model);
    }

    /// <summary>
    ///     Lấy dữ liệu thư mục theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-folder-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.NewsGroupType, TypeAudit.View)]
    public IActionResult GetByFolderId(Guid id)
    {
        return NResponseCommand(null, _serverFileAppService.GetByFolderId(id));
    }


    /// <summary>
    ///     Cập nhật mã cha - con
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("parent-update")]
    [CustomAuthorize(ModuleIdentity.ServerFileManager, TypeAudit.Edit)]
    public IActionResult ParentUpdate([FromBody] FolderServerFileEvent model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(_serverFileAppService.ParentUpdate(model), model);
    }

    #endregion

    #region =============================================DETAIL=========================================================

    /// <summary>
    ///     Dữ liệu chi tiết
    /// </summary>
    /// <returns></returns>
    [HttpGet("detail-paging")]
    [CustomAuthorize(ModuleIdentity.ServerFileManager, TypeAudit.Add)]
    public async Task<JsonResponse<Pagination<ServerFileDetailPagingDto>>> GetServerFileDetailPagingDto(
        string vSearch, int pageSize, int pageNumber, string authorId, Guid serverFileId, int groupContentTypeId)
    {
        return await Task.Run(() =>
        {
            var lData = _serverFileAppService.GetServerFileDetailPagingDto(vSearch,
                                                                           pageSize,
                                                                           pageNumber,
                                                                           authorId,
                                                                           serverFileId,
                                                                           groupContentTypeId).Result.ToList();
            return new OkResponse<Pagination<ServerFileDetailPagingDto>>("",
                                                                         new Pagination<ServerFileDetailPagingDto>
                                                                         {
                                                                             PageLists = lData,
                                                                             TotalRecord =
                                                                                 lData.Count > 0
                                                                                     ? lData[0].TotalRecord
                                                                                     : 0
                                                                         });
        });
    }

    /// <summary>
    ///     Lấy dữ liệu ServerFile theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-detail-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.ServerFileManager, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _serverFileAppService.ViewFile(id).Result);
    }

    /// <summary>
    ///     Thêm mới dữ liệu từ đường dẫn khác
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("upload-difference")]
    [CustomAuthorize(ModuleIdentity.ServerFileManager, TypeAudit.Add)]
    public async Task<IActionResult> UploadDifference([FromBody] UploadDifferenceEventModal model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(await _serverFileAppService.UploadDifference(model), model);
    }

    /// <summary>
    ///     Xem file
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("view-file/{id:guid}")]
    public async Task<IActionResult> ViewFile(Guid id)
    {
        var info = _serverFileAppService.ViewFile(id).Result;
        if (info.IsLocal)
        {
            //===========================================File local=================================================
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", info.FilePath);
            if (System.IO.File.Exists(fullPath))
            {
                var b = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(b, "application/octet-stream", info.Name);
            }
            //======================================================================================================
        }
        else
        {
            //===========================================File difference============================================
            return Ok(info.LinkUrl);
            //======================================================================================================
        }

        return NResponseBadRequest(false);
    }

    /// <summary>
    ///     Xem file
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("view-file-link/{id:guid}")]
    public async Task<IActionResult> ViewFileLink(Guid id)
    {
        var info = await _serverFileAppService.ViewFile(id);
        return Ok(new AvatarModal
        {
            Id      = info.Id,
            IsLocal = info.IsLocal,
            Name    = info.Name,
            Link    = info.IsLocal ? info.FilePath : info.LinkUrl
        });
        // info.IsLocal ?
        //            //===========================================File local=================================================
        //            // var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", info.FilePath);
        //            // if (System.IO.File.Exists(fullPath))
        //            // {
        //            //     var b = await System.IO.File.ReadAllBytesAsync(fullPath);
        //            //     return File(b, "application/octet-stream", info.Name);
        //            // }
        //            Ok("local;" +info.FilePath) :
        //            //======================================================================================================
        //            //===========================================File difference============================================
        //            Ok(info.LinkUrl);

        //======================================================================================================
        // return NResponseBadRequest(false);
    }

    /// <summary>
    ///     Cập nhật tên file
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPut("update-file-name-file")]
    public async Task<IActionResult> UpdateFileName([FromBody] UpdateFileNameModal model)
    {
        if (!ModelState.IsValid)
        {
            NotifyModelStateErrors();
            return NResponseCommand(false, model);
        }

        return NResponseCommand(await _serverFileAppService.UpdateNameFile(model), model);
    }

    /// <summary>
    ///     Trả về danh sách dữ liệu resize image
    /// </summary>
    /// <param name="model">danh sách dữ liệu nhận từ FE</param>
    /// <returns></returns>
    [HttpPost("resize-image-type")]
    public async Task<JsonResponse<IEnumerable<ResizeImageDto>>> ResizeImageType([FromBody] ResizeImageModal model)
    {
        return new OkResponse<IEnumerable<ResizeImageDto>>("", await _serverFileAppService.ResizeImageType(model));
    }

    #endregion
}