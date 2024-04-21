using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ITC.Domain.Commands.NewsManagers.NewsContentManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Events;
using ITC.Domain.Core.Notifications;
using ITC.Domain.Extensions;
using ITC.Domain.Interfaces;
using ITC.Domain.Interfaces.NewsManagers.NewsContentManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsGroupManagers;
using ITC.Domain.Interfaces.StudyManagers.MinusWord;
using ITC.Domain.Interfaces.SystemManagers.SystemLogs;
using ITC.Domain.Models.NewsManagers;
using MediatR;
using NCore.Actions;
using NCore.Enums;
using NCore.Helpers;
using NCore.Modals;
using Newtonsoft.Json;

namespace ITC.Domain.CommandHandlers.NewsManagers;

/// <summary>
///     Command Handler bài viết
/// </summary>
public class NewsContentCommandHandler : CommandHandler,
                                         IRequestHandler<AddNewsContentCommand, bool>,
                                         IRequestHandler<UpdateNewsContentCommand, bool>,
                                         IRequestHandler<DeleteNewsContentCommand, bool>,
                                         IRequestHandler<UpdateTimeAutoPostNewsContentCommand, bool>
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="user"></param>
    /// <param name="newsContentRepository"></param>
    /// <param name="newsContentQueries"></param>
    /// <param name="minusWordQueries"></param>
    /// <param name="systemLogRepository"></param>
    /// <param name="newsGroupRepository"></param>
    /// <param name="newsDomainQueries"></param>
    /// <param name="newsDomainRepository"></param>
    /// <param name="uow"></param>
    /// <param name="bus"></param>
    /// <param name="notifications"></param>
    public NewsContentCommandHandler(IUser                                    user,
                                     INewsContentRepository                   newsContentRepository,
                                     INewsContentQueries                      newsContentQueries,
                                     IMinusWordQueries                        minusWordQueries,
                                     ISystemLogRepository                     systemLogRepository,
                                     INewsGroupRepository                     newsGroupRepository,
                                     INewsDomainQueries                       newsDomainQueries,
                                     INewsDomainRepository                    newsDomainRepository,
                                     IUnitOfWork                              uow,
                                     IMediatorHandler                         bus,
                                     INotificationHandler<DomainNotification> notifications) :
        base(uow, bus, notifications)
    {
        _user                 = user;
        _repository           = newsContentRepository;
        _queries              = newsContentQueries;
        _minusWordQueries     = minusWordQueries;
        _systemLogRepository  = systemLogRepository;
        _newsGroupRepository  = newsGroupRepository;
        _newsDomainQueries    = newsDomainQueries;
        _newsDomainRepository = newsDomainRepository;
    }

#endregion

    private readonly Random _random = new Random();

    private string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
                                    .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

