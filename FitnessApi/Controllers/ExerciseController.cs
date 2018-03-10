using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FitnessApi.Models;
using FitnessApi.Models.database;
using System.Data.Entity.Migrations;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;

namespace FitnessApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExerciseController : ApiController
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Exercise
        public IEnumerable<Models.database.Exercise> Get()
       {
            return db.Exercises.ToArray();
        }

        // GET: api/Exercise/5
        public Models.database.Exercise Get(Guid id)
        {
            return db.Exercises.Find(id);
        }

        // POST: api/Exercise
        public void Post([FromBody]Exercise exercise)
        {
            if (exercise.Id == Guid.Empty)
            {
                exercise.Id = Guid.NewGuid();
                db.Exercises.Add(exercise);
                db.SaveChanges();
            }
            else
            {
                db.Exercises.AddOrUpdate(exercise);
                db.SaveChanges();
            }
        }

        // PUT: api/Exercise/5
        public void Put([FromBody]Exercise editedExercise)
        {
            db.Exercises.AddOrUpdate(editedExercise);
        }

        // DELETE: api/Exercise/5
        public void Delete(Guid id)
        {
            var ex = db.Exercises.Find(id);
            if (ex != null)
            {
                db.Exercises.Remove(ex);
                db.SaveChanges();
            }
        }
    }
}
