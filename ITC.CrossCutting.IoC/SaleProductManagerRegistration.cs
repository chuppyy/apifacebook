using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITC.Infra.CrossCutting.IoC;

public class SaleProductManagerRegistration : Registration
{
#region Constructors

    public SaleProductManagerRegistration(IServiceCollection services) : base(services)
    {
    }

    public override void Register(IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

        // #region AboutManager
        //
        //     AddScoped<IAboutManagerAppService, AboutManagerAppService>();
        //     AddScoped<IRequestHandler<AddAboutManagerCommand, bool>, AboutManagerCommandHandler>();
        //     AddScoped<IRequestHandler<UpdateAboutManagerCommand, bool>, AboutManagerCommandHandler>();
        //     AddScoped<IRequestHandler<UpdateDetailAboutManagerCommand, bool>, AboutManagerCommandHandler>();
        //     AddScoped<IRequestHandler<DeleteAboutManagerCommand, bool>, AboutManagerCommandHandler>();
        //     AddScoped<IRequestHandler<AddAboutAttackManagerCommand, bool>, AboutAttackManagerCommandHandler>();
        //     AddScoped<IRequestHandler<UpdateAboutAttackManagerCommand, bool>, AboutAttackManagerCommandHandler>();
        //     AddScoped<IRequestHandler<DeleteAboutAttackManagerCommand, bool>, AboutAttackManagerCommandHandler>();
        //     AddScoped<IAboutManagerRepository, AboutManagerRepository>();
        //     AddScoped<IAboutAttackManagerRepository, AboutAttackManagerRepository>();
        //     AddScoped<IAboutManagerQueries>(_ => new AboutManagerQueries(connection));
        //
        // #endregion
        //
        // #region ContactManager
        //
        //     AddScoped<IContactManagerAppService, ContactManagerAppService>();
        //     AddScoped<IRequestHandler<AddContactManagerCommand, bool>, ContactManagerCommandHandler>();
        //     AddScoped<IRequestHandler<UpdateContactManagerCommand, bool>, ContactManagerCommandHandler>();
        //     AddScoped<IRequestHandler<DeleteContactManagerCommand, bool>, ContactManagerCommandHandler>();
        //     AddScoped<IContactManagerRepository, ContactManagerRepository>();
        //     AddScoped<IContactManagerQueries>(_ => new ContactManagerQueries(connection));
        //
        // #endregion
        //
        // #region ContactCustomerManager
        //
        //     AddScoped<IContactCustomerManagerAppService, ContactCustomerManagerAppService>();
        //     AddScoped<IRequestHandler<AddContactCustomerManagerCommand, bool>, ContactCustomerManagerCommandHandler>();
        //     AddScoped<IRequestHandler<HandlerUserContactCustomerManagerCommand, bool>,
        //         ContactCustomerManagerCommandHandler>();
        //     AddScoped<IContactCustomerManagerRepository, ContactCustomerManagerRepository>();
        //     AddScoped<IContactCustomerManagerQueries>(_ => new ContactCustomerManagerQueries(connection));
        //
        // #endregion
        //
        // #region SlideManager
        //
        //     AddScoped<ISlideManagerAppService, SlideManagerAppService>();
        //     AddScoped<IRequestHandler<AddSlideManagerCommand, bool>, SlideManagerCommandHandler>();
        //     AddScoped<IRequestHandler<UpdateSlideManagerCommand, bool>, SlideManagerCommandHandler>();
        //     AddScoped<IRequestHandler<DeleteSlideManagerCommand, bool>, SlideManagerCommandHandler>();
        //
        // #endregion
        //
        // #region CommentManager
        //
        //     AddScoped<ICommentManagerAppService, CommentManagerAppService>();
        //     AddScoped<IRequestHandler<AddCommentManagerCommand, bool>, CommentManagerCommandHandler>();
        //     AddScoped<IRequestHandler<UpdateCommentManagerCommand, bool>, CommentManagerCommandHandler>();
        //     AddScoped<IRequestHandler<DeleteCommentManagerCommand, bool>, CommentManagerCommandHandler>();
        //     AddScoped<IRequestHandler<AgreeCommentManagerCommand, bool>, CommentManagerCommandHandler>();
        //     AddScoped<ICommentManagerRepository, CommentManagerRepository>();
        //     AddScoped<ICommentManagerQueries>(_ => new CommentManagerQueries(connection));
        //
        // #endregion
        //     
        // #region ImageLibraryManager
        //
        //     AddScoped<IImageLibraryManagerAppService, ImageLibraryManagerAppService>();
        //     AddScoped<IRequestHandler<AddImageLibraryManagerCommand, bool>, ImageLibraryManagerCommandHandler>();
        //     AddScoped<IRequestHandler<UpdateImageLibraryManagerCommand, bool>, ImageLibraryManagerCommandHandler>();
        //     AddScoped<IRequestHandler<DeleteImageLibraryManagerCommand, bool>, ImageLibraryManagerCommandHandler>();
        //     
        //     AddScoped<IRequestHandler<AddImageLibraryDetailManagerCommand, bool>, ImageLibraryDetailManagerCommandHandler>();
        //     AddScoped<IRequestHandler<UpdateImageLibraryDetailManagerCommand, bool>, ImageLibraryDetailManagerCommandHandler>();
        //     AddScoped<IRequestHandler<DeleteImageLibraryDetailManagerCommand, bool>, ImageLibraryDetailManagerCommandHandler>();
        //     
        //     AddScoped<IImageLibraryManagerRepository, ImageLibraryManagerRepository>();
        //     AddScoped<IImageLibraryDetailManagerRepository, ImageLibraryDetailManagerRepository>();
        //     AddScoped<IImageLibraryManagerQueries>(_ => new ImageLibraryManagerQueries(connection));
        //
        // #endregion
    }

#endregion
}