using FTsR.Models;
using Humanizer.Bytes;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Razor;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FTsR.Repo
{
    public class PinRepository
    {
        string connectionString;

        public PinRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Pin> GetPins()
        {
            List<Pin> pins = new List<Pin>();
            Pin pin;

            var data = GetPinDetailsFromDb();

            foreach (DataRow row in data.Rows)
            {
                var bytesAuthor = (byte[])row["AuthorProfileImage"];
                var bytesImage = (byte[])row["Image"];

                pin = new Pin
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Author = row["Author"].ToString(),
                    Title = row["Title"].ToString(),
                    Description = row["Description"].ToString(),
                    AuthorProfileImage = bytesAuthor,
                    Image = bytesImage,
                };
                pins.Add(pin);
            }

            return pins;
        }

        private DataTable GetPinDetailsFromDb()
        {
            var query = "SELECT Id, Author, Title, Description, AuthorProfileImage, Image FROM Pin";
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