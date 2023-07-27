using System;
using System.Collections.Generic;
using System.Data;
using Web_API.Data_Layer;
using Web_API.Models;

namespace Web_API.BusinessLayer
{
    public class BL
    {

        public DataLayer dataLayer = new DataLayer();
        public List<Students> GetListOfStudents()
        {
            try
            {
                DataTable table = new DataTable();
                List<Students> listStudents = new List<Students>();
                table = dataLayer.GetListOfStudents();

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in table.Rows)
                    {
                        Students student = new Students();
                        student.studentId = Convert.ToInt32(dataRow["Id"]);
                        student.firstName = dataRow["FirstName"].ToString();
                        student.lastName = dataRow["LastName"].ToString();
                        student.studentAddress = dataRow["Address"].ToString();
                        student.studentAge = dataRow["Age"].ToString();
                        student.phoneNumber = dataRow["PhoneNo"].ToString();
                        listStudents.Add(student);
                    }
                }
                return listStudents;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetListOfStudents due to "
                   + exception.Message, exception.InnerException);
            }
        }

        public string InsertStudent(Students student)
        {
            try
            {
                string response = dataLayer.InsertStudentInDB(student);
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in InsertStudent due to "
                   + exception.Message, exception.InnerException);
            }
        }
    }
}