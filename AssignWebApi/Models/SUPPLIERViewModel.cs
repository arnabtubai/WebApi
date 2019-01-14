using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignWebApi.Models
{
    public class SUPPLIERViewModel
    {

        public SUPPLIERViewModel()
        {
            POMASTERs = new List<POMASTERViewModel>();
        }
        [Display(Name = "Supplier Code")]
        [Required]
        [StringLength(4)]
        [Key]
        public string SUPLNO { get; set; }
        [Display(Name = "Supplier Name")]
        [Required]
        [StringLength(15)]
        public string SUPLNAME { get; set; }
        [Display(Name = "Supplier Address")]
        [StringLength(40)]
        public string SUPLADDR { get; set; }


        public virtual ICollection<POMASTERViewModel> POMASTERs { get; set; }

    }
}
