using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class teacherController : ApiController
    {
         [Route("api/Teacher/info")]
        public IHttpActionResult GetTeacher()
            {
                    IList<Teacher_View> tech = null;

                    using (var db=new TeacherEntities())
                    {
                        tech = db.Teacher_Info.Select(s=>new Teacher_View()
                        {
                            Id= s.Teacher_Id,
                            Name=s.Teacher_Name,
                            email=s.Teacher_Email
                        }).ToList<Teacher_View>();    
                    }
                if(tech.Count==0)
                {
                    return NotFound();
                }
                return Ok(tech);

            }
        [Route("api/Teacher/insert")]
        public IHttpActionResult PostTeacher(Teacher_View t)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid Data.");
                using(var db=new TeacherEntities())
                {
                    db.Teacher_Info.Add(new Teacher_Info()
                        {
                            Teacher_Id=t.Id,
                            Teacher_Name=t.Name,
                            Teacher_Email=t.email
                        });
                    db.SaveChanges();
                }
                
                return Ok();
        
            }
         [Route("api/Teacher/update")]
        public IHttpActionResult PutTeacher(Teacher_View t)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data.");
            using(var db= new TeacherEntities())
            {
                var et = db.Teacher_Info.Where(s => s.Teacher_Id == t.Id).FirstOrDefault<Teacher_Info>();

                if(et!=null)
                {
                    et.Teacher_Name = t.Name;
                    et.Teacher_Email = t.email;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        [Route("api/Teacher/delete/{id:int}")]
        public IHttpActionResult Delete(int id)
        { 
            using (var db=new TeacherEntities())
            {
                var teacher = db.Teacher_Info.Where(s => s.Teacher_Id == id).FirstOrDefault();
                db.Entry(teacher).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Ok();
        }
    }


}
