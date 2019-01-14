using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssignWebApi.Models
{
    public class PODETAILViewModel
    {
        [Display(Name = "Product Name")]
        [StringLength(4)]
        [Column(Order = 0)]
        [Key]
        public string PONO { get; set; }
        [Display(Name = "Item Name")]
        [StringLength(4)]
        [Column(Order = 1)]
        [Key]
        public string ITCODE { get; set; }
        [Display(Name = "Quantity")]
        public int? QTY { get; set; }

        public virtual ITEMViewModel ITEM { get; set; }

        public virtual POMASTERViewModel POMASTER { get; set; }

    }
}
