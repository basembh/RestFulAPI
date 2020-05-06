using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestFulAPI.Models; // That contains the data member




namespace RestFulAPI.Controllers
{
    
    public class SchoolController : ApiController
    {





        /////////////////////////////////////
        // get retrive data //
        public IHttpActionResult GetAllSchools()
        {           
            IList<SchoolViewModel> schools = null;
            using (var x = new TestSHEntities())
            {         
                schools = x.Schools
                        .Select(c => new SchoolViewModel()
                        {
                            RawID = c.RawID,
                            SchoolID = c.SchoolID,
                            School_Name = c.School_Name,
                            City = c.City,
                            Directorate = c.Directorate,
                            Lat = c.Lat,
                            Long= c.Long
                        }).ToList<SchoolViewModel>();                      
            }
            if (schools.Count == 0)
                return NotFound();
            return Ok(schools);
        }





        /////////////////////////////////////
        // POST - Insert new Record 
        public IHttpActionResult PostNewSchool(SchoolViewModel school)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data. Please Check again");

            using (var x = new TestSHEntities())
            {
                x.Schools.Add(new School()
                {
                    //RawID = c.RawID,
                    SchoolID = school.SchoolID,
                    School_Name = school.School_Name,
                    City = school.City,
                    Directorate = school.Directorate,
                    Lat = school.Lat,
                    Long = school.Long
                });
                x.SaveChanges();
            }
            return Ok();
        }





        /////////////////////////////////////
        // PUT - update school data based on Id 
        public IHttpActionResult PutSchool(SchoolViewModel school)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data. Please Check again");

            using (var x = new TestSHEntities())
            {
                var checkExistingSchool = x.Schools.Where(c => c.RawID == school.RawID)
                                                    .FirstOrDefault<School>();

                if (checkExistingSchool != null)
                {
                    checkExistingSchool.SchoolID = school.SchoolID;
                    checkExistingSchool.School_Name = school.School_Name;
                    checkExistingSchool.City = school.City;
                    checkExistingSchool.Directorate = school.Directorate;
                    checkExistingSchool.Lat = school.Lat;
                    checkExistingSchool.Long = school.Long;
                    x.SaveChanges();
                }
                else
                {
                    return NotFound();
                }                                   
            }
            return Ok();
        }




        /////////////////////////////////////
        // Delete - Delete school data based on Id 
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid enter valid school id");

            using (var x = new TestSHEntities())
            {
                var school = x.Schools
                    .Where(c => c.RawID == id)
                    .FirstOrDefault();
                x.Entry(school).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();
        }





    }
}
