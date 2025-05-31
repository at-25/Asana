using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Models
{
    public class Project
    {
        public List<ToDo> ToDos { get; set; } = new List<ToDo>();
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double CompletePercent => ToDos.Count == 0 ? 0 : 
            ToDos.Count(t => t.IsCompleted == true) * 100.0 / ToDos.Count;

        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description} - {CompletePercent:0}% Complete";
        }
    }
}