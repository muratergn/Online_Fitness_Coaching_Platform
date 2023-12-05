using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Gelisme
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int YapanId { get; set; }
        public virtual Kullanici Kullanici { get; set; }

        [Required]
        public float Kilo { get; set; }

        [Required]
        public int Boy { get; set; }

        [Required]
        public float VucutYagOrani { get; set; }

        [Required]
        public float KasKütlesi { get; set; }

        [Required]
        public float VucutKitleIndeksi { get; set; }


    }
}
