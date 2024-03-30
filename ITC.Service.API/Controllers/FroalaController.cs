#region

using System;
using System.IO;
using System.Threading.Tasks;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ITC.Service.API.Controllers;

/// <inheritdoc />
[Route("[controller]")]
[ApiController]
public class FroalaController : ApiController
{
#region Constructors

    /// <inheritdoc />
    public FroalaController(INotificationHandler<DomainNotification> notifications,
                           IMediatorHandler                         mediator) : base(notifications, mediator)
    { }

#endregion

#region Methods

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            // Xử lý tải lên ảnh
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName); // Đường dẫn lưu trữ ảnh trên máy chủ

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/images/{fileName}"; // Đường dẫn ảnh trên máy chủ
            return Ok(new { link = "https://localhost:5001/" + imageUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
#endregion
}