using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotMain.Models
{
    public class PlacementValidationResult
    {
        public bool PlacementSuccessfull { get; set; }
        public string ValidationMessage { get; set; }
        public Placement Placement { get; set; }
    }
}
