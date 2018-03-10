using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitnessApi.Models.database
{
    /// <summary>
    /// Basic information about an Exersice
    /// </summary>
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SkillLevel Difficulty { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
    }
    /// <summary>
    /// Consumer part of the exercise, where we fill out how many reps, sets, tempo and so on.
    /// </summary>
    public class PlannedExercise
    {

        public Guid Id { get; set; }
        public int Order { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public string Tempo { get; set; }
        public int MaxBreakInSeconds { get; set; }
        public Guid Fk_Exercise { get; set; }
        [ForeignKey("Fk_Exercise")]
        public virtual Exercise Exercise { get; set; }
        public Guid Fk_Session { get; set; }
        [ForeignKey("Fk_Session")]
        public virtual Session Session { get; set; }
        public virtual List<DoneExercise> DoneExercises { get; set; }
    }
    public class DoneExercise
    {
        public Guid Id { get; set; }
        public bool Failed { get; set; }
        public string Comment { get; set; }
        public int WeightLifted { get; set; }
        public Guid Fk_PlannedExercise { get; set; }
        [ForeignKey("Fk_PlannedExercise")]
        public virtual PlannedExercise PlannedExercise { get; set; }
    }
}