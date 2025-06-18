using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProsederal
{
    internal class Program
    {
   
    public static void AddPerson(string FirstName, string LastName, string Email)
        {
            string connectionString = "Server=.;Database=Project;User Id=sa;Password=Qq123098qQ;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("_AddPerson", connection);
            command.CommandType = CommandType.StoredProcedure;


            // Add parameters
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", "Alec");
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);


            // Execute
            connection.Open();
            command.ExecuteNonQuery();


            // Retrieve the ID of the new person
            int newPersonID = (int)command.Parameters["@NewPersonID"].Value;
            Console.WriteLine($"New Person ID: {newPersonID}");


            connection.Close();
        }
        //
        public static void exsistperson(int number)
        {
            string connectionString = "Server=.;Database=Project;User Id=sa;Password=Qq123098qQ;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SP_CheckPersonExists", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PersonID", number);
            var  parm = new SqlParameter("@val", SqlDbType.Int) {Direction=ParameterDirection.ReturnValue };
            command.Parameters.Add(parm);
            try
            {
                connection.Open();
                //object x = command.ExecuteScalar();
                command.ExecuteNonQuery(); // Use ExecuteNonQuery() as no result set is returned by SELECT
                int x = -1;
                // Retrieve the return value AFTER execution
                if (parm.Value != DBNull.Value)
                {
                   x = (int)parm.Value;
                }

                // Now check the captured return value
                if (x == 1)
                {
                    Console.WriteLine($"Person with ID {number} exists.");
                    //return true;
                }
                else if (x == 0)
                {
                    Console.WriteLine($"Person with ID {number} does not exist.");
                    //return false;
                }
                else
                {
                    Console.WriteLine($"Unexpected return value from SP: {parm}");
                    //return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        static void Main(string[] args)
        {
            //AddPerson("shaen", "Kasem", "asd@gmail.com");
            //Console.WriteLine("Enter Person ID to check existence:");
            //if (int.TryParse(Console.ReadLine(), out int personId))
            //{
                exsistperson(1055);
            //}
            //else
            //{
            //    Console.WriteLine("Invalid input. Please enter a valid integer ID.");
            //}
        }
    }
}
