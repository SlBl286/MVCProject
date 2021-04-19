using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace MVCProject.Models
{
    [Table("phong_ban")]
    public partial class PhongBan : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int id {get;}
        [Display(Name = "Tên Phòng")]
        [Required(ErrorMessage = "Không được bỏ trống tên")]  
        [Column("tenphongban")]
        public virtual string TenPhongBan { get; set; }

        public PhongBan() { }
        public PhongBan(string TenPhongBan)
        {
            this.TenPhongBan = TenPhongBan;
        }
    }
    
}
