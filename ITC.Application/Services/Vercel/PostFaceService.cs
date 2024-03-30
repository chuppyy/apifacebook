using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Core.NCoreLocal;
using ITC.Domain.Interfaces.NewsManagers.NewsContentManagers;
using Microsoft.Extensions.Logging;
using NCore.Actions;
using Newtonsoft.Json.Linq;

namespace ITC.Application.Services.Vercel;

public class PostFaceService : IPostFaceService
{
    private readonly INewsContentQueries      _newsContentQueries;
    private readonly ILogger<PostFaceService> _logger;

    public PostFaceService(INewsContentQueries      newsContentQueries,
                           ILogger<PostFaceService> logger)
    {
        _newsContentQueries = newsContentQueries;
        _logger             = logger;
    }

    public async Task<int> Quangcao(Guid?  newsContentId, string tkqcId, string token, string pageId, string pictureUrl,
                                    string linkUrl,
                                    string title,bool?IsPostImg)
    {
        int result = 1;
        //Lấy token page
        var pageToken = await PageToken(token, pageId);
        var effective_object_story_id = "";
        var i = 1;
        title = title.Replace("\"", "");
        if (IsPostImg==true)
        {
            Thread.Sleep(1000);
            while ((effective_object_story_id == "" || effective_object_story_id == null) && i < 5)
            {

                effective_object_story_id = await DangAnh(pageToken, title, pageId, pictureUrl);
                i++;
                Thread.Sleep(3000);
            }
            if (string.IsNullOrEmpty(effective_object_story_id))
            {
                _logger.LogInformation("=====Lỗi đăng Anh: :" + newsContentId);
                return 0;
            }
            _logger.LogInformation("=====Dang Anh Xong: :" + newsContentId);
            var x = await DangBinhLuan(pageToken, effective_object_story_id, linkUrl);
        }
        else
        {
            //ĐĂNG QUẢNG CÁO
            var adCreativeId = await Dangquangcao(tkqcId, token, pageId, pictureUrl, linkUrl, title);
            if (string.IsNullOrEmpty(adCreativeId)) return 0;
            //Lấy mã bài quảng cáo              

            //MessageBox.Show("====Id Quảng cáo và tokenpage: ===========:" + adCreativeId + ":" + pageToken);
            Thread.Sleep(8000);
            while ((effective_object_story_id == "" || effective_object_story_id == null) && i < 10)
            {
                Thread.Sleep(3000);
                effective_object_story_id = await IdQuangCao(token, adCreativeId);
                i++;
            }

            if (string.IsNullOrEmpty(effective_object_story_id))
            {
                _logger.LogInformation("=====Loi Dang Link: :" + newsContentId);
                return 0;
            }

            //Đăng bài lên page
            //var kk = await DangLenPage(pageToken, effective_object_story_id);
            //MessageBox.Show("=====Bắt đầu xử lý quảng cáo===========:");
            result = await DangLenPage(pageToken, effective_object_story_id);
            _logger.LogInformation("=====Dang Link Xong: :" + newsContentId);
        }
        if (result > 0)
        {
            _logger.LogInformation("=====Bắt đầu xử lý quảng cáo: XONG===========:" + newsContentId);
            var sBuilder = new StringBuilder();
            sBuilder.Append(
                $@"UPDATE NewsContents SET StatusId = {ActionStatusEnum.Active.Id} WHERE Id = '{newsContentId}';");
            _ = _newsContentQueries.SaveDomain(sBuilder).Result;
            _logger.LogInformation("=====Bắt đầu xử lý quảng cáo: CẬP NHẬT DB XONG===========:" + newsContentId);
        }
        else
        {
            _logger.LogInformation("=====Lỗi  quảng cáo: XONG===========:" + newsContentId);
            var sBuilder = new StringBuilder();
            sBuilder.Append(
                $@"UPDATE NewsContents SET StatusId = {ActionStatusEnum.Pending.Id} WHERE Id = '{newsContentId}';");
            _ = _newsContentQueries.SaveDomain(sBuilder).Result;
            _logger.LogInformation("=====cập nhạt lỗi quảng cáo: CẬP NHẬT DB XONG===========:" + newsContentId);
        }

        return result;




        ////ĐĂNG QUẢNG CÁO
        //var adCreativeId = await Dangquangcao(tkqcId, token, pageId, pictureUrl, linkUrl, title);
        //if (string.IsNullOrEmpty(adCreativeId)) return 0;
        ////Lấy mã bài quảng cáo
        ////Lấy token page
        //var pageToken = await PageToken(token, pageId);

        //var effective_object_story_id = "";
        //var i                         = 1;
        //_logger.LogInformation("====Id Quảng cáo và tokenpage: ===========:" + adCreativeId+":"+pageToken);
        //Thread.Sleep(8000);
        //while (string.IsNullOrEmpty(effective_object_story_id) && i < 30)
        //{
        //    Thread.Sleep(3000);
        //    effective_object_story_id = await IdQuangCao(token, adCreativeId);
        //    i++;
        //}

        //if (string.IsNullOrEmpty(effective_object_story_id))
        //{
        //    _logger.LogInformation("====LỖI: ===========:" + newsContentId);
        //    return 0;
        //}

        ////Đăng bài lên page
        ////var kk = await DangLenPage(pageToken, effective_object_story_id);
        //_logger.LogInformation("=====Bắt đầu xử lý quảng cáo===========:" + newsContentId);
        //var result = await DangLenPage(pageToken, effective_object_story_id);
        //if (result > 0)
        //{
        //    _logger.LogInformation("=====Bắt đầu xử lý quảng cáo: XONG===========:" + newsContentId);
        //    var sBuilder = new StringBuilder();
        //    sBuilder.Append(
        //        $@"UPDATE NewsContents SET StatusId = {ActionStatusEnum.Active.Id} WHERE Id = '{newsContentId}';");
        //    _ = _newsContentQueries.SaveDomain(sBuilder).Result;
        //    _logger.LogInformation("=====Bắt đầu xử lý quảng cáo: CẬP NHẬT DB XONG===========:" + newsContentId);
        //}

        return result;
    }

