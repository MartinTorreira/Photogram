using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{

    public partial class Logout : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.Logout(Context);
            Response.Redirect("~/Pages/Authentication.aspx");
        }
    }
}
