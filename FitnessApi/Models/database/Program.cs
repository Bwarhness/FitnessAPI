using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitnessApi.Models.database
{
    /// <summary>
    /// Used for the general program information
    /// </summary>
    public class Program
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public int TimesToRepeat { get; set; }
        public string Description { get; set; }
        public TrainingType TrainingType { get; set; }
        public virtual List<Session> Sessions { get; set; }
    }
    public enum SkillLevel
    {
        Beginner = 0,
        Intermediate = 10,
        Advanced = 20
    }
    public enum TrainingType
    {
        Strength = 0,
        Size = 10,
        Endurance = 20
    }
    /// <summary>
    /// A copy made for the user
    /// </summary>
    public class ProgramInstance
    {

        public Guid Id { get; set; }
        public Guid Fk_Program { get; set; }
        [ForeignKey("Fk_Program")]
        public virtual Program Program{ get; set; }
        public DateTime StartDate { get; set; }
        public string Fk_User { get; set; }
        [ForeignKey("Fk_User")]
        public virtual ApplicationUser User { get; set; }
    }
}