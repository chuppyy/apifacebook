#region

using System.Linq;
using ITC.Infra.CrossCutting.Identity.Interfaces;
using ITC.Infra.CrossCutting.Identity.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Repository;

public class ManageRoleRepository : Repository<Module>, IManageRoleRepository
{
#region Constructors

    public ManageRoleRepository(ApplicationDbContext context)
        : base(context)
    {
    }

#endregion

#region IManageRoleRepository Members

    public bool IsExistModuleByIdentity(string identity)
    {
        return Db.Modules.Any(x => x.Identity == identity);
    }


    public bool IsExistModuleByIdentity(string moduleId, string identity)
    {
        return Db.Modules.Any(x => x.Id != moduleId && x.Identity == identity);
    }

    public override Module GetById(string id)
    {
        var obj = Db.Modules.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (obj != null)
        {
            Db.Entry(obj).Collection(x => x.ModuleRoles).Load();
            Db.Entry(obj).Collection(x => x.Functions).Load();
        }

        return obj;
    }


    public void AddFunction(Function obj)
    {
        Db.Functions.Add(obj);
    }


    public Function GetFunctionById(string id)
    {
        return Db.Functions.FirstOrDefault(x => x.Id == id);
    }

    public Function GetFunctionById(string id, int weight)
    {
        return Db.Functions.FirstOrDefault(x => x.Id == id && x.Weight == weight);
    }

    public bool IsExistFunctionByModuleId(string moduleId, int weight)
    {
        return Db.Functions.Any(x => x.ModuleId == moduleId && x.Weight == weight);
    }

    public void RemoveFunction(Function obj)
    {
        Db.Functions.Remove(obj);
    }

    public void UpdateFunction(Function obj)
    {
        Db.Entry(obj).State = EntityState.Modified;
    }

    public void AddUserType(UserType obj)
    {
        Db.UserTypes.Add(obj);
    }

    public void UpdateUserType(UserType obj)
    {
        Db.Entry(obj).State = EntityState.Modified;
    }

    public UserType GetUserTypeById(string id, string userId = null)
    {
        if (string.IsNullOrEmpty(userId))
            return Db.UserTypes.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        return Db.UserTypes.FirstOrDefault(x => x.Id == id && x.User.CreateBy == userId && !x.IsDeleted);
    }

    public Portal AddPortal(Portal obj)
    {
        return Db.Portals.Add(obj).Entity;
    }

    public void UpdatePortal(Portal obj)
    {
        Db.Entry(obj).State = EntityState.Modified;
    }

    public void RemovePortal(Portal obj)
    {
        Db.Portals.Remove(obj);
    }

    public Portal GetPortalById(int id)
    {
        return Db.Portals.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
    }

    public Portal GetPortalByIdentity(string identity)
    {
        return Db.Portals.FirstOrDefault(x => x.Identity == identity && !x.IsDeleted);
    }

#endregion
}