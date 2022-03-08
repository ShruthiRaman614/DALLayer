using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataConnection
    {

        private IList<Product> Getproducts()
        {
            string ConnectionString = "data source=HPI-5CD1462PLJ\\SQLEXPRESS; initial catalog=EFCore4PM;Integrated Security=True;";

            List<Product> productList = new List<Product>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("Select * from Products", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    productList.Add(new Product()
                    {
                        ProductId = (int)reader["ProductId"],
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        UnitPrice = (decimal)reader["UnitPrice"],
                        Summary = (string)reader["Summary"],
                        CategoryId = (int)reader["CategoryId"]
                    });


                }

                return productList;

            }



        }



        private IList<Category> GetCategories()
        {
            string ConnectionString = "data source=HPI-5CD1462PLJ\\SQLEXPRESS; initial catalog=EFCore4PM;Integrated Security=True;";

            List<Category> categoryList = new List<Category>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("Select * from Category", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    categoryList.Add(new Category()
                    {

                        Name = (string)reader["Name"],

                        CategoryId = (int)reader["CategoryId"]
                    });


                }

                return categoryList;

            }



        }

        public string InsertProducts(Product p)
        {
            string ConnectionString = "data source=HPI-5CD1462PLJ\\SQLEXPRESS; initial catalog=EFCore4PM;Integrated Security=True;";
            int rowsAffected;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("SelectAllCustomers", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar);
                param.Value = p.Name;

                param = sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar);
                param.Value = p.Description;

                param = sqlCommand.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                param.Value = p.UnitPrice;

                param = sqlCommand.Parameters.Add("@CategoryId", SqlDbType.Int);
                param.Value = p.CategoryId;

                param = sqlCommand.Parameters.Add("@Summary", SqlDbType.VarChar);
                param.Value = p.Summary;

                conn.Open();

                rowsAffected = sqlCommand.ExecuteNonQuery();




            }

            if (rowsAffected > 0)
                return "Rows inserted successfully";
            else
                return "Insertion unsuccessful";
        }

        private Product GetProducts(int id)
        {
            Product p = null;
            string ConnectionString = "data source=HPI-5CD1462PLJ\\SQLEXPRESS; initial catalog=EFCore4PM;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("GetProducts", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = sqlCommand.Parameters.Add("@id", SqlDbType.VarChar);
                param.Value = id;
                conn.Open();
                SqlDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    p = new Product();
                    p.Name = rdr["Name"].ToString();
                    p.Description = rdr["Description"].ToString();
                    p.Summary = rdr["Summary"].ToString();
                    p.UnitPrice = (decimal)rdr["UnitPrice"];
                    p.CategoryId = (int)rdr["CategoryId"];


                }
            }
            return p;
        }

        private string DeleteProducts(int id)
        {
            int AffectedRows = 0;
            string ConnectionString = "data source=HPI-5CD1462PLJ\\SQLEXPRESS; initial catalog=EFCore4PM;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("DeleteProductsById", conn);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = sqlCommand.Parameters.Add("@Id", SqlDbType.Int);
                param.Value = id;
                conn.Open();
                AffectedRows = sqlCommand.ExecuteNonQuery();

            }
            return AffectedRows > 0 ? "Rows deleted successfully" : "Rows was not deleted";
        }



    }


}