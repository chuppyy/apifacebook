using System;
using System.Linq;
using System.Threading.Tasks;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using ITC.Domain.Models.NewsManagers;
using ITC.Infra.Data.Context;
using ITC.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITC.Infra.Data.FunctionRepositoryQueries.NewsManagers.NewsDomainManagers;

/// <summary>
///     Class repository phòng ban
/// </summary>
public class NewsDomainRepository : Repository<NewsDomain>, INewsDomainRepository
{
    #region Fields

    private readonly EQMContext _context;

    #endregion

    #region Constructors

    public NewsDomainRepository(EQMContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task<NewsDomain> GetFirt()
    {
        var info = await _context.NewsDomains.Where(x => !x.IsDeleted).OrderBy(x => x.Created).FirstOrDefaultAsync();
        return info;
    }

    public async Task<NewsDomain> GetDomainFirtOrDefaultAsync(string name)
    {
        var info = await _context.NewsDomains.FirstOrDefaultAsync(x => !x.IsDeleted && x.Name != name);
        return info ?? new NewsDomain();
    }

    public async Task DeleteDomainByName(string name)
    {
        var info = await _context.NewsDomains.FirstOrDefaultAsync(x => !x.IsDeleted && x.Name == name);
        if (info != null)
        {
            info.IsDeleted = true;
            info.Modified = DateTime.Now;
        }
    }
}