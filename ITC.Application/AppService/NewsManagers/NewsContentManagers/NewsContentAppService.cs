#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using AutoMapper;
using Hangfire;
using HtmlAgilityPack;
using ITC.Application.AppService.NewsManagers.NewsGroupManagers;
using ITC.Application.AppService.NewsManagers.NewsViaManagers;
using ITC.Application.Services.Vercel;
using ITC.Domain.Commands.NewsManagers.NewsContentManagers;
using ITC.Domain.Commands.SystemManagers.ServerFileManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.HomeManager;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using ITC.Domain.Core.NCoreLocal;
using ITC.Domain.Core.NCoreLocal.Enum;
using ITC.Domain.Interfaces.AuthorityManager.AuthorityManagerSystem;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsConfigManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsContentManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.CrossCutting.Identity.Migrations;
using Microsoft.Extensions.Logging;
using NCore.Actions;
using NCore.Enums;
using NCore.Helpers;
using NCore.Modals;
using Org.BouncyCastle.Utilities;
using IUser = ITC.Domain.Interfaces.IUser;

#endregion

namespace ITC.Application.AppService.NewsManagers.NewsContentManagers;

/// <summary>
///     Class service bài viết
/// </summary>
public class NewsContentAppService : INewsContentAppService
{
    #region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="queries"></param>
    /// <param name="repository"></param>
    /// <param name="authorityManagerQueries"></param>
    /// <param name="newsGroupAppService"></param>
    /// <param name="staffManagerRepository"></param>
    /// <param name="newsViaAppService"></param>
    /// <param name="postFaceService"></param>
    /// <param name="newsConfigRepository"></param>
    /// <param name="logger"></param>
    /// <param name="bus"></param>
    /// <param name="user"></param>
    /// <param name="newsDomainRepository"></param>
    /// <param name="newsGroupRepository"></param>
    /// <param name="newsContentRepository"></param>
    public NewsContentAppService(IMapper mapper,
                                     INewsContentQueries queries,
                                     INewsContentRepository repository,
                                     IAuthorityManagerQueries authorityManagerQueries,
                                     INewsGroupAppService newsGroupAppService,
                                     IStaffManagerRepository staffManagerRepository,
                                     INewsViaAppService newsViaAppService,
                                     IPostFaceService postFaceService,
                                     INewsConfigRepository newsConfigRepository,
                                     ILogger<NewsContentAppService> logger,
                                     IMediatorHandler bus,
                                     IUser user,
                                     INewsDomainRepository newsDomainRepository,
                                     INewsGroupRepository newsGroupRepository,
                                     INewsContentRepository newsContentRepository)
    {
        _mapper = mapper;
        _queries = queries;
        _repository = repository;
        _authorityManagerQueries = authorityManagerQueries;
        _newsGroupAppService = newsGroupAppService;
        _staffManagerRepository = staffManagerRepository;
        _newsViaAppService = newsViaAppService;
        _postFaceService = postFaceService;
        _newsConfigRepository = newsConfigRepository;
        _logger = logger;
        _bus = bus;
        _user = user;
        _newsDomainRepository = newsDomainRepository;
        _newsGroupRepository = newsGroupRepository;
        _newsContentRepository = newsContentRepository;
    }

    #endregion

    #region Fields

    private readonly IMediatorHandler _bus;
    private readonly IUser _user;
    private readonly IMapper _mapper;
    private readonly INewsContentQueries _queries;
    private readonly INewsContentRepository _repository;
    private readonly IAuthorityManagerQueries _authorityManagerQueries;
    private readonly INewsGroupAppService _newsGroupAppService;
    private readonly IStaffManagerRepository _staffManagerRepository;
    private readonly INewsViaAppService _newsViaAppService;
    private readonly IPostFaceService _postFaceService;
    private readonly INewsConfigRepository _newsConfigRepository;
    private readonly INewsDomainRepository _newsDomainRepository;
    private readonly ILogger<NewsContentAppService> _logger;
    private readonly INewsGroupRepository _newsGroupRepository;
    private readonly INewsContentRepository _newsContentRepository;

    #endregion

    #region INewsContentAppService Members

    /// <summary>
    ///     Thêm bài viết
    /// </summary>
    /// <param name="model"></param>
    public async Task<bool> Add(NewsContentEventModel model)
    {
        model.UrlRootLink = model.AvatarLink;
        var addCommand = _mapper.Map<AddNewsContentCommand>(model);
        addCommand.UrlRootLink = model.AvatarLink;
        await _bus.SendCommand(addCommand);
        model.Id = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        model.SecretKey = addCommand.SecretKey;
        return model.ResultCommand;
    }

