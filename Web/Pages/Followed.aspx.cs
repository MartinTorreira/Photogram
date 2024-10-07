using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileService;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Web;
using System.Linq;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class Followed : SpecificCulturePage
    {
        private static int followedPage = 0;
        private static int followedSize = 5;
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

            FollowedBlock followedBlock = usuarioService.FindAllCreatorsBlock(userId, followedPage, followedSize);

            GridFollowed.DataSource = followedBlock.followed.ToList();
            GridFollowed.DataBind();


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

        protected void GridFollowed_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("navigate") == 0)
            {
                long userId =
                    (long)GridFollowed.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;

                String url = String.Format("./ShowUserDetails.aspx?userId={0}", userId);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }

    }
}