using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileService;
using System;
using System.Web;
using System.Web.UI;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Web.UI.WebControls;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class FindUsers : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnFindUserClick(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                if (!string.IsNullOrEmpty(txtUser.Text.Trim())){
                    long userId = Convert.ToInt64(txtUser.Text);
                    UserProfile user = SessionManager.FindUserById(Context, userId);
                    if (user == null)
                    {
                        lblNotFound.Visible = true;
                    }
                    else {
                        Response.Redirect($"~/Pages/ShowUserDetails.aspx?searchValue={txtUser.Text}");
                    }
                  
                }

            }
        }
    }
}