using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignWebApi.Models
{
    public class ITEMViewModel
    {
        public ITEMViewModel()
        {
            PODETAILs = new List<PODETAILViewModel>();
        }
        [Key]
        [Display(Name = "Item Code")]
        [Required]
        [StringLength(4)]
        public string ITCODE { get; set; }

        [Display(Name = "Item Description")]
        [Required]
        [StringLength(15)]
        public string ITDESC { get; set; }
        [Display(Name = "Item Price")]
        [Required]
        public decimal? ITRATE { get; set; }

        public virtual ICollection<PODETAILViewModel> PODETAILs { get; set; }


    }
}
