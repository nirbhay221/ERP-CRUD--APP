using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Projects.DB
{
    public class UserProject
    {
        [Key]
        public int Id {  get; set; }

        [ForeignKey("UserId")]
        //public User User { get; set; }

        //[ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public string Role { get; set; }    

        public int UserId { get; set; }
        public int ProjectId {  get; set; }
    }
}
