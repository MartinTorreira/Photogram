using Es.Udc.DotNet.PracticaMaD.Model.UserProfileService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Web.Security;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class Authentication : SpecificCulturePage
    {
        private long searchedId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPasswordError.Visible = false;
            lblLoginError.Visible = false;
            if (Request.QueryString["imageId"] != null)
            {
                string imageIdString = Request.QueryString["imageId"];
                searchedId = Convert.ToInt64(imageIdString);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance
        /// containing the event data.</param>
        protected void BtnLoginClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    SessionManager.Login(Context, txtLogin.Text,
                        txtPassword.Text, checkRememberPassword.Checked);

                    FormsAuthentication.
                        RedirectFromLoginPage(txtLogin.Text,
                            checkRememberPassword.Checked);

                    UserProfile user = SessionManager.FindUser(Context);
                    string value = user.userId.ToString();
                   

                    if(searchedId != -1)
                    {
                        Response.Redirect($"~/Pages/ShowImage.aspx?searchValue={searchedId}");
                    }else
                    {
                        Response.Redirect($"~/Pages/ShowUserDetails.aspx?searchValue={value}");
                    }
                }
                catch (InstanceNotFoundException)
                {
                    lblLoginError.Visible = true;
                }
                catch (IncorrectPasswordException)
                {
                    lblPasswordError.Visible = true;
                }
            }
        }
    }
}