using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using Web_API.Models;

namespace Web_API.Data_Layer
{
    public class DataLayer
    {
        string _connectionString = "";

        public DataLayer()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["DemoCN"].ConnectionString;
        }

        public DataTable GetListOfStudents()
        {
            try
            {
                List<Students> lstStudents = new List<Students>();
                DataTable dataTable = new DataTable();
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("GetStudentList", con);
                    command.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                    con.Close();
                }
                return dataTable;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetListOfStudents due to "
                   + exception.Message, exception.InnerException);
            }
        }

        public string InsertStudentInDB(Students students)
        {
            try
            {
                string response = "";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("InsertStudent", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@firstName", students.firstName);
                    command.Parameters.AddWithValue("@lastName", students.lastName);
                    command.Parameters.AddWithValue("@studentAddress", students.studentAddress);
                    command.Parameters.AddWithValue("@studentAge", Convert.ToInt32(students.studentAge));
                    command.Parameters.AddWithValue("@phoneNumber", Convert.ToInt32(students.phoneNumber));

                    command.Parameters.Add("@PKID", SqlDbType.Int, 32);
                    command.Parameters["@PKID"].Direction = ParameterDirection.Output;

                    con.Open();
                    command.ExecuteNonQuery();

                    response = Convert.ToString(command.Parameters["@PKID"].Value);
                    con.Close();
                }
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in InsertStudentInDB due to "
                   + exception.Message, exception.InnerException);
            }
        }
    }
}