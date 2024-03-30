using System;
using System.Collections.Generic;
using Azure.Core;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ITC.Domain.Core.Models;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;
using NCore.Actions;
using NCore.Responses;
using Newtonsoft.Json.Linq;
using OAuth;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     Bài viết
/// </summary>
public class NewsContent : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public NewsContent(Guid   id,              string name,         string summary,   string   content,
                       int    position,        string secretKey,    string author,    string   urlRootLink,
                       Guid   newsGroupId,     string seoKeyword,   Guid   avatarId,  DateTime dateTimeStart,
                       Guid   newsGroupTypeId, int    attackViewId, int    statusId,  string   contentJson,
                       string avatarLink,      bool   avatarLocal,  Guid   projectId, string   metaTitle,
                       bool   agreeVia,        string linkTree,     string createdBy = null)
        : base(id, createdBy)
    {
        NewsAttacks = new List<NewsAttack>();
        ProjectId = projectId;
        Update(name, summary, content, position, secretKey, author, urlRootLink, newsGroupId, seoKeyword, avatarId,
               dateTimeStart, newsGroupTypeId, attackViewId, statusId, contentJson, avatarLink, avatarLocal,
               metaTitle, agreeVia, linkTree, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NewsContent()
    {
    }

    /// <summary>
    ///     Tên bài viết
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Tóm tắt
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     Tác giả
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    ///     Link đường dẫn gốc
    /// </summary>
    public string UrlRootLink { get; set; }

    /// <summary>
    ///     Nhóm tin
    /// </summary>
    public Guid NewsGroupId { get; set; }

    /// <summary>
    ///     Từ khóa SEO
    /// </summary>
    public string SeoKeyword { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Đường dẫn hình đại diện
    /// </summary>
    public string AvatarLink { get; set; }

    /// <summary>
    ///     Là file trong hệ thống
    /// </summary>
    public bool AvatarLocal { get; set; }

    /// <summary>
    ///     Ngày viết bài
    /// </summary>
    public DateTime DateTimeStart { get; set; }

    /// <summary>
    ///     Loại nhóm dữ liệu
    /// </summary>
    public Guid NewsGroupTypeId { get; set; }

    /// <summary>
    ///     Chế độ hiển thị file đính kèm
    /// </summary>
    public int AttackViewId { get; set; }

    /// <summary>
    ///     Json lưu cấu trúc bài viết
    /// </summary>
    public string ContentJson { get; set; }

    /// <summary>
    ///     MetaTitle
    /// </summary>
    public string MetaTitle { get; set; }

    /// <summary>
    ///     Lượt xem
    /// </summary>
    public int ViewEye { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    public bool   AgreeVia { get; set; }
    public string LinkTree { get; set; }

    /// <summary>
    /// Thời gian đăng bài tự động
    /// </summary>
    public DateTime? TimeAutoPost { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<NewsAttack> NewsAttacks { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên bài viết</param>
    /// <param name="summary">Tóm tắt</param>
    /// <param name="content">Nội dung</param>
    /// <param name="position">Vị trí</param>
    /// <param name="secretKey">Mã bí mật</param>
    /// <param name="author">Tác giả bài viết</param>
    /// <param name="urlRootLink">Đường dẫn gốc</param>
    /// <param name="newsGroupId">Loại tin</param>
    /// <param name="seoKeyword">Từ khóa SEO</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="dateTimeStart">Ngày viết bài</param>
    /// <param name="newsGroupTypeId">Loại bài viết</param>
    /// <param name="attackViewId">Chế độ hiển thị file đính kèm</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="contentJson">Cấu trúc bài viết</param>
    /// <param name="avatarLink">Đường dẫn ảnh đại diê</param>
    /// <param name="avatarLocal">Là hình ảnh trong hệ thống</param>
    /// <param name="metaTitle">đường dẫn website</param>
    /// <param name="linkTree"></param>
    /// <param name="createdBy">Người tạo</param>
    /// <param name="agreeVia"></param>
    public void Update(string name,         string summary,   string   content,       int    position,
                       string secretKey,    string author,    string   urlRootLink,   Guid   newsGroupId,
                       string seoKeyword,   Guid   avatarId,  DateTime dateTimeStart, Guid   newsGroupTypeId,
                       int    attackViewId, int    statusId,  string   contentJson,   string avatarLink,
                       bool   avatarLocal,  string metaTitle, bool     agreeVia,      string linkTree,
                       string createdBy = null)
    {
        Name            = name;
        Summary         = summary;
        Content         = content;
        Position        = position;
        SecretKey       = secretKey;
        Author          = author;
        UrlRootLink     = urlRootLink;
        NewsGroupId     = newsGroupId;
        SeoKeyword      = seoKeyword;
        AvatarId        = avatarId;
        DateTimeStart   = dateTimeStart;
        NewsGroupTypeId = newsGroupTypeId;
        AttackViewId    = attackViewId;
        StatusId        = statusId;
        ContentJson     = contentJson;
        AvatarLink      = avatarLink;
        AvatarLocal     = avatarLocal;
        MetaTitle       = metaTitle;
        AgreeVia        = agreeVia;
        LinkTree        = linkTree;
        Update(createdBy);
    }

    /// <summary>
    ///     Thêm file đính kèm
    /// </summary>
    /// <param name="models">Danh sách file đính kèm</param>
    /// <param name="historyPosition">Lịch sử cập nhật</param>
    /// <param name="userId">Mã người tạo</param>
    public void AddAttack(List<NewsContentAttackModel> models, int historyPosition, string userId)
    {
        foreach (var items in models)
            NewsAttacks.Add(new NewsAttack(Guid.NewGuid(), items.FileId, items.AttackDateTime, items.IsDownload,
                                           ActionStatusEnum.Active.Id, historyPosition, userId));
    }

    public async Task<string> ChangeToLinkTwitterAsync(string apiKey, string apiSecretKey, string accessToken, string accessTokenSecret, string link)
    {
        const string apiUrl = "https://api.twitter.com/2/tweets";

        var request = new OAuthRequest
        {
            Method = "POST",
            Type = OAuthRequestType.ProtectedResource,
            SignatureMethod = OAuthSignatureMethod.HmacSha1,
            ConsumerKey = apiKey,
            ConsumerSecret = apiSecretKey,
            Token = accessToken,
            TokenSecret = accessTokenSecret,
            RequestUrl = apiUrl
        };
        using var client = new HttpClient();
        // Generate OAuth header
        var oauthHeader = request.GetAuthorizationHeader();

        // Set up request data
        var tweetText = link;
        var requestData = $"{{\"text\": \"{tweetText}\"}}";

        // Add OAuth header to request
        client.DefaultRequestHeaders.Add("Authorization", oauthHeader);

        // Make the request
        var response = await client.PostAsync(apiUrl, new StringContent(requestData, Encoding.UTF8, "application/json"));

        // Read the response
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        Console.WriteLine(responseBody);
        var parsedJson = JObject.Parse(responseBody);
        var url = (string)parsedJson["data"]?["text"];
        return url;
    }
    public async Task<TestDto> CheckChangeToLinkTwitterAsync(string apiKey, string apiSecretKey, string accessToken, string accessTokenSecret, string link)
    {
        var check = "";
        const string apiUrl = "https://api.twitter.com/2/tweets";

        var request = new OAuthRequest
        {
            Method = "POST",
            Type = OAuthRequestType.ProtectedResource,
            SignatureMethod = OAuthSignatureMethod.HmacSha1,
            ConsumerKey = apiKey,
            ConsumerSecret = apiSecretKey,
            Token = accessToken,
            TokenSecret = accessTokenSecret,
            RequestUrl = apiUrl
        };
        using var client = new HttpClient();

        check += "- Mở cổng kết nối Twitter ";

        // Generate OAuth header
        var oauthHeader = request.GetAuthorizationHeader();

        check += "- Generate OAuth header ";
        // Set up request data
        var tweetText = link;
        var requestData = $"{{\"text\": \"{tweetText}\"}}";

        // Add OAuth header to request
        client.DefaultRequestHeaders.Add("Authorization", oauthHeader);

        check += "- Add OAuth header to request ";
        // Make the request
        var response = await client.PostAsync(apiUrl, new StringContent(requestData, Encoding.UTF8, "application/json"));

        // Read the response
        var responseBody = await response.Content.ReadAsStringAsync();

        check += $"- Read the response {responseBody} ";
        //Console.WriteLine(responseBody);
        var parsedJson = JObject.Parse(responseBody);
        check += $"- Parse Json {parsedJson} ";
        var url = (string)parsedJson["data"]?["text"];

        check += $"- Result {url} ";
        return new TestDto
        {
            Twitter = url,
            Check = check
        };
    }

    public class TestDto
    {
        public string Twitter { get; set; }
        public string Check { get; set; }
    }
}