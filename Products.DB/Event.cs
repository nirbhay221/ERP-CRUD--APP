using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.DB
{
    public class Event
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date {  get; set; }
        public string Location { get; set; }
        public ICollection<EventProject> EventProjects { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
