using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunctAppKoke.Models
{
    public class Jobsku
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(24)]
        public string Jobskunumber { get; set; } = string.Empty;
        [Required]
        public float CasesPerUnit { get; set; }
        [Required]
        public string Flavor { get; set; } = string.Empty;
        [Required]
        public int PrimaryPackageSize { get; set; }
        [Required]
        public string PrimaryPackageMaterial { get; set; } = string.Empty;
        [Required]
        public int SecondaryPackageSize { get; set; }

        [Required]
        public string SecondaryPackageMaterial { get; set; } = string.Empty;
        [Required]
        public bool IsPungent { get; set; }
        [Required]
        public bool IsSensitive { get; set; }
        public bool IspPrimaryPackageRecyclable { get; set; } = true;

    }
}
