using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileService;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.ModelUtil.IoC;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /* UserProfileDao */
            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();
            /*CategoryDao*/
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();
            /*ImgDao*/
            kernel.Bind<IImageDao>().
                To<ImageDaoEntityFramework>();
            /*TagDao*/
            kernel.Bind<ITagDao>().
                To<TagDaoEntityFramework>();
            /*CommentDao*/
            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            /* UserService */
            kernel.Bind<IUserService>().
                To<UserProfileService>();
            /*Category Servie*/
            kernel.Bind<ICategoryService>().
                To<CategoryService>();
            /*Image Service*/
            kernel.Bind<IImageService>().
                To<ImageService>();

            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["photogramEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}