#region IRequestHandler<AddNewsContentCommand,bool> Members

    /// <summary>
    ///     Handle thêm mới
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddNewsContentCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        var doc = new HtmlDocument();
        doc.LoadHtml(command.Content);
        if (command.Content.Contains("data:image/png;base64"))
        {
            NotifyValidationErrors("Không hỗ trợ xử lý hình ảnh base64");
            return await Task.FromResult(false);
            
            /*// Select all img elements with a base64 src attribute
            var imgElements = doc.DocumentNode.SelectNodes("//img[starts-with(@src, 'data:image/')]");

            if (imgElements != null)
            {
                foreach (var imgElement in imgElements)
                {
                    // Remove the img element
                    imgElement.Remove();
                }
            }*/
        }
        //Thêm thuộc tính cho thẻ img
        var imgTags = doc.DocumentNode.SelectNodes("//img");

        // Thêm thuộc tính decoding="async" vào mỗi thẻ <img> nếu nó chưa có
        if (imgTags != null)
        {
            foreach (var imgTag in imgTags)
            {
                // Kiểm tra xem thuộc tính decoding đã tồn tại chưa
                if (!imgTag.Attributes.Contains("decoding"))
                {
                    // Nếu chưa có, thêm thuộc tính decoding="async"
                    imgTag.Attributes.Add("decoding", "async");
                }
            }
        }
        command.Content = doc.DocumentNode.OuterHtml;

        var iCore          = new NCoreHelper();
        var iKey           = Guid.NewGuid();
        var iSecretKey     = iCore.GeneralSecretKey(iKey);
        var iPosition      = _repository.GetMaxPosition(command.NewsGroupId)?.Result ?? 0;
        
        // Thay thế từ khóa cấm
        command.Name = ReplaceMinusWord(command.Name);

        var iMetaTile = RandomString(6).ToLower(); // iCore.create_META_TITLE(command.Name);
        var iUserId   = _user.UserId;
        //========================================Xử lý đường dẫn ảnh đại diện==================================
        var iAvatarLink = command.AvatarLink;
        
        bool iAvatarLocal = !(!string.IsNullOrEmpty(iAvatarLink) && iAvatarLink.Contains("http"));
        //if (string.Compare(command.AvatarId.ToString(), Guid.Empty.ToString(), StringComparison.Ordinal) != 0)
        //{
        //    var svfInfo = _serverFileRepository.GetAsync(command.AvatarId).Result;
        //    iAvatarLink  = svfInfo.IsLocal ? svfInfo.FilePath : svfInfo.LinkUrl;
        //    iAvatarLocal = svfInfo.IsLocal;
        //}

        command.StatusId = ActionStatusEnum.Pending.Id;
        command.LinkTree = !string.IsNullOrEmpty(command.LinkTree) ? command.LinkTree.Trim() : "";
        //======================================================================================================
        var rAdd = new NewsContent(iKey,
            command.Name,
            command.Summary,
            command.Content,
            iPosition,
            iSecretKey,
           _user.StaffName,
            command.UrlRootLink,
            command.NewsGroupId,
            command.SeoKeyword,
            command.AvatarId,
            DateTime.Now,
            command.NewsGroupTypeId,
            command.AttackViewId,
            command.StatusId,
            JsonConvert.SerializeObject(command.NewsContentContentModels),
            iAvatarLink,
            iAvatarLocal,
            Guid.Empty,
            iMetaTile,
            command.AgreeVia,
            command.LinkTree,
            iUserId);
        //rAdd.AddAttack(command.NewsContentAttackModels, 1, iUserId);
        await _repository.AddAsync(rAdd);
        //=================Ghi Log==================
        await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
            DateTime.Now,
            SystemLogEnumeration.AddNew.Id,
            _user.StaffId,
            _user.StaffName,
            GetType().Name,
            "",
            iKey,
            "",
            JsonConvert.SerializeObject(command),
            iUserId));
        

        if (Commit())
        {
            //==========================================
            if (string.IsNullOrEmpty(command.LinkTree))
            {
                var group = await _queries.GetPagingById(rAdd.Id);

                // Nếu nhóm có loại twitter
                if (group.TypeId == (int)GroupType.Twitter)
                {
                    var link = CheckDomain(group.Domain) + "" + group.MetaName + "-" + group.UserCode + "-" +
                               group.MetaKey;

                    // Lấy token ngày cập nhật mới nhất và có số lần đăng nhỏ nhất
                    var firstToken = await _repository.GetTop1TokenTwitterAsync();

                    if (firstToken != null)
                    {
                        /*var apiKey = "ARIfbGxFri9pMRIdy7ddlDiXq";
                        var apiSecret = "ZOuPrLBOiN018NTCQhLGPGyLOejXvY8gEw9bSKJcsUImG4pBBK";
                        var token = "1728406911211745280-jhRqO3gsKkn1zFYExvN5zWKuHqXhUa";
                        var tokenSecret = "xhwp0IQQD5fVC92hiZqpp9QFwjB06jGdoucfUcjCzR0zm";*/

                        var linkTreeTwitter = await rAdd.ChangeToLinkTwitterAsync(firstToken.ApiKey, firstToken.ApiSecret,
                            firstToken.Token, firstToken.TokenSecret, link);
                        if (!string.IsNullOrEmpty(linkTreeTwitter))
                        {
                            rAdd.LinkTree = linkTreeTwitter;
                            firstToken.AmountPosted += 1;
                            var dateNow = DateTime.Now;
                            firstToken.ModifiedDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 12, 00, 000);
                            Commit();
                        }
                    }
                }
                else if (group.TypeId == (int)GroupType.Img)
                {
                    var link = CheckDomain(group.Domain) + "" + group.MetaName + "-" + group.UserCode + "-" +
                               group.MetaKey;
                    rAdd.LinkTree = link;
                    Commit();
                }
            }
            
            command.Id            = rAdd.Id;
            command.SecretKey = iSecretKey;
            command.ResultCommand = true;
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.them_moi_khong_thanh_cong);
        return await Task.FromResult(false);
    }
    public string CheckDomain(string value)
    {
        var check = value.Substring(value.Length - 1, 1);
        if (string.Compare(check, "/", StringComparison.Ordinal) == 0) return value;
        return value + "/";
    }

    #endregion

    #region IRequestHandler<DeleteNewsContentCommand,bool> Members

    /// <summary>
    ///     Handle xóa
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteNewsContentCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var iResult = _queries.DeleteAsync(command.Model).Result;
        if (iResult > 0)
        {
            command.ResultCommand = true;
            //=================Ghi Log==================
            await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                                                              DateTime.Now,
                                                              SystemLogEnumeration.Deleted.Id,
                                                              _user.StaffId,
                                                              _user.StaffName,
                                                              GetType().Name,
                                                              "",
                                                              Guid.Empty,
                                                              "",
                                                              JsonConvert.SerializeObject(command.Model),
                                                              _user.UserId));
            //==========================================
            return await Task.FromResult(true);
        }

        NotifyValidationErrors(NErrorHelper.xoa_khong_thanh_cong);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateNewsContentCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateNewsContentCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        var doc = new HtmlDocument();
        doc.LoadHtml(command.Content);
        if (command.Content.Contains("data:image/png;base64"))
        {
            NotifyValidationErrors("Không hỗ trợ xử lý hình ảnh base64");
            return await Task.FromResult(false);
            
            // Select all img elements with a base64 src attribute
            var imgElements = doc.DocumentNode.SelectNodes("//img[starts-with(@src, 'data:image/')]");

            if (imgElements != null)
            {
                foreach (var imgElement in imgElements)
                {
                    // Remove the img element
                    imgElement.Remove();
                }
            }
        }

        //Thêm thuộc tính cho thẻ img
        var imgTags = doc.DocumentNode.SelectNodes("//img");

        // Thêm thuộc tính decoding="async" vào mỗi thẻ <img> nếu nó chưa có
        if (imgTags != null)
        {
            foreach (var imgTag in imgTags)
            {
                // Kiểm tra xem thuộc tính decoding đã tồn tại chưa
                if (!imgTag.Attributes.Contains("decoding"))
                {
                    // Nếu chưa có, thêm thuộc tính decoding="async"
                    imgTag.Attributes.Add("decoding", "async");
                }
            }
        }
        command.Content = doc.DocumentNode.OuterHtml;

        var existing = _repository.GetAsync(command.Id).Result;
        if (existing != null)
        {
            // Lấy vị trí lịch sử đã sử dụng cho NewsAttack
            var iCore = new NCoreHelper();
            //var iHistoryPosition = _newsAttackRepository.GetMaxHistoryPosition(command.Id).Result;
            var iDateTimeStart = iCore.ConvertStringToDateTimeFromVueJs(command.DateTimeStart);

            // Thay thế từ khóa cấm
            command.Name = ReplaceMinusWord(command.Name);

            var iMetaTile = RandomString(6).ToLower(); //iCore.create_META_TITLE(command.Name);
            var iUserId   = _user.UserId;
            //========================================Xử lý đường dẫn ảnh đại diện==================================
            var iAvatarLink  = command.AvatarLink;

            bool iAvatarLocal = !(!string.IsNullOrEmpty(iAvatarLink) && iAvatarLink.Contains("http"));
            /*if (string.Compare(command.AvatarId.ToString(), Guid.Empty.ToString(), StringComparison.Ordinal) != 0)
            {
                var svfInfo = _serverFileRepository.GetAsync(command.AvatarId).Result;
                iAvatarLink  = svfInfo.IsLocal ? svfInfo.FilePath : svfInfo.LinkUrl;
                iAvatarLocal = svfInfo.IsLocal;
            }*/

            var linkTree = command.LinkTree;
            // Nếu nhập linkTree
            if (!string.IsNullOrEmpty(command.LinkTree))
            {
                linkTree = command.LinkTree.Trim();}
            else
            {
                var group = await _queries.GetPagingById(command.Id);
                if (group.TypeId == (int)GroupType.Twitter)
                {
                    var link = CheckDomain(group.Domain) + "" + group.MetaName + "-" + group.UserCode + "-" +
                               group.MetaKey;

                    // Lấy token ngày cập nhật mới nhất và có số lần đăng nhỏ nhất
                    var firstToken = await _repository.GetTop1TokenTwitterAsync();
                    
                    if (firstToken != null)
                    {
                        /*var apiKey = "ARIfbGxFri9pMRIdy7ddlDiXq";
                        var apiSecret = "ZOuPrLBOiN018NTCQhLGPGyLOejXvY8gEw9bSKJcsUImG4pBBK";
                        var token = "1728406911211745280-jhRqO3gsKkn1zFYExvN5zWKuHqXhUa";
                        var tokenSecret = "xhwp0IQQD5fVC92hiZqpp9QFwjB06jGdoucfUcjCzR0zm";*/

                        var linkTreeTwitter = await existing.CheckChangeToLinkTwitterAsync(firstToken.ApiKey, firstToken.ApiSecret,
                            firstToken.Token, firstToken.TokenSecret, link);
                        var linkTwitter = linkTreeTwitter?.Twitter;
                        _ = $"6. Link Twitter {linkTreeTwitter} ";

                        if (!string.IsNullOrEmpty(linkTwitter))
                        {
                            linkTree = linkTwitter;
                            firstToken.AmountPosted += 1;
                            var dateNow = DateTime.Now;
                            firstToken.ModifiedDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 12, 00, 000);
                        }
                    }
                    
                }else if (group.TypeId == (int)GroupType.Img)
                {
                    var link = CheckDomain(group.Domain) + "" + group.MetaName + "-" + group.UserCode + "-" +
                               group.MetaKey;
                    linkTree = link;
                }

                    /*var group = await _queries.GetPagingById(command.Id);
                    var link = CheckDomain(group.Domain) + "" + group.MetaName + "-" + group.UserCode + "-" +
                               group.MetaKey;
                    var apiKey = "ARIfbGxFri9pMRIdy7ddlDiXq";
                    var apiSecret = "ZOuPrLBOiN018NTCQhLGPGyLOejXvY8gEw9bSKJcsUImG4pBBK";
                    var token = "1728406911211745280-jhRqO3gsKkn1zFYExvN5zWKuHqXhUa";
                    var tokenSecret = "xhwp0IQQD5fVC92hiZqpp9QFwjB06jGdoucfUcjCzR0zm";

                    var linkTreeTwitter = await existing.UpdateLink(apiKey, apiSecret, token, tokenSecret, link);
                    if (!string.IsNullOrEmpty(linkTreeTwitter))
                    {
                        linkTree = linkTreeTwitter;
                    }*/
            }

                //Cập nhật link vercel khi thêm mới linktree
                if (string.IsNullOrEmpty(existing.LinkTree) && !string.IsNullOrEmpty(linkTree))
                {
                // Bảng NewsContent
                // Nếu là chuyển trạng thái từ chờ đăng => đã đăng thì cập nhật lại domain cho nhóm tin 
                var newsGroupInfo = _newsGroupRepository.GetAsync(existing.NewsGroupId).Result;
                if (newsGroupInfo != null)
                {
                    if (newsGroupInfo.AgreeVia)
                    {
                        var newsDomainFirst = _newsDomainQueries.GetFirstDomain().Result;
                        if (newsDomainFirst.Any())
                        {
                            newsGroupInfo.Domain = newsDomainFirst.FirstOrDefault()?.Name;
                            _newsGroupRepository.Update(newsGroupInfo);

                            var domainId = newsDomainFirst.FirstOrDefault()?.Id ?? Guid.Empty;
                            var newsDomainInfo = _newsDomainRepository.GetAsync(domainId).Result;
                            newsDomainInfo.IsDeleted = true;
                            newsDomainInfo.Modified  = DateTime.Now;
                            _newsDomainRepository.Update(newsDomainInfo);

                            // // Xóa dữ liệu linktree các bài viết chưa đăng mà có cùng nhóm danh mục như bài viết này !
                            // var sBuilder = new StringBuilder();
                            // sBuilder.Append(
                            //     $@"UPDATE NewsContents SET LinkTree = '' WHERE NewsGroupId = '{newsGroupInfo.Id}' AND StatusId != 3;");
                            // _ = _queries.SaveDomain(sBuilder).Result;
                        }
                    }
                }
            }

            //======================================================================================================
            existing.Update(command.Name,
                command.Summary,
                command.Content,
                existing.Position,
                existing.SecretKey,
                command.Author,
                command.UrlRootLink,
                command.NewsGroupId,
                command.SeoKeyword,
                command.AvatarId,
                iDateTimeStart,
                existing.NewsGroupTypeId,
                command.AttackViewId,
                existing.StatusId,
                JsonConvert.SerializeObject(command.NewsContentContentModels),
                iAvatarLink,
                iAvatarLocal,
                iMetaTile,
                command.AgreeVia,
                linkTree,
                iUserId);
            //existing.AddAttack(command.NewsContentAttackModels, iHistoryPosition + 1, iUserId);
            _repository.Update(existing);
            //=================Ghi Log==================
            /*await _systemLogRepository.AddAsync(new SystemLog(Guid.NewGuid(),
                DateTime.Now,
                SystemLogEnumeration.Update.Id,
                _user.StaffId,
                _user.StaffName,
                GetType().Name,
                "",
                existing.Id,
                JsonConvert.SerializeObject(existing),
                JsonConvert.SerializeObject(command),
                iUserId));*/
            //==========================================

            if (Commit())
            {
                // Xóa dữ liệu NewsAttack cũ
                //await _newsAttackQueries.DeleteAsync(command.Id);
                // Trả dữ liệu về FE
                command.ResultCommand = true;
                return await Task.FromResult(true);
            }

            NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
            return await Task.FromResult(false);
        }

        NotifyValidationErrors(NErrorHelper.du_lieu_khong_ton_tai + ": " + command.Id);
        return await Task.FromResult(false);
    }

