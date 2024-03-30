using System;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Core.Models;
using ITC.Domain.Interfaces.NewsManagers.NewsContentManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsContentManagers;

/// <summary>
///     Class repository bài viết
/// </summary>
public class NewsContentRepository : Repository<NewsContent>, INewsContentRepository
{
#region Fields

    private readonly EQMContext _context;

#endregion

#region Constructors

    public NewsContentRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

#endregion

    /// <inheritdoc cref="GetMaxPosition" />
    public async Task<int> GetMaxPosition(Guid typeId)
    {
        return 1;
        var result = await _context.NewsContents.Where(x => x.NewsGroupTypeId == typeId).MaxAsync(x => x.Position);
        return result + 1;
    }

    /// <inheritdoc cref="GetBySecretKey" />
    public async Task<NewsContent> GetBySecretKey(string secretKey)
    {
        return await _context.NewsContents.FirstOrDefaultAsync(x => x.SecretKey == secretKey);
    }

    public async Task<NewsContent> GetByIdAsync(Guid id)
    {
        var result = await _context.NewsContents.FindAsync(id); ;
        return result;
    }

    public async Task<TokenTwitter> GetTop1TokenTwitterAsync()
    {
        var dateNow = DateTime.Now;
        var toDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 00, 00, 000);
        var modifiedDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 12, 00, 000);
        var result = await _context.TokenTwitters.Where(x=>!x.IsDeleted).OrderByDescending(x=>x.ModifiedDate).ThenBy(x=>x.AmountPosted).FirstOrDefaultAsync();
        if (result == null) return null;
        {
            if (result.ModifiedDate >= toDate) return result;
            // Cập nhật tất cả số lần đã đăng token về 0
            var listUpdate = await _context.TokenTwitters.Where(x => !x.IsDeleted && x.ModifiedDate < toDate).ToListAsync();
            foreach (var item in listUpdate)
            {
                item.ModifiedDate = modifiedDate;
                item.AmountPosted = 0;
            }
        }
        return result;
    }
}