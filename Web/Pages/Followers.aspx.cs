using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileService;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Web;
using System.Linq;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class Followers : SpecificCulturePage
    {
        private static int followerPage = 0;
        private static int followerSize = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            long userId;
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService usuarioService = iocManager.Resolve<IUserService>();
            try
            {
                userId = Int32.Parse(Request.Params.Get("userId"));
            }
            catch (ArgumentNullException)
            {
                userId = SessionManager.GetUserSession(Context).UserProfileId;
            }

            FollowersBlock followerBlock = usuarioService.FindAllFollowersBlock(userId, followerPage, followerSize);

            GridFollowers.DataSource = followerBlock.followers.ToList();
            GridFollowers.DataBind();


           /* if (seguidoBlock.moreFollowed)
            {
                BtnNext.Visible = true;
            }
            else
            {
                BtnNext.Visible = false;
            }
            if (followedPage > 0)
            {
                BtnPrevious.Visible = true;
            }*/
        }

        protected void GridFollowers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("navigate") == 0)
            {
                long userId =
                    (long)GridFollowers.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;

                String url = String.Format("./ShowUserDetails.aspx?userId={0}", userId);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }

    }
}