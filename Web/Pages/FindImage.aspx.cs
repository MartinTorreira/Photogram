using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class FindImage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DropDownListCategories.DataSource = CreateDataSource();
                DropDownListCategories.DataTextField = "CategoryTextField";
                DropDownListCategories.DataValueField = "CategoryValueField";

                DropDownListCategories.DataBind();
            }
        }

        ICollection CreateDataSource()
        {

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("CategoryTextField", typeof(String)));
            dt.Columns.Add(new DataColumn("CategoryValueField", typeof(String)));

            // Add an initial null value
            dt.Rows.Add(CreateRow("", "", dt));

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICategoryService categoryService = iocManager.Resolve<ICategoryService>();
            List<Category> listC = categoryService.FindAllCategories();
            listC.ForEach(category =>
            {
                dt.Rows.Add(CreateRow(category.name, category.name, dt));
            });

            DataView dv = new DataView(dt);
            return dv;

        }

        DataRow CreateRow(String Text, String Value, DataTable dt)
        {

            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }
        protected void DropDownListCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = DropDownListCategories.SelectedValue;
            Session["SelectedCategory"] = selectedCategory;
        }

        protected void BtnFindImageClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!string.IsNullOrEmpty(txtImage.Text.Trim()))
                {
                    List<Image> imageList = SessionManager.findByKeyword(Context, txtImage.Text);

                    if (Session["SelectedCategory"] != null)
                    {
                        string value = Session["SelectedCategory"].ToString();
                        Category cat = SessionManager.getCategoryByName(Context, value);
                        imageList = SessionManager.filterByCategory(Context, imageList, cat.categoryId);
                    }
                    
                    if (imageList.Count > 0)
                    {
                        string url = "~/Pages/FindImageResult.aspx?";
                        List<long> ids = new List<long>();
                        foreach (Image i in imageList)
                        {
                            ids.Add(i.imageId);
                            url += $"Id={i.imageId}&";
                        }
                        url = url.TrimEnd('&');
                        Response.Redirect(url);
                    }
                    else
                    {
                        lblNotFound.Visible = true;
                    }
                }


            }
        }
    }
}