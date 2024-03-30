using System.Threading.Tasks;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;

/// <summary>
///     Lớp interface repository loại nhóm tin
/// </summary>
public interface INewsDomainRepository : IRepository<NewsDomain>
{
    Task<NewsDomain> GetFirt();
    Task<NewsDomain> GetDomainFirtOrDefaultAsync(string name);
    Task DeleteDomainByName(string name);
}