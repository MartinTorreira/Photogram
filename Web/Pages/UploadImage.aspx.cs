using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class UploadImage : SpecificCulturePage
    {
        void Page_Load(Object sender, EventArgs e)
        {

            // Load data for the DropDownList control only once, when the 
            // page is first loaded.
            if (!IsPostBack)
            {

                // Specify the data source and field names for the Text 
                // and Value properties of the items (ListItem objects) 
                // in the DropDownList control.
                DropDownListCategories.DataSource = CreateDataSource();
                DropDownListCategories.DataTextField = "CategoryTextField";
                DropDownListCategories.DataValueField = "CategoryValueField";

                // Bind the data to the control.
                DropDownListCategories.DataBind();

                // Set the default selected item, if desired.
                DropDownListCategories.SelectedIndex = 0;

            }

        }

        ICollection CreateDataSource()
        {

            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("CategoryTextField", typeof(String)));
            dt.Columns.Add(new DataColumn("CategoryValueField", typeof(String)));

            // Populate the table with sample values.
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICategoryService categoryService = iocManager.Resolve<ICategoryService>();
            List<Category> listC = categoryService.FindAllCategories();
            listC.ForEach(category =>
            {
                dt.Rows.Add(CreateRow(category.name, category.name, dt));
            });

            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;

        }

        DataRow CreateRow(String Text, String Value, DataTable dt)
        {

            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }
        protected void DropDownListCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void UploadPhoto(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (txtExposureTime.Text == "")
                {
                    txtExposureTime.Text = "0";
                }
                if (txtApertureSize.Text == "")
                {
                    txtApertureSize.Text = "0";
                }
                if (txtWhite.Text == "")
                {
                    txtWhite.Text = "0";
                }
                List<String> tagList = txtTags.Text.Split(' ').ToList();
                tagList.ForEach(tag =>
                {
                    if (tag == "")
                    {
                        tagList.Remove(tag);
                    }
                });
                SessionManager.UploadImage(Context, txtTitle.Text, txtDescription.Text, DropDownListCategories.SelectedValue, Convert.ToInt64(txtExposureTime.Text),
                    Convert.ToInt64(txtApertureSize.Text), Convert.ToInt64(txtWhite.Text), FileUpload.FileContent, FileUpload.FileName, MapPath("~"), tagList);

                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/MainPage.aspx"));
            }
        }
    }
}