    /// <summary>
    ///     Xóa bài viết
    /// </summary>
    /// <param name="model"></param>
    public bool Delete(DeleteModal model)
    {
        var deleteCommand = new DeleteNewsContentCommand(model.Models);
        _bus.SendCommand(deleteCommand);
        model.ResultCommand = deleteCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <summary>
    ///     Lấy theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<NewsContentGetByIdModel> GetById(Guid id)
    {
        var iValue = await _repository.GetAsync(id);
        if (iValue == null) return null;

        //=========================================Lấy dữ liệu từ ServerFile====================================
        // var iServerFileInfo = _serverFileAppService.ViewFile(iValue.AvatarId).Result;
        //======================================================================================================

        return new NewsContentGetByIdModel
        {
            Id = iValue.Id,
            Name = iValue.Name,
            Author = iValue.Author,
            Content = iValue.Content,
            Summary = iValue.Summary,
            AvatarId = iValue.AvatarId,
            AvatarLink = iValue.AvatarLink,
            SeoKeyword = iValue.SeoKeyword,
            StatusId = iValue.StatusId,
            DateTimeStart = iValue.DateTimeStart.ToString("dd - MM - yyyy HH:mm"),
            NewsGroupId = iValue.NewsGroupId,
            UrlRootLink = iValue.UrlRootLink,
            AgreeVia = iValue.AgreeVia,
            LinkTree = iValue.LinkTree,
            Link = iValue.AvatarLink,
            IsLocal = iValue.AvatarLocal // iServerFileInfo?.IsLocal ?? false
        };
    }

    /// <inheritdoc cref="Update" />
    public async Task<bool> Update(NewsContentEventModel model)
    {
        model.UrlRootLink = model.AvatarLink;
        var updateCommand = _mapper.Map<UpdateNewsContentCommand>(model);
        updateCommand.UrlRootLink = model.AvatarLink;
        await _bus.SendCommand(updateCommand);
        model.ResultCommand = updateCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <inheritdoc cref="GetPaging" />
    public async Task<IEnumerable<NewsContentPagingDto>> GetPaging(NewsContentPagingModel model)
    {
        return await Task.Run(() =>
        {
            // Lấy dữ liệu quyền của người dùng
            var permissionValue = _authorityManagerQueries.GetPermissionByMenuManagerValue(model.ModuleIdentity, _user.UserId).Result;
            // Xử lý dữ liệu
            //1. Lấy danh sách các nhóm bài viết liên quan đến các nhóm bài viết lựa chọn
            var lSendNewsGroupId = !string.IsNullOrEmpty(model.NewsGroupId)
                                       ? new NCoreHelper().ConvertJsonSerializer<Guid>(model.NewsGroupId)
                                       : new List<Guid>();
            var lNewsGroup = lSendNewsGroupId.Count > 0
                                 ? _newsGroupAppService.GetListIdFromListId(lSendNewsGroupId).Result
                                 : new List<Guid>();

            var staffId = Guid.Parse(_user.StaffId);
            var listByOwnerId = new List<string>();
            //2. Kiểm tra quyền xem dữ liệu tác giả khác
            if ((permissionValue & PermissionEnum.XemTacGiaKhac.Id) != 0)
            {
                //Nếu có lọc theo tác giả
                if (model.Author.CompareTo(Guid.Empty) != 0)
                {
                    var staffInfo = _staffManagerRepository.GetAsync(model.Author).Result;
                    if (staffInfo != null) model.Author = Guid.Parse(staffInfo.UserId);
                }
            }
            else
            {
                //var staffNowInfo = _staffManagerRepository.GetAsync(staffId).Result;
                model.Author = Guid.Parse(_user.UserId);
                listByOwnerId = _staffManagerRepository.GetByOwnerIdAsync(_user.UserId)?.Result;
            }

            var lData = (List<NewsContentPagingDto>)_queries.GetPaging(model, lNewsGroup, listByOwnerId).Result;

            var viewAuthor = (permissionValue & PermissionEnum.XemTacGiaKhac.Id) != 0;

            var dateCheck = new DateTime(2023, 12, 13, 22, 30, 00);
            foreach (var item in lData)
            {
                if (item.DateTimeStart > dateCheck && !string.IsNullOrEmpty(item.AvatarLink))
                {
                    // Lấy tên tệp tin và phần mở rộng
                    var fileExtension = Path.GetExtension(item.AvatarLink);
                    if (item.AvatarLink != null && item.AvatarLink.Contains("Uploads/Img/"))
                    {
                        var newFileName = item.AvatarLink.Replace($"{fileExtension}", $"_300x300{fileExtension}");
                        item.AvatarLink = newFileName;
                    }
                }
                item.Actions =
                    ActionPagingWorkManager.ActionAuthoritiesNews(permissionValue,
                        item.OwnerId,
                        viewAuthor ? item.OwnerId : _user.UserId,
                        true,
                        item.StatusId);
            }


            return lData;
        });
    }

    /// <inheritdoc cref="GetPagingAuto" />
    public async Task<IEnumerable<NewsContentPagingDto>> GetPagingAuto(NewsContentPagingModel model)
    {
        return await Task.Run(() =>
        {
            // Lấy dữ liệu quyền của người dùng
            var iPermission =
                _authorityManagerQueries
                    .GetPermissionByMenuManagerValue(model.ModuleIdentity, _user.UserId)
                    .Result;
            // Xử lý dữ liệu
            //1. Lấy danh sách các nhóm bài viết liên quan đến các nhóm bài viết lựa chọn
            var lSendNewsGroupId = !string.IsNullOrEmpty(model.NewsGroupId)
                                       ? new NCoreHelper().ConvertJsonSerializer<Guid>(model.NewsGroupId)
                                       : new List<Guid>();
            var lNewsGroup = lSendNewsGroupId.Count > 0
                                 ? _newsGroupAppService.GetListIdFromListId(lSendNewsGroupId).Result
                                 : new List<Guid>();
            var staffId = Guid.Parse(_user.StaffId);
            var staffNowInfo = _staffManagerRepository.GetAsync(staffId).Result;
            //2. Xuất dữ liệu NewsContent theo các điều kiện đã đưa vào
            if ((iPermission & PermissionEnum.XemTacGiaKhac.Id) != 0)
            {
                if (model.Author.CompareTo(Guid.Empty) != 0)
                {
                    var staffInfo = _staffManagerRepository.GetAsync(model.Author).Result;
                    if (staffInfo != null) model.Author = Guid.Parse(staffInfo.UserId);
                }
            }
            else
            {
                model.Author = Guid.Parse(staffNowInfo.UserId);
            }

            model.StatusId = ActionStatusEnum.Active.Id;
            var lData = (List<NewsContentPagingDto>)_queries.GetPagingAuto(model, lNewsGroup).Result;
            // Trả về Actions

            // var authoritiesValue = _authorityManagerQueries
            //                        .PermissionValueByMenuId(staffNowInfo.AuthorityId,
            //                                                 new NCoreHelperV2023().ViewAuthor)
            //                        .Result;
            // var viewAuthor = (authoritiesValue & PermissionEnum.XemTacGiaKhac.Id) != 0;
            //
            // foreach (var item in lData)
            //     item.Actions =
            //         ActionPagingWorkManager.ActionAuthoritiesNews(iPermission,
            //                                                       item.OwnerId,
            //                                                       viewAuthor ? item.OwnerId : _user.UserId,
            //                                                       true,
            //                                                       item.StatusId);
            return lData;
        });
    }

    /// <inheritdoc cref="NewsContentTypeCombobox" />
    public async Task<IEnumerable<ComboboxModalInt>> NewsContentTypeCombobox()
    {
        return await Task.Run(() =>
        {
            var lData = NewsContentTypeEnumeration.GetList().ToList();
            var lReturn = lData.Select(items => new ComboboxModalInt { Id = items.Id, Name = items.Name }).ToList();
            return lReturn;
        });
    }

    /// <inheritdoc cref="NewsAuthor" />
    public async Task<IEnumerable<ComboboxModal>> NewsAuthor()
    {
        return await Task.Run(() =>
        {
            var listReturn = new List<ComboboxModal>();
            var staffId = Guid.Parse(_user.StaffId);
            var staffInfo = _staffManagerRepository.GetAsync(staffId).Result;
            var authoritiesValue = _authorityManagerQueries
                                   .PermissionValueByMenuId(staffInfo.AuthorityId,
                                                            new NCoreHelperV2023().ViewAuthor)
                                   .Result;
            if ((authoritiesValue & PermissionEnum.XemTacGiaKhac.Id) != 0)
            {
                listReturn.Add(new ComboboxModal
                {
                    Id = Guid.Empty,
                    Name = "Tất cả"
                });
                var listAuthor = _queries.NewsAuthor().Result;
                listReturn.AddRange(listAuthor.Select(items => new ComboboxModal
                { Id = Guid.Parse(items.Id), Name = items.Name }));
            }
            else
            {
                listReturn.Add(new ComboboxModal
                {
                    Id = staffId,
                    Name = _user.StaffName
                });
            }

            return listReturn;
        });
    }

    /// <param name="model"></param>
    /// <inheritdoc cref="NewsContentCombobox" />
    public async Task<IEnumerable<NewsContentPagingComboboxDto>> NewsContentCombobox(NewsContentPagingModel model)
    {
        return await Task.Run(() =>
        {
            var lSendNewsGroupId = !string.IsNullOrEmpty(model.NewsGroupId)
                                       ? new NCoreHelper().ConvertJsonSerializer<Guid>(model.NewsGroupId)
                                       : new List<Guid>();
            var lNewsGroup = lSendNewsGroupId.Count > 0
                                 ? _newsGroupAppService.GetListIdFromListId(lSendNewsGroupId).Result
                                 : new List<Guid>();
            //2. Xuất dữ liệu NewsContent theo các điều kiện đã đưa vào
            var lData = _queries.GetPagingCombobox(model, lNewsGroup).Result.ToList();
            foreach (var items in lData) items.Id = NormalizeNumbers(items.Id);

            return lData;
        });
    }

    /// <inheritdoc cref="GetBySecrect" />
    public async Task<NewsContent> GetBySecrect(Guid projectId, string secrectKey)
    {
        return await _repository.GetBySecretKey(secrectKey);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ComboboxModal>> CopyLink(Guid id)
    {
        var dataInfo = await _queries.GetPagingById(id);
        string slug = ToSlug(dataInfo.Name, 20);
        return new List<ComboboxModal>
        {
            new()
            {
                Id = Guid.Empty,
                Name = CheckDomain(dataInfo.Domain) + "" +slug + "-" + dataInfo.MetaName + "-" + dataInfo.UserCode + "-" +
                       dataInfo.MetaKey
            }
        };
    }

    public string ToSlug(string text, int maxWords)
    {
        // Chuyển thành chữ thường
        text = text.ToLower();

        // Loại bỏ ký tự đặc biệt, chỉ giữ chữ và số + khoảng trắng
        text = Regex.Replace(text, @"[^a-z0-9\s]", "");

        // Tách thành từ
        var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Lấy tối đa maxWords từ
        if (words.Length > maxWords)
            words = words[..maxWords];

        // Ghép bằng dấu -
        return string.Join("-", words);
    }

    public string CheckDomain(string value)
    {
        var check = value.Substring(value.Length - 1, 1);
        if (string.Compare(check, "/", StringComparison.Ordinal) == 0) return value;
        return value + "/";
    }

    /// <inheritdoc cref="ReadLink"/>
    public async Task<ReadLink> ReadLink(string url)
    {
        return await Task.Run(async () =>
        {
            try
            {
                url = url.Replace(",", "");
                // Tạo đối tượng HttpClient để tải nội dung từ URL
                using var client = new HttpClient();

                var htmlContent = await client.GetStringAsync(url);

                // Sử dụng HTML Agility Pack để phân tích mã HTML
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                // Lấy tiêu đề của bài viết
                var title = htmlDoc.DocumentNode.SelectSingleNode("//title").InnerText;

                // Lấy URL hình ảnh (nếu có)
                var imageUrl = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:image']")
                                      ?.GetAttributeValue("content", "");

                // Loại bỏ các thẻ quảng cáo: script, ins, iframe
                var adTags = new[] { "script", "ins", "iframe", "h1", "span", "header" };

                foreach (var adTag in adTags)
                {
                    var adElements = htmlDoc.DocumentNode.SelectNodes($"//{adTag}");
                    if (adElements != null)
                    {
                        if (adTag == "iframe")
                        {
                            foreach (var adElement in adElements)
                            {
                                string id = adElement.GetAttributeValue("id", "");
                                if (!id.Contains("twitter"))
                                {
                                    // If the ID does not contain "twitter", remove the iframe
                                    adElement.Remove();
                                }
                                // adElement.Remove();
                            }
                        }
                        else
                        {
                            foreach (var adElement in adElements)
                            {
                                adElement.Remove();
                            }
                        }

                    }
                }

                //Loại bỏ thuộc tính class và style từ tất cả các phần tử
                var allElements = htmlDoc.DocumentNode.Descendants();
                foreach (var element in allElements)
                {
                    element.Attributes.Remove("class");
                    element.Attributes.Remove("style");
                    element.Attributes.Remove("id");
                    //element.Attributes.Remove("src"); 
                }

                var imgElements = htmlDoc.DocumentNode.SelectNodes("//img");

                if (imgElements != null)
                {
                    foreach (var img in imgElements)
                    {
                        // Kiểm tra nếu thẻ <img> có cả thuộc tính src và data-src
                        var srcAttribute = img.GetAttributeValue("src", "");
                        var dataSrcAttribute = img.GetAttributeValue("data-src", "");

                        if (!string.IsNullOrWhiteSpace(srcAttribute) &&
                            !string.IsNullOrWhiteSpace(dataSrcAttribute))
                        {
                            // Lấy giá trị của data-src và gán cho src
                            img.SetAttributeValue("src", dataSrcAttribute);
                        }
                    }
                }

                // Lấy nội dung trong thẻ <article>
                var article = htmlDoc.DocumentNode.SelectSingleNode("//article").InnerHtml;

                // Lưu nội dung hình ảnh
                var imageId = Guid.Empty;
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    var sendCommand = new UploadServerFileCommand(new UploadFileEventModel
                    {
                        Link = imageUrl,
                        IsLocal = false,
                        Name = Convert.ToDateTime(DateTime.Now).ToString("ddMMyyyyHHss"),
                        FileType = FileTypeEnumeration.Image.Id
                    });
                    await _bus.SendCommand(sendCommand);
                    imageId = sendCommand.Id;

                }

                return new ReadLink
                {
                    IsTrue = true,
                    Title = title,
                    ImageId = imageId,
                    ImageLink = imageUrl,
                    Body = article
                };
            }
            catch (Exception ex)
            {
                return new ReadLink
                {
                    IsTrue = false,
                    Title = "",
                    ImageId = Guid.Empty,
                    ImageLink = "",
                    Body = ex.Message
                };
            }
        });
    }

    /// <inheritdoc cref="PostNew"/>
    public async Task<PostNewFaceError> PostNew(PostNewFaceEvent model)
    {
        return await Task.Run(async () =>
        {
            var newsConfig = _newsConfigRepository.GetAllAsync().Result.FirstOrDefault();
            var dataReturn = new PostNewFaceError();
            var newInfo = _repository.GetAsync(model.Id).Result;
            if (newInfo == null)
            {
                dataReturn.Result = false;
                dataReturn.Value = "Bài viết không tồn tại";
                goto ketthuc;
            }

            var newGroupInfo = await _newsGroupRepository.GetByIdAsync(newInfo.NewsGroupId);
            if (newGroupInfo == null)
            {
                dataReturn.Result = false;
                dataReturn.Value = "Nhóm tin không tồn tại";
                goto ketthuc;
            }

            var newViaInfo = _newsViaAppService.GetById(Guid.Parse(newGroupInfo.IdViaQc));
            if (newViaInfo == null)
            {
                dataReturn.Result = false;
                dataReturn.Value = "Via không tồn tại";
                goto ketthuc;
            }

            var tkqcId = newViaInfo.IdTkQc; // "254760556290913";
            if (string.IsNullOrEmpty(tkqcId))
            {
                dataReturn.Result = false;
                dataReturn.Value = "Không có IdTkQc";
                goto ketthuc;
            }

            var token = newViaInfo.Token;
            if (string.IsNullOrEmpty(token))
            {
                dataReturn.Result = false;
                dataReturn.Value = "Không có token";
                goto ketthuc;
            }

            //"EAAMFx9dc1NMBO19gNJGfaiOnEdBqQ0WZBrBZBZAM4idW38Xe6bsvJ72tWqYArJgTNcZAQXoEZBf5pSDZCdV7ZCXZCKHmbpOmyLavjVuwom2diRqrTiTXmKzHPeIqCLjt2AKVZAoc6lbB471VhZCWUauNUel9HsfZBn8FAvrzm1Qq4MsR2ZBb4aAimw7P63RKDQxZCzR72Xn8m8x7m";
            //PageId
            var pageId = newGroupInfo.LinkTree; // "112183871936936";
            if (string.IsNullOrEmpty(pageId))
            {
                dataReturn.Result = false;
                dataReturn.Value = "Không có pageId";
                goto ketthuc;
            }

            var pictureUrl =
                CheckUrl(newInfo.AvatarLocal ? newsConfig?.Host + "" + newInfo.AvatarLink : newInfo.AvatarLink);
            if (string.IsNullOrEmpty(pictureUrl))
            {
                dataReturn.Result = false;
                dataReturn.Value = "Không có ảnh đại diện";
                goto ketthuc;
            }

            //"https://lifenews247.com/Images/files/collage%20161.png";
            var linkUrl = newInfo.LinkTree;
            if (model.IsPostImg == true)
            {
                /*var staffInfo =  _staffManagerRepository.GetByUserId(newInfo.CreatedBy).Result;
                if (staffInfo != null)
                {
                }*/

                //linkUrl = CheckDomain(newGroupInfo.Domain) + "" + newGroupInfo.MetaTitle + "-" + code + "-" +
                //       newInfo.SecretKey;
            }

            if (string.IsNullOrEmpty(linkUrl))
            {
                dataReturn.Result = false;
                dataReturn.Value = "Không có linkUrl";
                goto ketthuc;
            }

            //"loveanimalnewsh.vercel.app/doc-bao/how-a-tiny-injured-puppy-helped-me-when-i-most-required-it-1286";
            var title = newInfo.Name; //"tieu đề";
            if (string.IsNullOrEmpty(title))
            {
                dataReturn.Result = false;
                dataReturn.Value = "Không có tiêu đề";
                goto ketthuc;
            }



            var result = await _postFaceService.Quangcao(newInfo.Id, tkqcId, token, pageId, pictureUrl, linkUrl, title, model.IsPostImg);

            dataReturn.Result = result > 0;

            dataReturn.Value = result > 0 ? "Thành công" : "Thất bại";

            // Nếu bằng số lần đã đăng thì cập nhật lại Domain
            if (newGroupInfo.Amount > 0)
            {
                // Tăng dần số bài đã đăng
                if (newGroupInfo.AmountPosted != null)
                {
                    newGroupInfo.AmountPosted += 1;
                }
                else
                {
                    newGroupInfo.AmountPosted = 1;
                }

                if (newGroupInfo.Amount == newGroupInfo.AmountPosted)
                {
                    //Delete domain cũ
                    await _newsDomainRepository.DeleteDomainByName(newGroupInfo.Name);

                    // Thêm mới domain mới
                    var domain = await _newsDomainRepository.GetDomainFirtOrDefaultAsync(newGroupInfo.Name);
                    newGroupInfo.Domain = domain?.Name;
                }

                _newsGroupRepository.Update(newGroupInfo);
                _newsGroupRepository.SaveChanges();
            }

            // if (result > 0)
            // {
            //     // Cập nhật trạng thái đã đăng cho bài viết
            //     newInfo.StatusId = ActionStatusEnum.Active.Id;
            //     _repository.Update(newInfo);
            //     _repository.SaveChanges();
            // }

            ketthuc:;
            return dataReturn;
        });
    }

    private string CheckUrl(string value)
    {
        var https = "https://";
        var http = "http://";
        if (string.IsNullOrEmpty(value)) return "";
        // https://api.vbonews.com/Uploads/FileFolder/fol-21102023/fol-qsumigtqhl/638334851784605006.jpeg
        // Thay đổi dấu https://api.vbonews.com/Uploads\FileFolder\fol-21102023\fol-qsumigtqhl\638334851784605006.jpeg
        value = value.Replace("\\", "/");
        if (value.Substring(0, https.Length) == https) return value;
        if (value.Substring(0, http.Length) == http) return value;
        return https + "" + value;
    }


    #region ======================================================MAIN==================================================

    private string NormalizeNumbers(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;

        var normalized = text;

        var allNumbers = text.Where(char.IsNumber).Distinct().ToArray();

        foreach (var ch in allNumbers)
        {
            var equalNumber = char.Parse(char.GetNumericValue(ch).ToString("N0"));
            normalized = normalized.Replace(ch, equalNumber);
        }

        return normalized;
    }

    /// <inheritdoc cref="UpdateTimeAutoPost"/>
    public async Task<bool> UpdateTimeAutoPost(NewsContentUpdateTimeAutoPostModel model)
    {
        var addCommand = _mapper.Map<UpdateTimeAutoPostNewsContentCommand>(model);
        await _bus.SendCommand(addCommand);
        model.Id = addCommand.Id;
        model.ResultCommand = addCommand.ResultCommand;
        return model.ResultCommand;
    }

    /// <summary>
    /// Chạy lịch
    /// </summary>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task SchedulerStart()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var newsConfig = _newsConfigRepository.GetAllAsync().Result.FirstOrDefault();

        var host = newsConfig?.Host ?? "";

        var listContent = _queries.AutoPostFaceList().Result;

        //Cập nhật luôn.
        List<Task<int>> taskSave = new List<Task<int>>();
        _logger.LogInformation("=====Bắt chạy xử lý lập lịch===========:");

        //Cập nhật trạng thái đã đăng
        foreach (var items in listContent)
        {
            var tkqcId = items.TkqcId;
            if (string.IsNullOrEmpty(tkqcId))
            {
                goto ketthuc;
            }

            var token = items.Token;
            if (string.IsNullOrEmpty(token))
            {
                goto ketthuc;
            }

            //"EAAMFx9dc1NMBO19gNJGfaiOnEdBqQ0WZBrBZBZAM4idW38Xe6bsvJ72tWqYArJgTNcZAQXoEZBf5pSDZCdV7ZCXZCKHmbpOmyLavjVuwom2diRqrTiTXmKzHPeIqCLjt2AKVZAoc6lbB471VhZCWUauNUel9HsfZBn8FAvrzm1Qq4MsR2ZBb4aAimw7P63RKDQxZCzR72Xn8m8x7m";
            //PageId
            var pageId = items.PageId; // "112183871936936";
            if (string.IsNullOrEmpty(pageId))
            {
                goto ketthuc;
            }

            var pictureUrl =
                CheckUrl(items.AvatarLocal ? host + "" + items.AvatarLink : items.AvatarLink);
            if (string.IsNullOrEmpty(pictureUrl))
            {
                goto ketthuc;
            }

            var linkUrl = items.LinkUrl;
            if (string.IsNullOrEmpty(linkUrl))
            {
                goto ketthuc;
            }

            //"loveanimalnewsh.vercel.app/doc-bao/how-a-tiny-injured-puppy-helped-me-when-i-most-required-it-1286";
            var title = items.Name; //"tieu đề";
            if (string.IsNullOrEmpty(title))
            {
                goto ketthuc;
            }

            if (!string.IsNullOrEmpty(tkqcId) &&
                !string.IsNullOrEmpty(token) &&
                !string.IsNullOrEmpty(pageId) &&
                !string.IsNullOrEmpty(pictureUrl) &&
                !string.IsNullOrEmpty(linkUrl) &&
                !string.IsNullOrEmpty(title))
            {
                _logger.LogInformation("=====ADD QC===========:" + items.Id);
                _logger.LogInformation("=====ADD QC===========:" + title);
                //await _postFaceService.Quangcao(items.Id, tkqcId, token, pageId, pictureUrl, linkUrl, title);
                bool isImg = false;
                if (items.TypeId == 3)
                    isImg = true;
                taskSave.Add(_postFaceService.Quangcao(items.Id, tkqcId, token, pageId, pictureUrl, linkUrl, title, isImg));
                _logger.LogInformation("=====ADD QC: XONG===========:" + items.Id);
                // RunPostFace(items.Id, tkqcId, token, pageId, pictureUrl, linkUrl, title).GetAwaiter().GetResult();
                // _ = RunPostFace(items.Id, tkqcId, token, pageId, pictureUrl, linkUrl, title);
                // _logger.LogInformation("=====Đã chạy qua hàm xử lý===========:" + items.Id);
            }

            ketthuc:;
        }

        if (taskSave.Count > 0)
        {
            // _logger.LogInformation("=====RUN QC===========:" + JsonConvert.SerializeObject(taskSave));
            await Task.WhenAll(taskSave);
            _logger.LogInformation("=====RUN QC: XONG===========:");
        }
    }

    // private async Task RunPostFace(Guid   newsContentId, string tkqcId, string token, string pageId, string pictureUrl,
    //                                string linkUrl,
    //                                string title)
    // {
    //     // Thread.Sleep(300000);
    //     // return;
    //     await _postFaceService.Quangcao(newsContentId, tkqcId, token, pageId, pictureUrl, linkUrl, title);
    //
    //     // if (result > 0)
    //     // {
    //     //     // Cập nhật trạng thái đã đăng cho bài viết
    //     //     var sBuilder = new StringBuilder();
    //     //     sBuilder.Append(
    //     //         $@"UPDATE NewsContents SET StatusId = {ActionStatusEnum.Active.Id} WHERE Id = '{newsContentId}';");
    //     //     _ = _queries.SaveDomain(sBuilder).Result;
    //     // }
    // }

    /// <inheritdoc cref="GetScheduleConfig"/>
    public Task<int> GetScheduleConfig(string authorities)
    {
        var iPermission =
            _authorityManagerQueries
                .GetPermissionByMenuManagerValue(authorities, _user.UserId)
                .Result;
        return Task.FromResult((iPermission & PermissionEnum.ChayTuDong.Id) != 0
                                   ? new NCoreHelperV2023().ReturnScheduleConfigPostFace()
                                   : 0);
    }

    /// <inheritdoc cref="GetScheduleSave"/>
    [Obsolete("Obsolete")]
    public async Task<int> GetScheduleSave(int id)
    {
        return await Task.Run(async () =>
        {
            var cronJobName = "schedulerPostFace";
            var fullPath = new NCoreHelperV2023().ScheduleConfigPostFace;
            await File.WriteAllTextAsync(fullPath, id.ToString());
            if (id == 2)
            {
                // Start
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                SchedulerStart();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                RecurringJob.AddOrUpdate(cronJobName,
                                         () => SchedulerStart(),
                                         Cron.MinuteInterval(10));
            }
            else
            {
                // Remove
                RecurringJob.RemoveIfExists(cronJobName);
            }

            return 1;
        });
    }

    /// <inheritdoc cref="GetDetail"/>
    public async Task<NewsMainModel> GetDetail(string id)
    {

        var host = new NCoreHelperV2023().ReturnHostWebsite();

        var detail = await _queries.GetDetail(id);
        var maQC1 = "<div id=\"qcmgidgb\"></div>";
        var maQC2 = "<div id=\"qcmgidgb2\"></div>";
        var maQC3 = "<div id=\"qcmgidgb3\"></div>";
        var maQC4 = "<div id=\"qcmgidgb4\"></div>";
        var maQC5 = "<div id=\"qcmgidgb5\"></div>";
        var maQC6 = "<div id=\"qcmgidgb6\"></div>";
        var maQC7 = "<div id=\"qcmgidgb7\"></div>";
        var maQC8 = "<div id=\"qcmgidgb8\"></div>";

        //var maQCGA = "<div id=\"qcgb\"></div>";
        //var maQCGA2 = "<div id=\"qcgb2\"></div>";
        //maQC = "<div>sdsd</div>";
        var noiDungs = detail.Content.Split(new[] { "</p>" }, StringSplitOptions.None);
        string result = "";
        var count = noiDungs.Length;

        var pageIndex1 = 1;
        var pageIndex2 = 3;
        var pageIndex3 = 5;
        var pageIndex4 = 7;
        var pageIndex5 = 9;
        var pageIndex6 = 11;
        var pageIndex7 = 13;
        var pageIndex8 = 15;
        //if (count > 10)
        //{
        //    pageIndex1 = (count + 3) / 4;
        //    pageIndex2 = count / 2 + 2;
        //}

        for (int i = 0; i < count; i++)
        {
            if (i < count - 1)
                result += noiDungs[i] + "</p>";
            else
            {
                result += noiDungs[i];
            }

            if (i == pageIndex1 && i < count - 1)
            {
                result += maQC1;
            }
            else if (i == pageIndex2 && i < count - 1)
            {
                result += maQC2;
            }
            else if (i == pageIndex3 && i < count - 1)
            {
                result += maQC3;
            }
            else if (i == pageIndex4 && i < count - 1)
            {
                result += maQC4;
            }else if (i == pageIndex5 && i < count - 1)
            {
                result += maQC5;
            }
            else if (i == pageIndex6 && i < count - 1)
            {
                result += maQC6;
            }
            else if (i == pageIndex7 && i < count - 1)
            {
                result += maQC7;
            }
            else if (i == pageIndex8 && i < count - 1)
            {
                result += maQC8;
            }
        }

        try
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(result);
            //chèn scrip:
            // Lấy tất cả các thẻ img trong đoạn mã

            HtmlNodeCollection figureNodes = htmlDocument.DocumentNode.SelectNodes("//figure");
            // Kiểm tra xem có ít nhất một thẻ img hay không
            if (figureNodes != null && figureNodes.Count > 0)
            {
                // Tạo một thẻ div mới
                HtmlNode divNode = HtmlNode.CreateNode("<div id=\"qcImg\"> </div>");

                // Chèn thẻ div vào trước thẻ img đầu tiên
                figureNodes[0].ParentNode.InsertBefore(divNode, figureNodes[0]);
            }
            else
            {
                HtmlNodeCollection imgNodes = htmlDocument.DocumentNode.SelectNodes("//img");


                // Kiểm tra xem có ít nhất một thẻ img hay không
                if (imgNodes != null && imgNodes.Count > 0)
                {
                    // Tạo một thẻ div mới
                    HtmlNode divNode = HtmlNode.CreateNode("<div id=\"qcImg\"> </div>");

                    // Chèn thẻ div vào trước thẻ img đầu tiên
                    imgNodes[0].ParentNode.InsertBefore(divNode, imgNodes[0]);
                }
            }
            result = htmlDocument.DocumentNode.OuterHtml;

        }
        catch (Exception e)
        {
            throw;
        }

        detail.Content = result;
        if (string.IsNullOrEmpty(detail.AvatarLink))
        {
            if (!string.IsNullOrEmpty(detail.UrlRootLink))
            {
                detail.AvatarLink = detail.UrlRootLink;
                _logger.LogInformation("=====Anh null thay thế===========:" + detail.UrlRootLink);
            }
            else
            {
                detail.AvatarLink = "https://apinews.sportsandtravelonline.com/Uploads/Img//638521374628029939.png";
                _logger.LogInformation("=====Anh null===========:" + id);
                _logger.LogInformation("=====Anh id baiviet===========:" + detail.Id);
                _logger.LogInformation("=====Anh local===========:" + detail.AvatarLocal);
                _logger.LogInformation("=====Lỗi tieu de: ===========:" + detail.Name);
                var news = await _repository.GetBySecretKey(id);
                _logger.LogInformation("=====Anh news id===========:" + news?.Id);
                if (news != null && !string.IsNullOrEmpty(news.AvatarLink))
                {
                    detail.AvatarLink = news.AvatarLink;
                    _logger.LogInformation("=====Anh news avartar===========:" + news.AvatarLink);
                }
                else if (!string.IsNullOrEmpty(news.UrlRootLink))
                {
                    detail.AvatarLink = detail.UrlRootLink;
                    _logger.LogInformation("=====Anh null thay thế 2===========:" + detail.UrlRootLink);
                }

            }


        }
        else if (detail.AvatarLocal)
        {
            detail.AvatarLink = host + "" + detail.AvatarLink;
            _logger.LogInformation("=====Lỗi tieu de: ===========:" + detail.Name);
        }            
            
        return detail;

    }

