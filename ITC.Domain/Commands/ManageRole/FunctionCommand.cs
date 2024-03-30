namespace ITC.Domain.Commands.ManageRole;

public abstract class FunctionCommand : BaseIdentityCommand
{
#region Constructors

    protected FunctionCommand()
    {
    }

    protected FunctionCommand(string name, int weight, string description, string moduleId)
    {
        Name        = name;
        Weight      = weight;
        Description = description;
        ModuleId    = moduleId;
    }

#endregion

#region Properties

    public string Description { get; protected set; }
    public string ModuleId    { get; protected set; }
    public string Name        { get; protected set; }

    public int Weight { get; protected set; }

#endregion
}