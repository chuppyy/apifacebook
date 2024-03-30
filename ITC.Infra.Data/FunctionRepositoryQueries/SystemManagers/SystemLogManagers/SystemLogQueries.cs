#region

using ITC.Domain.Interfaces.SystemManagers.SystemLogs;

#endregion

namespace ITC.Infra.Data.FunctionRepositoryQueries.SystemManagers.SystemLogManagers;

/// <summary>
///     SystemLog queries
/// </summary>
public class SystemLogQueries : ISystemLogQueries
{
#region Fields

    private readonly string _connectionString;

#endregion

#region Constructors

    public SystemLogQueries(string connectionString)
    {
        _connectionString = connectionString;
    }

#endregion
}