using FTsR.Models;
using System.Data;
using System.Data.SqlClient;
namespace FTsR.Repo
{
    public class CompaniesRepo
    {
        string connectionString;

        public CompaniesRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Companies> GetCompanies()
        {
            List<Companies> companies = new List<Companies>();
            Companies company;

            var data = GetCompaniesDataFromDb();

            foreach (DataRow row in data.Rows)
            {
                company = new Companies
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Type = row["Type"].ToString(),
                    Branch = row["Branch"].ToString()
                };
                companies.Add(company);
            }

            return companies;
        }

        private DataTable GetCompaniesDataFromDb()
        {
            var query = "SELECT Id, Name, Type, Branch FROM Companies";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //public List<CompaniesModelForGraph> GetCompaniesForGraph()
        //{
        //    List<CompaniesModelForGraph> productsForGraph = new List<CompaniesModelForGraph>();
        //    CompaniesModelForGraph companiesForGraph;

        //    var data = GetProductsForGraphFromDb();

        //    foreach (DataRow row in data.Rows)
        //    {
        //        companiesForGraph = new CompaniesModelForGraph
        //        {
        //            Type = row["Type"].ToString(),
        //            Branch = row["Branch"].ToString()
        //        };
        //        productsForGraph.Add(companiesForGraph);
        //    }

        //    return productsForGraph;
        //}

        private DataTable GetProductsForGraphFromDb()
        {
            var query = "SELECT Category, COUNT(Id) Products FROM Product GROUP BY Category";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