    /// <inheritdoc cref="GetDetailNew"/>
    public async Task<NewsMainModel> GetDetailNew(string id)
    {
        var host = new NCoreHelperV2023().ReturnHostWebsite();

        var detail = await _queries.GetDetail(id);

        // Danh sách quảng cáo của bạn (dùng trực tiếp)
        var ads = new List<string>
    {
        @"<div class=""adsconex-banner-parallax"" data-ad-placement=""banner20"" id=""div_ub_inpage20""></div>",        
        @"<div class=""adsconex-banner"" data-ad-placement=""banner2"" id=""ub-banner2""></div>",
        @"<div class=""adsconex-banner-parallax"" data-ad-placement=""banner21"" id=""div_ub_inpage21""></div>",
        @"<div id=""qctaboo-mid""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner3"" id=""ub-banner3""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner4"" id=""ub-banner4""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner5"" id=""ub-banner5""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner6"" id=""ub-banner6""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner7"" id=""ub-banner7""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner8"" id=""ub-banner8""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner9"" id=""ub-banner9""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner10"" id=""ub-banner10""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner11"" id=""ub-banner-11""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner12"" id=""ub-banner-12""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner13"" id=""ub-banner-13""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner14"" id=""ub-banner-14""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner15"" id=""ub-banner-15""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner18n"" id=""ub-banner18""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner19"" id=""ub-banner19""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner20n"" id=""ub-banner20""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner21n"" id=""ub-banner21""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner22"" id=""ub-banner22""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner23"" id=""ub-banner23""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner24"" id=""ub-banner24""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner25"" id=""ub-banner25""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner26"" id=""ub-banner26""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner27"" id=""ub-banner27""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner16"" id=""ub-banner1-300x600""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner17"" id=""ub-banner2-300x600""></div>",
        @"<div class=""adsconex-banner"" data-ad-placement=""banner18"" id=""ub-banner3-300x600""></div>"
    };

        const int threshold = 40; // ngưỡng 40 từ
        int accumulated = 0;
        int adIndex = 0;
        if (detail == null || string.IsNullOrEmpty(detail.Content))
            return detail;

        var paragraphs = detail.Content.Split("<p>", StringSplitOptions.RemoveEmptyEntries);
        var sb = new StringBuilder();

        foreach (var paragraph in paragraphs)
        {
            // Đếm số từ trong đoạn hiện tại
            int wordCount = paragraph.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
            accumulated += wordCount;

            // Thêm đoạn vào kết quả
            sb.Append("<p>").Append(paragraph);

            // Nếu đạt ngưỡng và còn quảng cáo
            if (accumulated >= threshold && adIndex < ads.Count)
            {
                sb.Append(ads[adIndex]);
                adIndex++;

                // Reset đếm lại từ đầu (theo yêu cầu của bạn)
                accumulated = 0;
            }
        }


        var result = sb.ToString();

        // Xử lý Html để chèn <div id="qcImg"> trước figure/img đầu tiên (giữ logic bạn có)
        try
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(result);

            var figureNodes = htmlDocument.DocumentNode.SelectNodes("//figure");
            if (figureNodes != null && figureNodes.Count > 0)
            {
                var divNode = HtmlNode.CreateNode("<div id=\"qcImg\"> </div>");
                figureNodes[0].ParentNode.InsertBefore(divNode, figureNodes[0]);
            }
            else
            {
                var imgNodes = htmlDocument.DocumentNode.SelectNodes("//img");
                if (imgNodes != null && imgNodes.Count > 0)
                {
                    var divNode = HtmlNode.CreateNode("<div id=\"qcImg\"> </div>");
                    imgNodes[0].ParentNode.InsertBefore(divNode, imgNodes[0]);
                }
            }

            result = htmlDocument.DocumentNode.OuterHtml;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi xử lý HTML để chèn qcImg cho bài {Id}", id);
            throw;
        }

