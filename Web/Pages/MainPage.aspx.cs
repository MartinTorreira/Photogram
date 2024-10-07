using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class MainPage : SpecificCulturePage
    {
        int pageNum;

        protected void Page_Load(object sender, EventArgs e)
        {

            TableRow index = new TableRow();
            TableCell cindex = new TableCell();
            cindex.Controls.Add(new LiteralControl("Images"));
            index.Cells.Add(cindex);

            TableCell cindex2 = new TableCell();
            cindex2.Controls.Add(new LiteralControl("Title"));
            index.Cells.Add(cindex2);

            Table1.Rows.Add(index);
            pageNum = 0;
            updateImageList(pageNum);
        }


        protected void Next(object sender, EventArgs e)
        {
            pageNum++;
            updateImageList(pageNum);
        }

        public void updateImageList(int page) {
            List<Image> images = SessionManager.getFollowed(Context, page);

            

            if (images.Count != 0)
            {

                for (int j = 0; j < images.Count; j++)
                {
                    TableRow r = new TableRow();

                    TableCell c = new TableCell();
                    c.Controls.Add(new LiteralControl("<img src=" + images[j].path + "  Width=" + (char)34 + "124px" + (char)34 + " Height =" + (char)34 + "120px" + (char)34 + "/>"));
                    r.Cells.Add(c);


                    string b = "<a href=" + (char)34 + "ShowImage.aspx?searchValue="+ images[j].imageId +" "+ (char)34 + " > "+images[j].title+"</a>";

                    TableCell c2 = new TableCell();
                    c2.Controls.Add(new LiteralControl(b));

                    r.Cells.Add(c2);

                    Table1.Rows.Add(r);
                }
            }else {
                btnNext.Visible = false;
            }

        }


        protected void BtnMyProfile_Click(object sender, EventArgs e)
        {
            UserProfile user = SessionManager.FindUser(Context);
            string value = user.userId.ToString();
            Response.Redirect($"~/Pages/ShowUserDetails.aspx?searchValue={value}");

        }


    }
}