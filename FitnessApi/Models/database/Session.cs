using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitnessApi.Models.database
{
    public class Session
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid Fk_Program  { get; set; }
        public int Order { get; set; }
        [ForeignKey("Fk_Program")]
        public Program Program { get; set; }
        public virtual List<PlannedExercise> Exercises { get; set; }
    }
}