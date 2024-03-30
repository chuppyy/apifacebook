#region

using ITC.Infra.CrossCutting.Identity.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class ApplicationDbContext : IdentityDbContext
{
#region Constructors

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

#endregion

#region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApplicationUserMap());
        modelBuilder.ApplyConfiguration(new ApplicationRoleMap());
        modelBuilder.ApplyConfiguration(new UserTypeMap());
        modelBuilder.ApplyConfiguration(new FunctionMap());
        modelBuilder.ApplyConfiguration(new FunctionRoleMap());
        modelBuilder.ApplyConfiguration(new FunctionDecentralizationMap());
        modelBuilder.ApplyConfiguration(new ModuleMap());
        modelBuilder.ApplyConfiguration(new ModuleRoleMap());
        modelBuilder.ApplyConfiguration(new ModuleDecentralizationMap());
        modelBuilder.ApplyConfiguration(new PortalMap());
        modelBuilder.ApplyConfiguration(new ModuleGroupMap());

        base.OnModelCreating(modelBuilder);
    }

#endregion

#region Properties

    public DbSet<FunctionDecentralization> FunctionDecentralizations { get; set; }
    public DbSet<Function>                 FunctionRoles             { get; set; }
    public DbSet<Function>                 Functions                 { get; set; }
    public DbSet<ModuleDecentralization>   ModuleDecentralizations   { get; set; }
    public DbSet<ModuleGroup>              ModuleGroups              { get; set; }
    public DbSet<ModuleRole>               ModuleRoles               { get; set; }
    public DbSet<Module>                   Modules                   { get; set; }

    public DbSet<Portal> Portals { get; set; }
#pragma warning disable CS0108, CS0114
    public DbSet<ApplicationUser> Users { get; set; }
#pragma warning restore CS0108, CS0114
    public DbSet<UserType> UserTypes { get; set; }

#endregion
}