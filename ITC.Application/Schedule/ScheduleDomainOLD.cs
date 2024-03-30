using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using ITC.Domain.Core.NCoreLocal;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Application.Schedule;

public class ScheduleDomainOLD : IInvocable
{
    private readonly INewsDomainRepository _domainRepository;

    public ScheduleDomainOLD(INewsDomainRepository domainRepository)
    {
        _domainRepository = domainRepository;
    }

    public async Task Invoke()
    {
        // Gọi file kiểm tra
        if (new NCoreHelperV2023().ReturnScheduleConfigDomain() == 2)
        {
            await _domainRepository.AddAsync(new NewsDomain(Guid.NewGuid(), "test domain", "domain new", ""));
            _domainRepository.SaveChanges();
        }
    }
}