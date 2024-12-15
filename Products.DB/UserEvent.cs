using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.DB
{
    public class UserEvent
    {
        [Key]
        public int Id {  get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        public String Role { get; set; }
        public int UserId {  get; set; }
        public int EventId {  get; set; }
    }
}
