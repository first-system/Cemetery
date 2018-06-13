using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cemetery.Models
{
    public class Deceased
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        [Display(Name ="Имя")]
        public string FName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Фамилия")]
        public string LName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Отчество")]
        public string SName { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Дата смерти")]
        public DateTime? DateDeath { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("BurialPlace")]
        public int? BurialPlaseId { get; set; }
        
        public virtual BurialPlace BurialPlace { get; set; }

        [MaxLength(300)]
        [Display(Name ="Ключевые слова")]
        public string Search { get; set; }

        public string Photo { get; set; }

        [Display(Name ="Описание")]
        [MaxLength(200)]
        public string Description { get; set; }

    }
}