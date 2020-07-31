using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LengKeng.Models
{
    public class SinhVien
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [ForeignKey("StudentID")]
        public Student Students { get; set; }
    }
}
