using System.Collections.Generic;
using System.Threading.Tasks;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;

namespace ITC.Application.Services.Vercel;

public interface IVercelService
{
    Task<List<DomainVercel>> CreatedVercel(string tokenGit, string ownerGit, string projectDefaultGit, string teamId,
                                           string tokenVercel);
}