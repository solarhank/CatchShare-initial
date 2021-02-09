using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchShare.Models
{
    public class Catch
    {
        public int Id { get; set; }
        public string Species { get; set; }
        public int Length { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public Catch()
        {

        }
    }
}