#endregion

#region IRequestHandler<UpdateTimeAutoPostNewsContentCommand,bool> Members

    /// <summary>
    ///     Handle cập nhật thời gian đăng bài tự động
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateTimeAutoPostNewsContentCommand command, CancellationToken cancellationToken)
    {
        // Gán mặc định giá trị trả về khi xử lý = 0
        command.ResultCommand = false;

        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }

        var existing = _repository.GetAsync(command.Id)?.Result;
        if (existing != null)
        {
            existing.TimeAutoPost = command.Time;
            _repository.Update(existing);
            //==========================================
            if (Commit())
            {
                command.ResultCommand = true;
                return await Task.FromResult(true);
            }

            NotifyValidationErrors(NErrorHelper.cap_nhat_khong_thanh_cong);
            return await Task.FromResult(false);
        }

        NotifyValidationErrors(NErrorHelper.du_lieu_khong_ton_tai + ": " + command.Id);
        return await Task.FromResult(false);
    }

#endregion

    /// <summary>
    ///     Thay thế từ khóa cấm
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private string ReplaceMinusWord(string value)
    {
        var result = value;
        // Danh sách từ khóa cấm
        var listMinusWord = _minusWordQueries.GetPaging(new PagingModel
        {
            Search     = "",
            StatusId   = ActionStatusEnum.Active.Id,
            PageNumber = 0,
            PageSize   = 0
        }).Result;
        return listMinusWord.Aggregate(result, (current, items) => current.Replace(items.Root, items.Replace));
    }

#region Fields

    private readonly INewsContentRepository _repository;
    private readonly INewsContentQueries    _queries;
    private readonly IMinusWordQueries      _minusWordQueries;
    private readonly ISystemLogRepository   _systemLogRepository;
    private readonly INewsGroupRepository   _newsGroupRepository;
    private readonly INewsDomainQueries     _newsDomainQueries;
    private readonly INewsDomainRepository  _newsDomainRepository;
    private readonly IUser                  _user;

    #endregion
}