        // Gán lại content rồi xử lý avatar như trước
        detail.Content = result;

        if (string.IsNullOrEmpty(detail.AvatarLink))
        {
            detail.AvatarLink = "https://apinews.sportsandtravelonline.com/Uploads/Img//638521374628029939.png";
            _logger.LogInformation("=====Anh null===========:" + id);
            _logger.LogInformation("=====Anh id baiviet===========:" + detail.Id);
            _logger.LogInformation("=====Anh local===========:" + detail.AvatarLocal);
            _logger.LogInformation("=====Lỗi tieu de: ===========:" + detail.Name);
        }
        else if (detail.AvatarLocal)
        {
            detail.AvatarLink = host + "" + detail.AvatarLink;
            _logger.LogInformation("=====Lỗi tieu de: ===========:" + detail.Name);
        }

        return detail;
    }


    public async Task<NewsMainModel> GetDetailBasic(string id)
    {

        var host = new NCoreHelperV2023().ReturnHostWebsite();

        var detail = await _queries.GetDetailBasic(id);
        
        if (string.IsNullOrEmpty(detail.AvatarLink))
        {
            detail.AvatarLink = "https://apinews.sportsandtravelonline.com/Uploads/Img//638521374628029939.png";
            _logger.LogInformation("=====Anh null===========:" + id);
            _logger.LogInformation("=====Anh id baiviet===========:" + detail.Id);
            _logger.LogInformation("=====Anh local===========:" + detail.AvatarLocal);
            _logger.LogInformation("=====Lỗi tieu de: ===========:" + detail.Name);
        }
        else if (detail.AvatarLocal)
        {
            detail.AvatarLink = host + "" + detail.AvatarLink;
            _logger.LogInformation("=====Lỗi tieu de: ===========:" + detail.Name);
        }

        return detail;

    }


    /// <inheritdoc cref="ListContentByGroup"/>
    public async Task<IEnumerable<HomeMainGroupModel>> ListContentByGroup(List<Guid> groupModel, int numberOf)
    /// <inheritdoc cref="GetDetail"/>
    public async Task<NewsThreadModel> GetDetailThread(string profileId, string categoryId, int position,int top)
    {

        int positionNow = await _queries.GetPositionThread(profileId);
        var dataInfo = await _queries.GetDetailThread( categoryId,  (position-1+ positionNow)%top);
        dataInfo.Url = CheckDomain(dataInfo.Domain) + "" + dataInfo.MetaName + "-" + dataInfo.UserCode + "-" +
                       dataInfo.MetaKey;
        //Cập nhật position
        var isUpdate =await _queries.UpdateThread(profileId);
        return dataInfo;

    }

        /// <inheritdoc cref="ListContentByGroup"/>
        public async Task<IEnumerable<HomeMainGroupModel>> ListContentByGroup(List<Guid> groupModel, int numberOf)
    {
        return await Task.Run(() =>
        {
            var host = new NCoreHelperV2023().ReturnHostWebsite();
            groupModel ??= new List<Guid>();
            var typeWeb = 2;
            var typeWeb = 4;
            if (groupModel.Count == 0)
            {
                //groupModel.Add(new Guid("ff3e877d-cfed-4bc5-bb3b-7b2d27980b3d"));
                //groupModel.Add(new Guid("7202eb2f-07f8-465b-bd22-dee3fbb54885"));
                //groupModel.Add(new Guid("b493f1b1-3225-45a2-aaf2-788753e87f44"));
                //groupModel.Add(new Guid("aff363c8-5802-4eda-a3c0-40b5cd672312"));
                //groupModel.Add(new Guid("74f93626-644e-4b0f-b9dc-0954a4b31c48"));

                switch (typeWeb)
                {
                    case 1:
                        {
                            //Cele
                            groupModel.Add(new Guid("b493f1b1-3225-45a2-aaf2-788753e87f44"));
                            groupModel.Add(new Guid("7202eb2f-07f8-465b-bd22-dee3fbb54885"));
                            groupModel.Add(new Guid("aff363c8-5802-4eda-a3c0-40b5cd672312"));
                            groupModel.Add(new Guid("5be1f886-5dfd-4815-aff4-86b7bf07de23"));
                            groupModel.Add(new Guid("73838721-d11f-43de-a86c-506fb87514cc"));
                            break;
                        }
                    case 2:
                    {
                        //Chuẩn
                        groupModel.Add(new Guid("d01a8b56-2987-4e28-aaad-23ca0d741e4a"));
                        groupModel.Add(new Guid("dfcfd087-1d55-49d4-9f12-976852062200"));
                        groupModel.Add(new Guid("b2b4f2c2-69e9-4750-9feb-dbfc1d839b15"));
                        groupModel.Add(new Guid("2196a244-0ec0-4579-89fe-49e4c3781839"));
                        groupModel.Add(new Guid("8ca10812-026c-4f2d-bf3a-550c0d1337eb"));
                        break;
                    }
                        {
                            //Chuẩn
                            groupModel.Add(new Guid("78BEA156-3990-491F-9092-1818B9265ED1"));
                            groupModel.Add(new Guid("C48D3685-9F2A-472D-995D-2405111991EA"));
                            groupModel.Add(new Guid("A4331E7B-ADD6-4732-A872-3B15B468F4D3"));
                            groupModel.Add(new Guid("F8C27993-F5F0-402C-8695-5386630A4281"));
                            groupModel.Add(new Guid("A98A233D-7FBB-4F39-BC9C-8E31292C7E8A"));
                            break;
                        }
                    case 3:
                        {
                            //News
                            groupModel.Add(new Guid("5ECA838C-B4BA-4382-8B6A-87B8D82D8699"));
                            groupModel.Add(new Guid("E5E9311E-7FCC-4983-99E6-0818E4AAD341"));
                            groupModel.Add(new Guid("E81ED3E7-1AA5-48F7-960E-D27FA0FB0D67"));
                            groupModel.Add(new Guid("F09947AC-939D-486C-B806-DF86EF1B3B3D"));
                            groupModel.Add(new Guid("3F595F31-3B29-4920-8FE0-E117C92DE673"));
                            break;
                        }
                    case 4:
                        {
                            //NewsPaper
                            groupModel.Add(new Guid("8DBF3865-7494-4CA5-A29E-3B2CF30E40A1"));
                            groupModel.Add(new Guid("53133009-D967-4D4D-BDD7-6F2AD92FE26E"));
                            groupModel.Add(new Guid("FBCB44F5-5A4A-4152-B99F-AEE48D7DAFD2"));
                            groupModel.Add(new Guid("71E262E6-F490-4DE4-B473-FE3E4BCFE80C"));
                            groupModel.Add(new Guid("41301CCB-D5D5-49BD-9DC8-1F2B9C81703D"));
                            break;
                        }

                    default:
                        {
                            //Cele
                            groupModel.Add(new Guid("b493f1b1-3225-45a2-aaf2-788753e87f44"));
                            groupModel.Add(new Guid("7202eb2f-07f8-465b-bd22-dee3fbb54885"));
                            groupModel.Add(new Guid("aff363c8-5802-4eda-a3c0-40b5cd672312"));
                            groupModel.Add(new Guid("5be1f886-5dfd-4815-aff4-86b7bf07de23"));
                            groupModel.Add(new Guid("73838721-d11f-43de-a86c-506fb87514cc"));
                            break;
                        }
                }








            }

            if (numberOf == 0) numberOf = 4;
            var listData = _queries.ListContentByGroup(groupModel, numberOf).Result;
            var group = listData.Select(x => x.GroupName).Distinct();

            var listReturn = new List<HomeMainGroupModel>();

            var dateCheck = new DateTime(2023, 12, 13, 22, 30, 00);
            foreach (var groupItem in group)
            {
                var child = listData.Where(x => x.GroupName == groupItem);
                var listChild = child.Select(childItem => new HomeMainContentModel
                {
                    Id = childItem.Id,
                    Name = childItem.Name,
                    DateTimeStart = childItem.DateTimeStart,
                    AvatarLink = childItem.AvatarLocal ? host + "" + childItem.AvatarLink : childItem.AvatarLink
                }).ToList();
                foreach (var item in listChild)
                {
                    if (item.DateTimeStart <= dateCheck || string.IsNullOrEmpty(item.AvatarLink)) continue;

                    var fileExtension = Path.GetExtension(item.AvatarLink);
                    if (item.AvatarLink != null)
                    {
                        string newFileName = item.AvatarLink;
                        if (fileExtension != "")
                        {
                            newFileName = item.AvatarLink.Replace($"{fileExtension}", $"_300x300{fileExtension}");
                        }

                        item.AvatarLink = newFileName;
                    }
                }
                listReturn.Add(new HomeMainGroupModel
                {
                    GroupName = groupItem,
                    Detail = listChild
                });
            }

            return listReturn;
        });
    }

    /// <inheritdoc cref="HomeNewsLifeModel"/>
    public async Task<HomeNewsLifeModel> HomeNewsLifeModel(string id)
    {
        //Lấy Id bài đăng
        var parts = id.Split('-');

        // Lấy phần tử cuối cùng trong mảng để có ID
        var idBai = parts[^1];
        return await _queries.HomeNewsLifeModel(idBai);
    }

  

    #endregion
    #endregion
}