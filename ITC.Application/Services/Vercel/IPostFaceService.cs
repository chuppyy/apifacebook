using System;
using System.Threading.Tasks;

namespace ITC.Application.Services.Vercel;

public interface IPostFaceService
{
    Task<int> Quangcao(Guid?  newsContentId, string tkqcId, string token, string pageId, string pictureUrl,
                       string linkUrl,       string title,bool? IsPostImg);
}