#region

using ITC.Domain.Core.Events;
using ITC.Domain.Core.Models;
using ITC.Domain.Models.AuthorityManager;
using ITC.Domain.Models.CompanyManagers;
using ITC.Domain.Models.MenuManager;
using ITC.Domain.Models.NewsManagers;
using ITC.Domain.Models.SaleProductManagers;
using ITC.Domain.Models.StudyManagers;
using ITC.Domain.Models.SystemManagers;
using ITC.Infra.Data.Mappings.AuthorityManager;
using ITC.Infra.Data.Mappings.CompanyManagers;
using ITC.Infra.Data.Mappings.NewsManagers;
using ITC.Infra.Data.Mappings.SaleProductManagers;
using ITC.Infra.Data.Mappings.StudyManagers;
using ITC.Infra.Data.Mappings.SystemManagers;
using ITC.Infra.Data.Mappings.TokenTwitterManagers;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ITC.Infra.Data.Context;

/// <inheritdoc />
public class EQMContext : DbContext
{
    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorityMapping());
        modelBuilder.ApplyConfiguration(new AuthorityDetailMapping());
        modelBuilder.ApplyConfiguration(new AuthorityUserMapping());
        modelBuilder.ApplyConfiguration(new MenuGroupMapping());
        modelBuilder.ApplyConfiguration(new MenuManagerMapping());
        modelBuilder.ApplyConfiguration(new PermissionDefaultMapping());
        modelBuilder.ApplyConfiguration(new StaffManagerMapping());
        modelBuilder.ApplyConfiguration(new ServerFileMapping());
        modelBuilder.ApplyConfiguration(new NewsGroupMapping());
        modelBuilder.ApplyConfiguration(new NewsGroupTypeMapping());
        modelBuilder.ApplyConfiguration(new NewsContentMapping());
        modelBuilder.ApplyConfiguration(new NewsAttackMapping());
        modelBuilder.ApplyConfiguration(new NewsSeoKeyWordMapping());
        modelBuilder.ApplyConfiguration(new NewsCommentMapping());
        modelBuilder.ApplyConfiguration(new NewsRecruitmentMapping());
        modelBuilder.ApplyConfiguration(new SystemLogMapping());
        modelBuilder.ApplyConfiguration(new SubjectManagerMapping());
        modelBuilder.ApplyConfiguration(new SubjectTypeManagerMapping());
        modelBuilder.ApplyConfiguration(new TableDeleteManagerMapping());
        modelBuilder.ApplyConfiguration(new AboutManagerMapping());
        modelBuilder.ApplyConfiguration(new AboutAttackManagerMapping());
        modelBuilder.ApplyConfiguration(new CommentManagerMapping());
        modelBuilder.ApplyConfiguration(new ImageLibraryManagerMapping());
        modelBuilder.ApplyConfiguration(new ImageLibraryDetailManagerMapping());
        modelBuilder.ApplyConfiguration(new MinusWordMapping());
        modelBuilder.ApplyConfiguration(new NewsViaMapping());
        modelBuilder.ApplyConfiguration(new NewsGithubMapping());
        modelBuilder.ApplyConfiguration(new NewsDomainMapping());
        modelBuilder.ApplyConfiguration(new NewsConfigMapping());
        modelBuilder.ApplyConfiguration(new NewsVercelMapping());
        modelBuilder.ApplyConfiguration(new TokenTwitterMapping());
    }

    #endregion

    #region Constructors

    public EQMContext(DbContextOptions<EQMContext> options) : base(options)
    {
    }

    protected EQMContext()
    {
    }

    #endregion

    #region Properties

    public DbSet<SystemLog>                 SystemLogs                 { get; set; }
    public DbSet<Authority>                 Authorities                { get; set; }
    public DbSet<AuthorityDetail>           AuthorityDetails           { get; set; }
    public DbSet<AuthorityUser>             AuthorityUsers             { get; set; }
    public DbSet<MenuGroup>                 MenuGroups                 { get; set; }
    public DbSet<MenuManager>               MenuManagers               { get; set; }
    public DbSet<PermissionDefault>         PermissionDefaults         { get; set; }
    public DbSet<StaffManager>              StaffManagers              { get; set; }
    public DbSet<ServerFile>                ServerFiles                { get; set; }
    public DbSet<NewsGroup>                 NewsGroups                 { get; set; }
    public DbSet<NewsGroupType>             NewsGroupTypes             { get; set; }
    public DbSet<NewsContent>               NewsContents               { get; set; }
    public DbSet<NewsAttack>                NewsAttacks                { get; set; }
    public DbSet<NewsSeoKeyWord>            NewsSeoKeyWords            { get; set; }
    public DbSet<NewsComment>               NewsComments               { get; set; }
    public DbSet<NewsRecruitment>           NewsRecruitments           { get; set; }
    public DbSet<SubjectManager>            SubjectManagers            { get; set; }
    public DbSet<SubjectTypeManager>        SubjectTypeManagers        { get; set; }
    public DbSet<TableDeleteManager>        TableDeleteManagers        { get; set; }
    public DbSet<AboutManager>              AboutManagers              { get; set; }
    public DbSet<AboutAttackManager>        AboutAttackManagers        { get; set; }
    public DbSet<ContactManager>            ContactManagers            { get; set; }
    public DbSet<ContactCustomerManager>    ContactCustomerManagers    { get; set; }
    public DbSet<CommentManager>            CommentManagers            { get; set; }
    public DbSet<ImageLibraryManager>       ImageLibraryManagers       { get; set; }
    public DbSet<ImageLibraryDetailManager> ImageLibraryDetailManagers { get; set; }
    public DbSet<MinusWord>                 MinusWords                 { get; set; }
    public DbSet<NewsVia>                   NewsVias                   { get; set; }
    public DbSet<NewsGithub>                NewsGithubs                { get; set; }
    public DbSet<NewsDomain>                NewsDomains                { get; set; }
    public DbSet<NewsConfig>                NewsConfigs                { get; set; }
    public DbSet<NewsVercel>                NewsVercels                { get; set; }
    public DbSet<TokenTwitter> TokenTwitters { get; set; }

    #endregion
}