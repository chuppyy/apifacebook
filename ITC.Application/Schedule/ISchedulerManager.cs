using System.Threading.Tasks;

namespace ITC.Application.Schedule;

public interface ISchedulerManager
{
    Task StartSchedulerDomain();
}