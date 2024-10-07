using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;


namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class EditComment : System.Web.UI.Page
    {
        private long searchedId = -1;
        private long imageId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["searchValue"] != null)
            {
                string imageIdString = Request.QueryString["searchValue"];
                searchedId = Convert.ToInt64(imageIdString);

                Comment c = SessionManager.getComment(Context,searchedId);

                //comment.InnerText = c.content;
                imageId = c.imageId;

                if (!SessionManager.compareId(Context, c.userId))
                {
                    comment.Visible = false;
                    btnSubmit.Visible = false;
                }

            }
        }


        protected void Comment_Update(object sender, EventArgs e)
        {
            SessionManager.updateComment(Context, searchedId, comment.InnerText);
            Response.Redirect($"~/Pages/ShowImage.aspx?searchValue={imageId}");

        }
    }
}