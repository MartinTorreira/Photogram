using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class ShowImage : System.Web.UI.Page
    {
        private long searchedId = -1;
        public int page = 0;
        private List<Comment> latestComments = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["searchValue"] != null) 
            {
                string imageIdString = Request.QueryString["searchValue"];
                searchedId = Convert.ToInt64(imageIdString);

                Image image = SessionManager.getImage(Context, searchedId);

                Console.WriteLine(searchedId);

                txtImageId.Text = searchedId.ToString();
                txtName.Text =  image.title;
                txtDescription.Text = image.description;
                txtAuthor.Text = image.UserProfile.firstName;
                imagePath.Src = image.path;
                imagePath.Width = 124;
                imagePath.Height = 120;
                txtAperture.Text = image.apertureSize.ToString();
                txtExposure.Text = image.exposureTime.ToString();
                txtWhite.Text = image.whiteBalance.ToString();
                txtLikes.Text = SessionManager.getLikes(Context, searchedId).ToString();

                foreach (Tag a in image.Tag) {
                    TableRow r = new TableRow();
                    TableCell c = new TableCell();

                    c.Controls.Add(new LiteralControl(a.title));
                    r.Cells.Add(c);
                    Table1.Rows.Add(r);

                }

                repeater_Bind();
            }

        }

        protected void repeater_Bind() {
            latestComments = SessionManager.getComments(Context, searchedId, page, 10);
            repeater.DataSource = latestComments;
            repeater.DataBind();
        }

        protected void Next(object sender, EventArgs e)
        {
            int commentnum = SessionManager.getNumberOfComments(searchedId);
            if (commentnum % 10 < page) {
                page = page +1;
            }
            repeater_Bind();
            
        }
        protected void Previous(object sender, EventArgs e)
        {
            if (page != 0) {
                page = page - 1;
            }
            repeater_Bind();
        }

        protected void Like(object sender, EventArgs e) {
        if (!SessionManager.IsUserAuthenticated(Context))
        {
            Response.Redirect($"~/Pages/Authentication.aspx");
        }
        string imageIdString = Request.QueryString["searchValue"];
        searchedId = Convert.ToInt64(imageIdString);

        SessionManager.doLike(Context,searchedId);
        txtLikes.Text = SessionManager.getLikes(Context, searchedId).ToString();

        }

        protected void Unlike(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                Response.Redirect($"~/Pages/Authentication.aspx");
            }
            string imageIdString = Request.QueryString["searchValue"];
            searchedId = Convert.ToInt64(imageIdString);

            SessionManager.unLike(Context, searchedId);
            txtLikes.Text = SessionManager.getLikes(Context, searchedId).ToString();

        }

        protected void Comment_Submit(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                Response.Redirect($"~/Pages/Authentication.aspx?imageId={searchedId}");
            
            }
            string imageIdString = Request.QueryString["searchValue"];
            searchedId = Convert.ToInt64(imageIdString);
            SessionManager.addComment(Context, comment.InnerText.Trim(), searchedId);
            Response.Redirect($"~/Pages/ShowImage.aspx?searchValue={searchedId}");
            

        }
        protected void Comment_Delete(object sender, EventArgs e)
        {

            string imageIdString = Request.QueryString["searchValue"];
            searchedId = Convert.ToInt64(imageIdString);

            SessionManager.deleteComment(Context, searchedId, Convert.ToInt64(((Button)sender).CommandArgument.ToString()));
            Response.Redirect($"~/Pages/ShowImage.aspx?searchValue={searchedId}");
        }

        protected void Comment_Edit(object sender, EventArgs e)
        {
            Response.Redirect($"~/Pages/EditComment.aspx?searchValue={Convert.ToInt64(((Button)sender).CommandArgument.ToString())}");

        }

        protected Boolean ShowButton(long id)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                return false;
            }
            
            return SessionManager.compareId(Context, id);
        }


    }
}