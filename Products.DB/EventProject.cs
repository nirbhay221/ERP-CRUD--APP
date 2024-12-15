using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.DB
{
    public class EventProject
    {
        [Key]
        public int Id {  get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }    
        public int EventId {  get; set; }
        public int ProjectId { get; set; }
    }
}
