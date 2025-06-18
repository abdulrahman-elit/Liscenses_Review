using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace testprocedurediff
{

    internal class Program
    {
        public static string updateQuerys(int PerformanceRating,string name)
        {
            string updateQuery = @"UPDATE Employees2 SET Salary = Salary   ";
            switch (PerformanceRating)
            {
                case > 90:
                    return updateQuery += @"* 1.15 WHERE PerformanceRating > 90 and name = @name;";
                case >= 75 and <= 90:
                    return updateQuery += @"* 1.10 WHERE PerformanceRating BETWEEN 75 AND 90 and name = @name;";
                case >= 50 and <= 74:
                    return updateQuery += @"* 1.05 WHERE PerformanceRating BETWEEN 50 AND 74 and name = @name;";
                default:
                    return updateQuery += @"WHERE PerformanceRating < 50 and name = @name;";
            }
        }
        public static string GenerateSelectQuery(string Department, int PerformanceRating, string name)
        {
            double bonusPercentage = 0.0; 

            switch (Department)
            {
                case "Sales":
                    if (PerformanceRating > 90)
                        bonusPercentage = 0.15;
                    else if (PerformanceRating >= 75 && PerformanceRating <= 90)
                        bonusPercentage = 0.10;
                    else 
                        bonusPercentage = 0.05;
            
                    break;

                case "HR":
                    if (PerformanceRating > 90)
                        bonusPercentage = 0.10;
                    else if (PerformanceRating >= 75 && PerformanceRating <= 90)
                        bonusPercentage = 0.08;
                    else 
                        bonusPercentage = 0.04;
                    break;

                default: 
                    if (PerformanceRating > 90)
                        bonusPercentage = 0.08;
                    else if (PerformanceRating >= 75 && PerformanceRating <= 90)
                        bonusPercentage = 0.06;
                    else 
                        bonusPercentage = 0.03;
                    break;
            }

            string updateQuery = $@"
        select * , Salary * {bonusPercentage} as bouns from Employees2
        WHERE Name = @name AND Department = @Department ;
   ";

            return updateQuery;
        }
        public static void print(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Console.Write($"{col.ColumnName}: {row[col]} ");
                }
                Console.WriteLine();
            }
        }
        public static DataTable TestDiff()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM Employees2";
            string connectionString = "Server=.;Database=C21_DB1;User Id=sa;Password=Qq123098qQ;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error occurred: " + ex.Message);
                    }
                }
            }


            return dt;
        }
        public static void testthequerysql()
        {
            string connectionString = "Server=.;Database=C21_DB1;User Id=sa;Password=Qq123098qQ;";

            string updateQuery = @"
                UPDATE Employees2
                SET Salary = 
                    CASE 
                        WHEN PerformanceRating > 90 THEN Salary * 1.15
                        WHEN PerformanceRating BETWEEN 75 AND 90 THEN Salary * 1.10
                        WHEN PerformanceRating BETWEEN 50 AND 74 THEN Salary * 1.05
                        ELSE Salary
                    END;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        //Console.WriteLine($"{rowsAffected} row(s) updated.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        public static DataTable testthequerysql2()
        {
            string connectionString = "Server=.;Database=C21_DB1;User Id=sa;Password=Qq123098qQ;";

            string updateQuery = @"
               
                    use C21_DB1;

                    SELECT
                        Name,
                        Department,
                        Salary,
                        PerformanceRating,
                        Bonus = CASE 
                                    WHEN Department = 'Sales' THEN
                                        CASE 
                                            WHEN PerformanceRating > 90 THEN Salary * 0.15
                                            WHEN PerformanceRating BETWEEN 75 AND 90 THEN Salary * 0.10
                                            ELSE Salary * 0.05
                                        END
                                    WHEN Department = 'HR' THEN
                                        CASE 
                                            WHEN PerformanceRating > 90 THEN Salary * 0.10
                                            WHEN PerformanceRating BETWEEN 75 AND 90 THEN Salary * 0.08
                                            ELSE Salary * 0.04
                                        END
                                    ELSE
                                        CASE 
                                            WHEN PerformanceRating > 90 THEN Salary * 0.08
                                            WHEN PerformanceRating BETWEEN 75 AND 90 THEN Salary * 0.06
                                            ELSE Salary * 0.03
                                        END
                                END
                    FROM Employees2;


                    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            return dt;
                           
                        }
                        //Console.WriteLine($"{rowsAffected} row(s) updated.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                return null;
            }
        }
        private static void _testthequeryprogramingCSharp(DataRow dr)
        {
            string connectionString = "Server=.;Database=C21_DB1;User Id=sa;Password=Qq123098qQ;";

            string updateQuery = updateQuerys(Convert.ToInt32((dr["PerformanceRating"])), dr["name"].ToString());


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", dr["name"].ToString());
                        int rowsAffected = cmd.ExecuteNonQuery();
                        //Console.WriteLine($"{rowsAffected} row(s) updated.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

        }
        public static void _testthequeryprogramingCSharp(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                _testthequeryprogramingCSharp(dr);
            }
        }
        public static DataRow _testthequeryprogramingCSharp2(DataRow dr)
        {
            string connectionString = "Server=.;Database=C21_DB1;User Id=sa;Password=Qq123098qQ;";

            string updateQuery = GenerateSelectQuery(dr["Department"].ToString(),Convert.ToInt32((dr["PerformanceRating"])), dr["name"].ToString());


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", dr["name"].ToString());
                        cmd.Parameters.AddWithValue("@Department", dr["Department"].ToString());
                      using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            if (dt.Rows.Count > 0)
                            {
                                DataRow row = dt.Rows[0];
                                return row;
                            }
                            else
                            {
                                Console.WriteLine("No data found for the given criteria.");
                                return null;
                            }
                            //print(dt);
                        }
                        //Console.WriteLine($"{rowsAffected} row(s) updated.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return null;    
        }
        //public static DataTable _testthequeryprogramingCSharp2(DataTable dt)
        //{
        //    DataTable resultTable = new DataTable();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        DataRow newRow = _testthequeryprogramingCSharp2(dr);
        //        if (newRow != null)
        //        {
        //          resultTable.ImportRow(newRow);
        //        }
        //    }
        //    return resultTable;
        //}
        // Ensure this is at the top of your file
        public static string GenerateSelectQuery2(int PerformanceRating, string name)
        {
            double bonusPercentage = 0.0;

            switch (Department)
            {
                case "Sales":
                    if (PerformanceRating > 90)
                        bonusPercentage = 0.15;
                    else if (PerformanceRating >= 75 && PerformanceRating <= 90)
                        bonusPercentage = 0.10;
                    else
                        bonusPercentage = 0.05;

                    break;

                case "HR":
                    if (PerformanceRating > 90)
                        bonusPercentage = 0.10;
                    else if (PerformanceRating >= 75 && PerformanceRating <= 90)
                        bonusPercentage = 0.08;
                    else
                        bonusPercentage = 0.04;
                    break;

                default:
                    if (PerformanceRating > 90)
                        bonusPercentage = 0.08;
                    else if (PerformanceRating >= 75 && PerformanceRating <= 90)
                        bonusPercentage = 0.06;
                    else
                        bonusPercentage = 0.03;
                    break;
            }

            string updateQuery = $@"
        select * , Salary * {bonusPercentage} as bouns from Employees2
        WHERE Name = @name AND Department = @Department ;
   ";

            return updateQuery;
        }

        public static DataTable _testthequeryprogramingCSharp2(DataTable dt)
    {
        DataTable resultTable = new DataTable();
        bool schemaInitialized = false; // Flag to track if schema has been set

        foreach (DataRow dr in dt.Rows)
        {
           DataRow newRow = _testthequeryprogramingCSharp2(dr); 
            if (newRow != null)
            {
              
                if (!schemaInitialized)
                {
                
                    foreach (DataColumn col in newRow.Table.Columns)
                    {
                        resultTable.Columns.Add(col.ColumnName, col.DataType);
                    }
                    schemaInitialized = true;
                }
                resultTable.ImportRow(newRow);
            }
        }
        return resultTable;
    }

    static void Main(string[] args)
        {
            //==========================================================================================================================
            //Example 1
            //DataTable dt = TestDiff();
            //Console.WriteLine("Before update:");
            //print(dt);
            //Console.WriteLine();
            //Console.WriteLine();
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //testthequerysql();
            //stopwatch.Stop();
            //DataTable dt2 = TestDiff();
            //Console.WriteLine($"SQL query execution time: {stopwatch.ElapsedMilliseconds} ms");
            //Console.WriteLine("After update:");
            //print(dt2);
            //stopwatch.Restart();
            //DataTable dt3 = TestDiff();
            //_testthequeryprogramingCSharp(dt3);
            //stopwatch.Stop();
            //Console.WriteLine($"C# query execution time: {stopwatch.ElapsedMilliseconds} ms");
            //DataTable dt4 = TestDiff();
            //Console.WriteLine("After C# update:");
            //print(dt4);
            //Console.ReadLine();
            //==========================================================================================================================
            //Example 2
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //print(_testthequeryprogramingCSharp2(TestDiff()));
            //stopwatch.Stop();
            //Console.WriteLine("C# query execution time: " + stopwatch.ElapsedMilliseconds + " ms");
            //stopwatch.Restart();
            //print(testthequerysql2());
            //stopwatch.Stop();
            //Console.WriteLine("SQL query execution time: " + stopwatch.ElapsedMilliseconds + " ms");
            //==========================================================================================================================

        }
    }
}
