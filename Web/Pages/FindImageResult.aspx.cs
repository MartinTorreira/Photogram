using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class FindImageResult : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    string[] imageIds = Request.QueryString["Id"].Split(',');
                    List<Image> imageList = new List<Image>(); 
                    foreach (string id in imageIds)
                    {
                        if (long.TryParse(id, out long imageId))
                        {
                            imageList.Add(SessionManager.getImageById(Context,imageId));
                        }
                    }

                    repeater.DataSource = imageList;
                    repeater.DataBind();
                }
            }
        }


        protected void rptImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image img = (Image)e.Item.DataItem;
                System.Web.UI.WebControls.Image imgThumbnail = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgThumbnail");
                HyperLink lnkImage = (HyperLink)e.Item.FindControl("lnkImage");

                imgThumbnail.ImageUrl = img.path;
                lnkImage.NavigateUrl = $"ShowImage.aspx?searchValue={img.imageId}";
                lnkImage.Text = img.title;
                lnkImage.Visible = true;
            }
        }

    }
    
}