#region

using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.ManageRole;

public class ModuleGroupCommand : Command
{
#region Methods

    public override bool IsValid()
    {
        return true;
    }

#endregion

#region Properties

    public string Description { get; set; }
    public string Id          { get; set; }
    public string Name        { get; set; }

#endregion
}

public class AddModuleGroupCommand : ModuleGroupCommand
{
}

public class UpdateModuleGroupCommand : ModuleGroupCommand
{
}

public class RemoveModuleGroupCommand : ModuleGroupCommand
{
#region Constructors

    public RemoveModuleGroupCommand(string id)
    {
        Id = id;
    }

#endregion
}