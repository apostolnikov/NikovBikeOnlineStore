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

            //check if the url contains an id parameter
            if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                FillPage(id);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ProductModel productModel = new ProductModel();
        Product product = CreateProduct();

        //check if the url contains an id parameter
        if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            //ID exits - update existing row
            int id = Convert.ToInt32(Request.QueryString["id"]);
            lblResult.Text = productModel.UpdateProduct(id, product);
        }
        else
        {
            //ID does not exist - create new row
            lblResult.Text = productModel.InsertProduct(product);
        }
    }

    private void FillPage(int id)
    {
        //Get selected product from DB
        ProductModel productModel = new ProductModel();
        Product product = productModel.GetProduct(id);

        //Fill textboxes
        txtDescription.Text = product.Description;
        txtName.Text = product.Name;
        txtPrice.Text = product.Price.ToString();

        //Set dropdown list values
        ddlImage.SelectedValue = product.Image;
        ddlType.SelectedValue = product.TypeId.ToString();

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

}