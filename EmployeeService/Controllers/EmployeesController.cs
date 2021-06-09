using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace EmployeeService.Controllers
{
    [EnableCorsAttribute("*","*","*")]
    public class EmployeesController : ApiController
    {
        // GET: Employee
        //public IEnumerable<Employee> Get()
        //{
        //    using(EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        return entities.Employees.ToList();
        //    }
        //}
        
        [System.Web.Http.HttpGet]
        public HttpResponseMessage FetchEmployees(int id)
        {
            using(EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity= entities.Employees.FirstOrDefault(e => e.ID == id);
                if (entity!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id: " + id + " not found.");
                }
            }
        }
        //[EnableCors("*", "*", "*")]
        //[DisableCors]
        //public HttpResponseMessage Get(string Gender = "All")
        //{
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        switch (Gender.ToLower())
        //        {
        //            case "all":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
        //            case "male":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(emp => emp.Gender == "male").ToList());
        //            case "female":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(emp => emp.Gender == "female").ToList());
        //            default:
        //                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for gender must be all,male,female. " + Gender + " is not valid");
        //        }
        //    }
        //}
        [BasicAuthentication]
        public HttpResponseMessage Get(string Gender = "All")
        {
            string userName = Thread.CurrentPrincipal.Identity.Name;

            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {                
                switch (userName.ToLower())
                {                   
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(emp => emp.Gender == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(emp => emp.Gender == "female").ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri+employee.ID.ToString());
                    return message;
                }
            }  
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put(int id,[FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity != null)
                    {
                        entity.FirstName=employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id: " + id + " not found to update.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity!=null)
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id: " + id + " not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}