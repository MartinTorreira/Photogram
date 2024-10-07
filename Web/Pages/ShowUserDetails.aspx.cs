using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class ShowUserDetails : SpecificCulturePage
    {

        private long searchedId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ValidRedirection"] = false;

            if (Request.QueryString["searchValue"] != null)
                {
                    string userIdString = Request.QueryString["searchValue"];
                    searchedId = Convert.ToInt64(userIdString);
                    UserProfile current = null;

                    if (SessionManager.IsUserAuthenticated(Context))
                    {
                        current = SessionManager.FindUser(Context);
                    }
                    UserProfile searched = SessionManager.FindUserById(Context, searchedId);

                    txtUserId.Text = userIdString;
                    txtFirstname.Text = searched.firstName;
                    txtLastname.Text = searched.lastName;
                    txtEmail.Text = searched.email;
                    txtCountry.Text = searched.country;

                    lnkFollowers.NavigateUrl = Response.ApplyAppPathModifier("/Pages/Followers.aspx" + "?userId=" + searchedId);
                    lnkFollowed.NavigateUrl = Response.ApplyAppPathModifier("/Pages/Followed.aspx" + "?userId=" + searchedId);


                    if (!searched.Equals(current))
                    {
                        if (current == null) {
                            btnFollow.Visible = false;
                            btnUnfollow.Visible = false;
                        }
                        else if (current.UserProfile2.Contains(searched))
                        {
                            btnFollow.Visible = false;
                            btnUnfollow.Visible = true;
                        }
                        else
                        {
                            btnFollow.Visible = true;
                            btnUnfollow.Visible = false;
                        }
                    }
                    else
                    {
                        btnFollow.Visible = false;
                        btnUnfollow.Visible = false;
                    }

            }

            List<Image> latestImages = SessionManager.getLastImages(Context, searchedId);
            repeater.DataSource = latestImages;
            repeater.DataBind();

        }

        protected void BtnFollow_Click(object sender, EventArgs e)
        {
            if (searchedId != -1)
            {
                SessionManager.Follow(Context, searchedId);
                Response.Redirect("~/Pages/ShowUserDetails.aspx?searchValue=" + searchedId);
            }
        }

        protected void BtnUnfollow_Click(object sender, EventArgs e)
        {
            if (searchedId != -1)
            {
                SessionManager.UnFollow(Context, searchedId);
                Response.Redirect("~/Pages/ShowUserDetails.aspx?searchValue=" + searchedId);
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