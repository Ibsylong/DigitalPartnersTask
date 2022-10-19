using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalPartnersTask.Models
{
    public class Shift
    {
        public int ID { get; set; }

        public int ShiftWorkerID { get; set; }

        public string TasksID { get; set; }
        [Required(ErrorMessage = "Venlig vælg en medarbejde")]
        public string ShiftWorker { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string Tasks { get; set; }

    }
}
