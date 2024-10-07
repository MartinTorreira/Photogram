using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web
{

    public partial class PracticaMaD : System.Web.UI.MasterPage
    {

        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";


        protected void Page_Load(object sender, EventArgs e)
        {

            List<TagDto> tagList = SessionManager.getTagsByUsage();
            repeater.DataSource = tagList;
            repeater.DataBind();
            
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
                if (lnkFindUser != null)
                    lnkFindUser.Visible = false;
                if (lnkFollowers != null)
                    lnkFollowers.Visible = false;
                if (lnkMainPage != null)
                    lnkMainPage.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lnkUploadImage != null)
                    lnkUploadImage.Visible = false;
                if (lnkFindImage != null)
                    lnkFindImage.Visible = true;

            }
            else
            {
                if (lnkAuthentication != null)
                    lnkAuthentication.Visible = false;

            }
        }





    }


}