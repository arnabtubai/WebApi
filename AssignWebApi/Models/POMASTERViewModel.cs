using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignWebApi.Models
{
    public class POMASTERViewModel
    {
        public POMASTERViewModel()
        {
            PODETAILs = new List<PODETAILViewModel>();
        }
        [Display(Name = "Product ID")]
        [StringLength(4)]
        [Key]
        public string PONO { get; set; }
        [Display(Name = "Manufacturing Date")]

        public DateTime? PODATE { get; set; }
        [Display(Name = "Supplier")]
        [StringLength(4)]
        public string SUPLNO { get; set; }

        public virtual ICollection<PODETAILViewModel> PODETAILs { get; set; }

        public virtual SUPPLIERViewModel SUPPLIER { get; set; }


    }
}