    private async Task<string> DangAnh(string tokenPage, string title, string idPage, string imageUrl)
    {
        var result = "";
        //string accessToken = "EAAMFx9dc1NMBOzZCSpx4XmO91ZBXcVCb5ut4yezRi5DQf5tqCjrr0EkjbHj6mcWjgzZCM6x3SZBNqYEJ1SBi9g5ZCBCBEkSE28VxrVZBbfqDowOU6g5NqyIwTSuTcQlVQKioEv4sYxbBL4rM9EmXwRv1ZCJZBCg0yZCOBT2fQHtbm0MYBGjv2LhBr7xvSaWfBjc8V03enXnAd";
        //string adCreativeId = "23862411508580086";
        title= title.Replace("\n", "\\n");
        
        // Create an HttpClient instance
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = $"https://graph.facebook.com/v16.0/{idPage}/photos";

            // Thông tin thay thế cho các tham số
            //string accessToken = "EAAMFx9dc1NMBO8Vye1stszYQoRsNZAWed0Vxy7U856jnmkiRAuMYMCdYgljMsnMKT0BKf4wIWJNKxazUadA8DjPN581ZBksW96xuuyzTfKkxMdekZAiy9ECAZCeg3hVBZCLzOxpIobW3RIWfjD8qSHLVXTRo0fF0pTPCSjZCZAaXAyq4vJuQQQ1sItOu8jThDuZAejrpBDKg4uMxXCIZD";
            //string message = "Test";

          
            // Tạo dữ liệu JSON
            string jsonData = $@"{{
                ""access_token"":""{tokenPage}"",
                ""message"":""{title}"",

                ""url"":""{imageUrl}""
            }}";

            // Tạo nội dung yêu cầu HTTP
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Gửi yêu cầu POST đến Facebook API
            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            // Xử lý kết quả
            if (response.IsSuccessStatusCode)
            {
                // Read and display the response content
                var responseBody = await response.Content.ReadAsStringAsync();
                var parsedJson = JObject.Parse(responseBody);
                result = (string)parsedJson["post_id"];
            }
            else
            {
                Console.WriteLine($"Lỗi: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }

        return result;
    }

    private async Task<int> DangBinhLuan(string accessToken, string effective_object_story_id, string link)
    {
        //string accessToken = "EAAMFx9dc1NMBO7hmnFbIMFrOPufOCjqoJ0u0DtapCvUj4pZCQQ4YOzNOtkUQ8DstvOAAvxGZCzxpIN93xEzB4wk1xjEb0CUvnFsIwvv4ZACP3oHvDYX3XOcpwS33aEYqD5nOac83kml5digqia8UnG3QAj882fcldjIsd8L5uuz0vcg4wsXamZCO5YzhIbSCZBVwOcQPSeofCW4UZD";

        // Define the URL

        try
        {
            var postUrl = $"https://graph.facebook.com/v18.0/{effective_object_story_id}/comments";
            string message = $"Read more 👉:{link}";
            using (HttpClient client = new HttpClient())
            {
                // Tạo đối tượng HttpRequestMessage với method POST và nội dung JSON
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, postUrl);
                request.Content = new StringContent(
                    $"{{\"access_token\":\"{accessToken}\",\"message\":\"{message}\"}}",
                    Encoding.UTF8,
                    "application/json"
                );
                // Gửi yêu cầu và nhận phản hồi
                HttpResponseMessage response = await client.SendAsync(request);
                // Đọc và hiển thị phản hồi
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
        }
        catch (Exception e)
        {
            return 0;
        }
        return 1;
    }

    private async Task<string> Dangquangcao(string tkqcId,  string token, string pageId, string pictureUrl,
                                            string linkUrl, string title)
    {
        title = title.Replace("\"","");
        title = title.Replace("\n", "\\n");
        var adCreativeId = "";
        using (var client = new HttpClient())
        {
            // Define the URL "{NCoreHelperV2023.RewriteUrl(title)+".com"}"
            var url = "https://graph.facebook.com/v16.0/act_" + tkqcId + "/adcreatives?access_token=" + token;
            // Define the JSON payload
            var jsonPayload = $@"
                        {{
                            ""object_story_spec"": {{
                                ""page_id"": ""{pageId}"",
                                ""link_data"": {{
                                    ""picture"": ""{pictureUrl}"",
                                    ""link"": ""{linkUrl}"",
                                    ""caption"": ""{NCoreHelperV2023.RewriteUrl(title) +".com"}"",
                                    ""message"": ""{title}"",
                                    ""name"": "" "",
                                    ""description"": "" ""
                                }}
                            }},
                            ""degrees_of_freedom_spec"": {{
                                ""creative_features_spec"": {{
                                    ""standard_enhancements"": {{
                                        ""enroll_status"": ""OPT_OUT""
                                    }}
                                }}
                            }}
                        }}";

            // Prepare the content
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Send the POST request
            try
            {
                var response = await client.PostAsync(url, content);

                // Check the response status and content
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var parsedJson      = JObject.Parse(responseContent);
                    adCreativeId = (string)parsedJson["id"];
                    Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
            catch (Exception e)
            {
                // _logger.LogInformation("=================Dangquangcao-loi-e===================");
                // _logger.LogInformation("response.StatusCode: " + e.Message);
            }
        }

        return adCreativeId;
    }

    private async Task<string> IdQuangCao(string token, string adCreativeId)
    {
        var result = "";
        //string accessToken = "EAAMFx9dc1NMBOzZCSpx4XmO91ZBXcVCb5ut4yezRi5DQf5tqCjrr0EkjbHj6mcWjgzZCM6x3SZBNqYEJ1SBi9g5ZCBCBEkSE28VxrVZBbfqDowOU6g5NqyIwTSuTcQlVQKioEv4sYxbBL4rM9EmXwRv1ZCJZBCg0yZCOBT2fQHtbm0MYBGjv2LhBr7xvSaWfBjc8V03enXnAd";
        //string adCreativeId = "23862411508580086";

        // Create an HttpClient instance
        using (var client = new HttpClient())
        {
            // Define the URL with the access token and page ID
            var url =
                $"https://graph.facebook.com/v16.0/{adCreativeId}?access_token={token}&fields=effective_object_story_id";

            try
            {
                // Send a GET request
                var response = await client.GetAsync(url);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and display the response content
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var parsedJson   = JObject.Parse(responseBody);
                    result = (string)parsedJson["effective_object_story_id"];
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        return result;
    }

    private async Task<string> PageToken(string accessToken, string pageId)
    {
        var result = "";
        //string accessToken = "EAAMFx9dc1NMBOzZCSpx4XmO91ZBXcVCb5ut4yezRi5DQf5tqCjrr0EkjbHj6mcWjgzZCM6x3SZBNqYEJ1SBi9g5ZCBCBEkSE28VxrVZBbfqDowOU6g5NqyIwTSuTcQlVQKioEv4sYxbBL4rM9EmXwRv1ZCJZBCg0yZCOBT2fQHtbm0MYBGjv2LhBr7xvSaWfBjc8V03enXnAd";

        // Create an HttpClient instance
        try
        {
            using (var client = new HttpClient())
            {
                // Define the URL with the access token and fields
                var url =
                    $"https://graph.facebook.com/v16.0/{pageId}?fields=access_token&access_token={accessToken}";

                try
                {
                    // Send a GET request
                    var response = await client.GetAsync(url);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read and display the response content
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var parsedJson   = JObject.Parse(responseBody);
                        result = (string)parsedJson["access_token"];
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        catch (Exception E)
        {
        }

        return result;
    }

    private async Task<int> DangLenPage(string accessToken, string effective_object_story_id)
    {
        //string accessToken = "EAAMFx9dc1NMBO7hmnFbIMFrOPufOCjqoJ0u0DtapCvUj4pZCQQ4YOzNOtkUQ8DstvOAAvxGZCzxpIN93xEzB4wk1xjEb0CUvnFsIwvv4ZACP3oHvDYX3XOcpwS33aEYqD5nOac83kml5digqia8UnG3QAj882fcldjIsd8L5uuz0vcg4wsXamZCO5YzhIbSCZBVwOcQPSeofCW4UZD";

        // Define the URL
        var url = $"https://graph.facebook.com/v16.0/{effective_object_story_id}";

        // Define the JSON payload
        var jsonPayload = "{\"is_published\": \"true\"}";

        // Create an HttpClient instance
        using (var client = new HttpClient())
        {
            // Add the access token to the request headers
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            // Create the request content with the JSON payload
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                // Send a POST request with the JSON payload
                var response = await client.PostAsync(url, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and display the response content
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        return 1;
    }
}