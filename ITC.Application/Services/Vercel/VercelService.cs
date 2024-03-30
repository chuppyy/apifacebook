using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;
using ITC.Domain.Interfaces.NewsManagers.NewsVercelManagers;
using Newtonsoft.Json.Linq;

namespace ITC.Application.Services.Vercel;

public class VercelService : IVercelService
{
    private readonly INewsVercelQueries _vercelQueries;
    // private readonly string[] _domainNames =
    // {
    //     "newsnowhere",
    //     "timeswatch",
    //     "globeupdate",
    //     "todayinsight",
    //     "newslineup",
    //     "infochronicle",
    //     "newsbreakers",
    //     "freshreports",
    //     "topheadliners",
    //     "newsstreaming",
    //     "instantnewsflash",
    //     "newsflasher",
    //     "breakingnewsbuzz",
    //     "newsbulletinonline",
    //     "globalnewsviews",
    //     "currenteventsworld",
    //     "newstidbit",
    //     "expressnewsreport",
    //     "newsblastonline",
    //     "newschronicler",
    //     "todaysecho",
    //     "newswatcherpro",
    //     "insightreporters",
    //     "newsmirrors",
    //     "newsbeacon",
    //     "timenewsupdate",
    //     "infoheadlines",
    //     "thelatestnewsnow",
    //     "quicknewsbreak",
    //     "buzzworthynews",
    //     "hotpressupdate",
    //     "globechronicles",
    //     "eventreporterlive",
    //     "newswhiz",
    //     "wordonthewire",
    //     "newsfeedlive",
    //     "freshheadlinebuzz",
    //     "newstidingsnow",
    //     "trendsetternews",
    //     "globalnewscenter",
    //     "currentaffairsworld",
    //     "insightnewstoday",
    //     "newschroniclepro",
    //     "todaystidings",
    //     "newsflasherpro",
    //     "buzzfeedupdate",
    //     "breaknewsdaily",
    //     "newsbulletinlive",
    //     "worldnewsupdate",
    //     "eventchronicles",
    //     "newslineuppro",
    //     "infochronicler",
    //     "newsstreampro",
    //     "newstidbitslive",
    //     "expressnewsdaily",
    //     "newsblastpro",
    //     "newschronicleslive",
    //     "todaysechopro",
    //     "newswatcherlive",
    //     "insightreportspro",
    //     "newsmirrorspro",
    //     "newsbeaconlive",
    //     "timenewsupdate",
    //     "infoheadlinespro",
    //     "thelatestnewspro",
    //     "quicknewsbreak",
    //     "buzzworthynewspro",
    //     "hotpressupdate",
    //     "globechroniclespro",
    //     "eventreporterpro",
    //     "newswhizpro",
    //     "wordonthewire",
    //     "newsfeedlive",
    //     "freshheadlinebuzzpro",
    //     "newstidingsnow",
    //     "trendsetternews",
    //     "globalnewscenterpro",
    //     "currentaffairsworld",
    //     "insightnewstoday",
    //     "newschroniclepro",
    //     "todaystidings",
    //     "newsflasherpro",
    //     "buzzfeedupdate",
    //     "breaknewsdaily",
    //     "newsbulletinlive",
    //     "worldnewsupdate",
    //     "eventchroniclespro",
    //     "newslineuppro",
    //     "infochronicler",
    //     "newsstreampro"
    //     // Thêm các tên miền khác ở đây
    // };

    public VercelService(INewsVercelQueries vercelQueries)
    {
        _vercelQueries = vercelQueries;
    }

