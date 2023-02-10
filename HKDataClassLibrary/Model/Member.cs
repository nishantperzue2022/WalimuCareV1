using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HKDataClassLibrary.Model
{
    [Table("Members")]
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int member_id { get; set; }
        public string phone_no { get; set; }
        public int national_id { get; set; }
        public int pin { get; set; }
        public bool OTP_send { get; set; }

        //public ICollection<> someforeighKeyRelation { get; set; }

        //[ForeignKey(nameof(Owner))] one to one relation
        //public ing MbrId { get; set; }
        //public Member Member { get; set; }
    }
}
