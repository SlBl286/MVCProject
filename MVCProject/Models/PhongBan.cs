using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace MVCProject.Models
{
    public class PhongBan
    {
        [Display(Name = "Tên Phòng")]
        [Required(ErrorMessage = "Không được bỏ trống tên")]  
        public string TenPhongBan { get; set; }

        public PhongBan() { }
        public PhongBan(string TenPhongBan)
        {
            this.TenPhongBan = TenPhongBan;
        }
    }
}