    public async Task<List<DomainVercel>> CreatedVercel(string tokenGit, string ownerGit, string projectDefaultGit,
                                                        string teamId,   string tokenVercel)
    {
        //Tạo project git
        if (string.IsNullOrEmpty(tokenGit) || string.IsNullOrEmpty(ownerGit) || string.IsNullOrEmpty(projectDefaultGit))
        {
            Console.WriteLine($"Vui lòng nhập đầy đủ thông tin");
            return new List<DomainVercel>();
        }

        var dateTime = DateTime.Now;
        //Tên project git new
        var projectNew = projectDefaultGit + dateTime.Day + dateTime.Month + dateTime.Hour + dateTime.Minute;
        try
        {
            // Tạo đối tượng HttpClient.
            using (var httpClient = new HttpClient())
            {
                // Cài đặt các tiêu đề của yêu cầu.
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenGit);
                // Xây dựng nội dung yêu cầu.
                var apiUrl = $"https://api.github.com/repos/{ownerGit}/{projectDefaultGit}/generate";
                var requestBody = "{\"owner\":\"" + ownerGit + "\",\"name\":\"" + projectNew +
                                  "\",\"description\":\"This is your first repository\"}";
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Your-App-Name"); // Thêm User-Agent vào yêu cầu
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                // Gửi yêu cầu POST đến API GitHub.
                var response = await httpClient.PostAsync(apiUrl, content);
                // Đọc và xử lý kết quả trả về.
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    // Xử lý dữ liệu trả về nếu cần.
                    //Console.WriteLine("Tạo project GitHub thành công!");
                    Console.WriteLine("Tạo project GitHub thành công!\r\n");
                }
                else
                {
                    // Xử lý lỗi nếu cần.
                    //Console.WriteLine($"API call failed. Status code: {response.StatusCode}, Error: {responseContent}");
                    Console.WriteLine(
                        "Tạo project GitHub thất bại;code: {response.StatusCode}, Error: {responseContent} \r\n");
                }
            }
        }
        catch (Exception ex)
        {
            // Xử lý lỗi nếu có.
            Console.WriteLine("Error occurred: " + ex.Message);
        }
        //Tạo vercel 1->60

        // tt:
        //projectNew = "vbonews18101526";
        var result = await TaoVercel(tokenGit, ownerGit, teamId, tokenVercel, projectNew);
        return result;
    }

    private async Task<List<DomainVercel>> TaoVercel(string tokenGit,    string ownerGit, string teamId,
                                                     string tokenVercel, string repoName)
    {
                   
        if (string.IsNullOrEmpty(repoName) || string.IsNullOrEmpty(teamId))
        {
            Console.WriteLine($"Vui lòng nhập đầy đủ thông tin");
            return new List<DomainVercel>();
        }

        var refVercel = "";
        var repoId    = "";

        #region Lấy repoId và refVercel

        var apiUrlRef =
            $"https://api.github.com/repos/{ownerGit}/{repoName}/git/refs/heads/main"; // URL của endpoint API
        var apiUrlRepo = $"https://api.github.com/repos/{ownerGit}/{repoName}";        // URL của endpoint API
        //Thread.Sleep(60000);
        var timeReturn = 1;
        var isRepo     = false;
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenGit);
            client.DefaultRequestHeaders.Add("User-Agent", "Your-App-Name"); // Thêm User-Agent vào yêu cầu
            //StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.GetAsync(apiUrlRef);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var parsedJson      = JObject.Parse(responseContent);
                    refVercel = (string)parsedJson["object"]["sha"];
                    //Console.WriteLine("Thông tin về tham chiếu:");
                    //Console.WriteLine(responseContent);                       
                }
                else
                {
                    //Console.WriteLine("Lỗi khi lấy Ref: " + response.StatusCode);
                    ////Console.WriteLine($"Lỗi khi lấy Ref: {response.StatusCode}");
                    //Console.WriteLine($"Lỗi khi lấy Ref: {response.StatusCode}\r\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy Ref2: " + ex.Message);
                Console.WriteLine($"Lỗi khi lấy Ref2:");
            }

            //RepoId
            try
            {
                var response = await client.GetAsync(apiUrlRepo);
                while (isRepo == false && timeReturn < 20)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var parsedJson      = JObject.Parse(responseContent);
                        repoId = (string)parsedJson["id"];
                        //Console.WriteLine("Thông tin về tham chiếu:");
                        //Console.WriteLine(responseContent);
                        isRepo = true;
                    }
                    else
                    {
                        timeReturn += 1;
                        Thread.Sleep(3000);
                        //Console.WriteLine("Lỗi khi lấy repo: " + response.StatusCode);
                        //Console.WriteLine($"Lỗi khi lấy repo: {response.StatusCode}");
                        //Console.WriteLine($"Lỗi khi lấy repo: {response.StatusCode}\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy repo2:");
            }
        }

        #endregion

        //Xử lý vercel
        var apiUrlVercel =
            $"https://api.vercel.com/v13/deployments?forceNew=0&skipAutoDetectionConfirmation=0&teamId={teamId}"; // URL của endpoint API
        var domainVercels = new List<DomainVercel>();

        // Danh sách vercel
        var domainNames = _vercelQueries.ListVercel().Result.Select(x => x.Name);
        
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenVercel);
            client.DefaultRequestHeaders.Add("User-Agent",    "Your-App-Name"); // Thêm User-Agent vào yêu cầu

            Random random        = new Random();
            var    _domainChilds = domainNames.OrderBy(x => random.Next()).Take(55).ToArray();

            for (var i = 0; i < 50; i++)
            {
                var name = _domainChilds[i] + GenerateCode(DateTime.Now);
                domainVercels.Add(new DomainVercel
                {
                    Name     = name + ".vercel.app",
                    IdDomain = 1
                });
                var requestBodyVercel = $@"
                {{
                    ""name"": ""{name}"",
                    ""gitSource"": {{
                        ""ref"": ""{refVercel}"",
                        ""repoId"": ""{repoId}"",
                        ""sha"": """",
                        ""type"": ""github""
                    }},
                    ""projectSettings"": {{
                        ""buildCommand"": ""next build"",
                        ""devCommand"": ""next"",
                        ""framework"": ""nextjs""
                    }}
                }}";
                var content = new StringContent(requestBodyVercel, Encoding.UTF8, "application/json");
                try
                {
                    var response = await client.PostAsync(apiUrlVercel, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Tạo thành công vercel:{name}\r\n");
                    }
                    else
                    {
                        Console.WriteLine($"Lỗi khi tạo vercel:{name}\r\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi gọi API: " + ex.Message);
                    Console.WriteLine($"Lỗi khi tạo vercel:{name}");
                }
            }
        }

        // Add vào db 
        return domainVercels;
    }

    //Tạo mã ngẫu nhiên
    private string GenerateCode(DateTime dateTime)
    {
        var alphabet = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz";
        var day      = dateTime.Day;
        var month    = dateTime.Month;
        var hour     = dateTime.Hour;
        var minute   = dateTime.Minute;
        var code     = $"{alphabet[day - 1]}{alphabet[month - 1]}{alphabet[hour]}{alphabet[minute]}";
        if (minute      > 51) code += "b";
        else if (minute > 25) code += "a";
        if (day > 25) code += "s";
        return code;
    }
}