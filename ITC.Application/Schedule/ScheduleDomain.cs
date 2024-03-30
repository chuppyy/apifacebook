using System;
using System.Threading.Tasks;
using ITC.Domain.Core.NCoreLocal;
using ITC.Domain.Interfaces.NewsManagers.NewsDomainManagers;
using ITC.Domain.Models.NewsManagers;

namespace ITC.Application.Schedule;

/// <summary>
/// Hàm xử lý chạy lập lịch
/// </summary>
public class SchedulerManager : ISchedulerManager
{
    private readonly INewsDomainRepository _domainRepository;

    public SchedulerManager(INewsDomainRepository domainRepository)
    {
        _domainRepository = domainRepository;
    }

    async Task ISchedulerManager.StartSchedulerDomain()
    {
        if (new NCoreHelperV2023().ReturnScheduleConfigDomain() == 2)
        {
            await _domainRepository.AddAsync(new NewsDomain(Guid.NewGuid(), "test domain", "domain new", ""));
            _domainRepository.SaveChanges();
        }
    }
}