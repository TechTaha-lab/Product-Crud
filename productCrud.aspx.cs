using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace project2.product_crud
{
    public partial class productCrud : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=dbs;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductsGrid();
            }
        }
        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            BindProductsGrid(); // Rebind GridView to show in edit mode
        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (fileImage.HasFile)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + fileImage.FileName;
                string filePath = Server.MapPath("ProductImages/") + fileName;
                fileImage.SaveAs(filePath);

                string ProductName = txtProductName.Text;
                int Quantity = int.Parse(txtQuantity.Text);
                string Category = txtCategory.Text;
                decimal Price = decimal.Parse(txtPrice.Text);
                string ImageUrl = "~/ProductImages/" + fileName;
                string Description = txtDescription.Text;

                string selectQuery = @"SELECT COUNT(*) FROM Products WHERE ProductName = @ProductName";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@ProductName", ProductName);

                try
                {
                    connection.Open();
                    int existingCount = (int)selectCommand.ExecuteScalar();
                    if (existingCount > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product already exists.');", true);
                    }
                    else
                    {
                        string insertQuery = @"INSERT INTO Products (ProductName, Quantity, Category, Price, ImageUrl, Description)
                                           VALUES (@ProductName, @Quantity, @Category, @Price, @ImageUrl, @Description)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@ProductName", ProductName);
                        insertCommand.Parameters.AddWithValue("@Quantity", Quantity);
                        insertCommand.Parameters.AddWithValue("@Category", Category);
                        insertCommand.Parameters.AddWithValue("@Price", Price);
                        insertCommand.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                        insertCommand.Parameters.AddWithValue("@Description", Description);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            BindProductsGrid();
                            ClearInputFields();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to add product.');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
                }
                finally
                {
                    connection.Close();
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select an image file.');", true);
            }
        }

        private void BindProductsGrid()
        {
            string query = "SELECT ProductID, ProductName, Quantity, Category, Price, ImageUrl, Description FROM Products";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();

            try
            {
                adapter.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error fetching products: {ex.Message}');", true);
            }
        }
        protected void edit(object sender, EventArgs e)
        {
            Button btnEdit = (Button)sender;
            GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

            if (gvProducts.EditIndex == row.RowIndex)
            {
                int productID = Convert.ToInt32(gvProducts.DataKeys[row.RowIndex].Values["ProductID"]);
                string productName = ((TextBox)row.Cells[1].Controls[0]).Text;
                int quantity = Convert.ToInt32(((TextBox)row.Cells[2].Controls[0]).Text);
                string category = ((TextBox)row.Cells[3].Controls[0]).Text;
                decimal price = Convert.ToDecimal(((TextBox)row.Cells[4].Controls[0]).Text);
                string description = ((TextBox)row.Cells[6].Controls[0]).Text;
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=dbs;Integrated Security=True");
                string updateQuery = @"UPDATE Products 
                               SET ProductName = @ProductName, 
                                   Quantity = @Quantity, 
                                   Category = @Category, 
                                   Price = @Price, 
                                   Description = @Description 
                               WHERE ProductID = @ProductID";

                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@Category", category);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@ProductID", productID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Update successful
                        BindProductsGrid(); // Refresh GridView to reflect changes
                        ClearInputFields(); // Clear input fields after update
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product updated successfully.');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to update product.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
                }
                finally
                {
                    connection.Close();
                }
            }
        }




        protected void delete(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            int productID = Convert.ToInt32(btnDelete.CommandArgument);

            string deleteQuery = "DELETE FROM Products WHERE ProductID = @ProductID";
            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@ProductID", productID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        BindProductsGrid(); // Rebind GridView to reflect deletion
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product deleted successfully.');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to delete product.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
                }
                finally
                {
                    connection.Close();
                }
            }
        }



        private void ClearInputFields()
        {
            txtProductName.Text = "";
            txtQuantity.Text = "";
            txtCategory.Text = "";
            txtPrice.Text = "";
            txtDescription.Text = "";
        }
    }
}
