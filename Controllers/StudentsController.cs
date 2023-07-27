using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.BusinessLayer;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class StudentsController : ApiController
    {
        public BL dataLayer = new BL();

        [HttpGet]
        [Route("GetListOfStudents")]
        public List<Students> GetListOfBootCampStudents()
        {
            try
            {
                List<Students> students = new List<Students>();
                students = dataLayer.GetListOfStudents();
                return students;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetListOfStudents due to "
                   + exception.Message, exception.InnerException);
            }
        }

        [HttpPost]
        [Route("InsertStudent")]
        public string InsertStudent([FromBody] Students student)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string resposne = dataLayer.InsertStudent(student);
                    if (!string.IsNullOrEmpty(resposne))
                    {
                        return "Student Added Successfully!";
                    }
                    else
                    {
                        return "Student Not Added Successfully!";
                    }
                }
                else
                {
                    return "Model Is Not Valid";
                }
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
