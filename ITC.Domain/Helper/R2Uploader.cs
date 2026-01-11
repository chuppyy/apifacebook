using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using SkiaSharp; // Thư viện nén ảnh
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyR2Project.Utils
{
    public class R2Config
    {
        public string AccountId { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string PublicDomain { get; set; }
        public string Folder { get; set; }
    }

    public static class R2Uploader
    {
        private static readonly HttpClient _httpClient;

        static R2Uploader()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | (SecurityProtocolType)12288;
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,
                MaxConnectionsPerServer = 200
            };
            _httpClient = new HttpClient(handler);
            _httpClient.Timeout = TimeSpan.FromSeconds(60);
        }

        // =========================================================
        // 1. UPLOAD TỪ FORM FILE (Postman, Form Data)
        // =========================================================
        public static async Task<string> UploadFromFormFile(Stream inputStream, string originalFileName, string contentType, R2Config config)
        {
            try
            {
                Stream streamToUpload = inputStream;
                string finalExtension = Path.GetExtension(originalFileName);
                string finalContentType = contentType;

                // Logic nén ảnh
                if (IsImage(originalFileName) && !originalFileName.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                {
                    var compressedStream = CompressToWebP(inputStream, quality: 75, maxWidth: 1920);
                    if (compressedStream != null)
                    {
                        streamToUpload = compressedStream;
                        finalExtension = ".webp";
                        finalContentType = "image/webp";
                    }
                }

                return await UploadStreamToR2(streamToUpload, finalContentType, finalExtension, config);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[R2 FormFile Error]: {ex.Message}");
                return null;
            }
        }

        // =========================================================
        // 2. UPLOAD TỪ BASE64 (Bổ sung cái bạn đang thiếu)
        // =========================================================
        public static async Task<string> UploadFromBase64Async(string base64Full, R2Config config, string folder)
        {
            if (string.IsNullOrEmpty(base64Full)) return null;

            try
            {
                // 1. Phân tích chuỗi Base64
                // Dạng: "data:image/png;base64,iVBORw0KGgo..."
                var match = Regex.Match(base64Full, @"data:(?<type>image/.+?);base64,(?<data>.+)");
                var cleanBase64 = base64Full;
                var contentType = "image/jpeg"; // Mặc định

                if (match.Success)
                {
                    contentType = match.Groups["type"].Value;
                    cleanBase64 = match.Groups["data"].Value;
                }

                // 2. Chuyển sang Stream
                var bytes = Convert.FromBase64String(cleanBase64);
                using var originalStream = new MemoryStream(bytes);

                // 3. Tự động nén sang WebP (Giống hàm FormFile)
                Stream streamToUpload = originalStream;
                string finalExt = GetExtension(contentType);
                string finalContentType = contentType;

                // Chỉ nén nếu không phải GIF
                if (!contentType.Contains("gif"))
                {
                    var compressedStream = CompressToWebP(originalStream, quality: 75, maxWidth: 1920);
                    if (compressedStream != null)
                    {
                        streamToUpload = compressedStream; // Dùng stream đã nén
                        finalExt = ".webp";
                        finalContentType = "image/webp";
                    }
                }

                // 4. Upload
                return await UploadStreamToR2(streamToUpload, finalContentType, finalExt, config);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[R2 Base64 Error]: {ex.Message}");
                return null;
            }
        }

        // =========================================================
        // 3. UPLOAD TỪ URL (Link ảnh có sẵn)
        // =========================================================
        public static async Task<string> UploadFromUrlAsync(string url, R2Config config, string folder)
        {
            if (string.IsNullOrEmpty(url)) return null;
            if (url.Contains(config.PublicDomain)) return url;

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return url;

                using var stream = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType ?? "image/jpeg";
                var ext = GetExtension(contentType);

                // Với URL ta upload thẳng, không nén lại để đảm bảo tốc độ tải trang gốc
                return await UploadStreamToR2(stream, contentType, ext, config);
            }
            catch { return url; }
        }

        // =========================================================
        // HÀM XỬ LÝ NÉN ẢNH (SkiaSharp)
        // =========================================================
        private static Stream CompressToWebP(Stream inputStream, int quality = 75, int maxWidth = 1920)
        {
            try
            {
                inputStream.Position = 0;
                using var originalBitmap = SKBitmap.Decode(inputStream);
                if (originalBitmap == null) return null;

                int newWidth = originalBitmap.Width;
                int newHeight = originalBitmap.Height;

                if (originalBitmap.Width > maxWidth)
                {
                    double ratio = (double)maxWidth / originalBitmap.Width;
                    newWidth = maxWidth;
                    newHeight = (int)(originalBitmap.Height * ratio);
                }

                var imageInfo = new SKImageInfo(newWidth, newHeight);
                using var resizedBitmap = originalBitmap.Resize(imageInfo, SKFilterQuality.Medium);
                using var image = SKImage.FromBitmap(resizedBitmap);

                var data = image.Encode(SKEncodedImageFormat.Webp, quality);
                return new MemoryStream(data.ToArray());
            }
            catch
            {
                return null;
            }
        }

        // =========================================================
        // HÀM CORE UPLOAD AWS S3/R2
        // =========================================================
        private static async Task<string> UploadStreamToR2(Stream stream, string contentType, string ext, R2Config config)
        {
            string folder = $"{config.Folder}/{DateTime.Now:dd-MM-yyyy}";

            stream.Position = 0;
            var fileName = $"{Guid.NewGuid()}{ext}";
            var key = $"{folder}/{fileName}";

            var credentials = new BasicAWSCredentials(config.AccessKey, config.SecretKey);
            var s3Config = new AmazonS3Config
            {
                ServiceURL = $"https://{config.AccountId}.r2.cloudflarestorage.com",
                ForcePathStyle = true
            };

            using var client = new AmazonS3Client(credentials, s3Config);

            var putRequest = new PutObjectRequest
            {
                BucketName = config.BucketName,
                Key = key,
                InputStream = stream,
                ContentType = contentType,
                DisablePayloadSigning = true
            };

            await client.PutObjectAsync(putRequest);

            var domain = config.PublicDomain.TrimEnd('/');
            return $"{domain}/{key}";
        }

        private static bool IsImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false;

            var ext = Path.GetExtension(fileName).ToLower();

            // Cách viết không cần System.Linq
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".webp" };
            return Array.IndexOf(validExtensions, ext) >= 0;
        }

        private static string GetExtension(string mime) => mime.ToLower() switch
        {
            "image/png" => ".png",
            "image/gif" => ".gif",
            "image/webp" => ".webp",
            "image/svg+xml" => ".svg",
            _ => ".jpg"
        };
    }
}