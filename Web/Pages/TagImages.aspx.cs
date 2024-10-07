using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class TagImages : SpecificCulturePage
    {
        int pageNum;
        string tagString;
        protected void Page_Load(object sender, EventArgs e)
        {
            tagString = Request.QueryString["Tag"];
            TableRow index = new TableRow();
            TableCell cindex = new TableCell();
            cindex.Controls.Add(new LiteralControl("Images"));
            index.Cells.Add(cindex);

            TableCell cindex2 = new TableCell();
            cindex2.Controls.Add(new LiteralControl("Title"));
            index.Cells.Add(cindex2);

            TableCell cindex3 = new TableCell();
            cindex3.Controls.Add(new LiteralControl("User"));
            index.Cells.Add(cindex3);

            TableCell cindex4 = new TableCell();
            cindex4.Controls.Add(new LiteralControl("Number of Likes"));
            index.Cells.Add(cindex4);

            TableCell cindex5 = new TableCell();
            cindex5.Controls.Add(new LiteralControl("Like"));
            index.Cells.Add(cindex5);

            Table1.Rows.Add(index);
            pageNum = 0;
            updateImageList(pageNum);
        }

        protected void Next(object sender, EventArgs e)
        {
            pageNum++;
            updateImageList(pageNum);
        }


        protected void Like(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                Response.Redirect($"~/Pages/Authentication.aspx");
            }

            SessionManager.doLike(Context, Convert.ToInt64(((Button)sender).CommandArgument.ToString()));
            Response.Redirect($"~/Pages/TagImages.aspx?Tag="+ tagString);
            //txtLikes.Text = SessionManager.getLikes(Context, searchedId).ToString();

        }
        public void updateImageList(int page)
        {
            List<Image> images = SessionManager.getImagesByTag(tagString, page);



            if (images.Count != 0)
            {

                for (int j = 0; j < images.Count; j++)
                {
                    TableRow r = new TableRow();

                    TableCell c = new TableCell();
                    c.Controls.Add(new LiteralControl("<img src=" + images[j].path + "  Width=" + (char)34 + "124px" + (char)34 + " Height =" + (char)34 + "120px" + (char)34 + "/>"));
                    r.Cells.Add(c);


                    string b = "<a href=" + (char)34 + "ShowImage.aspx?searchValue=" + images[j].imageId + " " + (char)34 + " > " + images[j].title + "</a>";

                    TableCell c2 = new TableCell();
                    c2.Controls.Add(new LiteralControl(b));

                    r.Cells.Add(c2);

                    UserProfile a = SessionManager.FindUserById(Context,(long)images[j].userId);
                    string d = "<a href=" + (char)34 + "ShowUserDetails.aspx?searchValue=" + images[j].userId + " " + (char)34 + " > " + a.loginName + "</a>";

                    TableCell c3 = new TableCell();
                    c3.Controls.Add(new LiteralControl(d));

                    r.Cells.Add(c3);

                    TableCell c4 = new TableCell();
                    c4.Controls.Add(new LiteralControl(SessionManager.getNumberOfLikes(images[j].imageId).ToString()+" Likes"));
                    r.Cells.Add(c4);


                    TableCell c5 = new TableCell();
                    Button likebutton = new Button();
                    likebutton.Click += new EventHandler(Like);
                    likebutton.Text = "Like";
                    likebutton.CommandArgument = images[j].imageId.ToString();

                    c5.Controls.Add(likebutton);
                    r.Cells.Add(c5);

                    Table1.Rows.Add(r);
                }
            }
            else
            {
                btnNext.Visible = false;
            }

        }
    }


    
}