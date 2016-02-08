using System;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Management_ManageProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //to avoid to not get the data again when refreshing the page
        if (!IsPostBack)
        {
            GetImages();
        }
    }
    
    private void GetImages()
    {
        try
        {
            //get all filepaths
            string[] images = Directory.GetFiles(Server.MapPath("~/Images/Products"));

            //get all filenames and add them to an array list 
            ArrayList imageList = new ArrayList();
            foreach (var image in images)
            {
                string imageName = image.Substring(image.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                imageList.Add(imageName);
            }

            //set the array list as the dropdownsview's datasource and refresh
            ddlImage.DataSource = imageList;
            ddlImage.AppendDataBoundItems = true;
            ddlImage.DataBind();
        }
        catch (Exception e)
        {
            lblResult.Text = e.ToString();
        }
    }

    private Product CreateProduct()
    {
        Product product = new Product();
        product.Name = txtName.Text;
        product.Price = Convert.ToDecimal(txtPrice.Text);
        product.TypeId = Convert.ToInt32(ddlType.SelectedValue);
        product.Description = txtDescription.Text;
        product.Image = ddlImage.SelectedValue;

        return product;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ProductModel productModel = new ProductModel();
        Product product = CreateProduct();
        lblResult.Text = productModel.InsertProduct(product);
    }
}