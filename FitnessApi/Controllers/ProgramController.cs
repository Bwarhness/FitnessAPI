using FitnessApi.Models;
using FitnessApi.Models.database;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FitnessApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProgramController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Program
        public IEnumerable<Program> Get()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var res = db.Programs.ToList();
            return db.Programs.ToList();
        }

        // GET: api/Program/5
        public Program Get(int id)
        {
            return db.Programs.Find(id);
        }
        [Route("api/SelectProgram")]
        [HttpPost]
        public void SelectProgram(Guid id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            currentUser.Fk_Program = id;
        }

        [Route("api/GetProgramForUser")]
        [HttpGet]
        [Authorize]
        public Program GetProgramForUser()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            return db.Programs.Where(p => p.Id == currentUser.Fk_Program).FirstOrDefault();
        }

        // POST: api/Program
        public Guid Post([FromBody]Program newProgram)
        {
            var programToSave = new Program()
            {
                Description = newProgram.Description,
                SkillLevel = newProgram.SkillLevel,
                TimesToRepeat = newProgram.TimesToRepeat,
                Title = newProgram.Title,
                TrainingType = newProgram.TrainingType
            };
            if (newProgram.Id == Guid.Empty)
            {
                programToSave.Id = Guid.NewGuid();
            }
            db.Programs.Add(programToSave);
            if (newProgram.Sessions != null)
            {
                int sessionOrder = 0;
                foreach (var session in newProgram.Sessions)
                {
                    var tmpSession = new Session()
                    {
                        Id = Guid.NewGuid(),
                        Order = sessionOrder,
                        Fk_Program = programToSave.Id,
                        Title = session.Title
                    };
                    db.Sessions.Add(tmpSession);

                    sessionOrder++;

                    var exerciseOrder = 0;
                    foreach (var item in session.Exercises)
                    {

                        var plannedExercise = new PlannedExercise()
                        {
                            Id = Guid.NewGuid(),
                            Fk_Session = tmpSession.Id,
                            MaxBreakInSeconds = item.MaxBreakInSeconds,
                            Reps = item.Reps,
                            Sets = item.Sets,
                            Order = exerciseOrder,
                            Tempo = item.Tempo,
                            Fk_Exercise = item.Fk_Exercise
                        };
                        exerciseOrder++;
                        db.PlannedExercises.Add(plannedExercise);

                    }
                }
            }
            
            db.SaveChanges();
            return programToSave.Id;
        }

        // PUT: api/Program/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Program/5
        public void Delete(int id)
        {
        }
    }
}
