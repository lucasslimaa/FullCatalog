using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FullCatalog.App.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(200, ErrorMessage = "The field {0}  needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(14, ErrorMessage = "The field {0}  needs to have between {2} and {1} characters", MinimumLength = 11)]
        public string DocumentNumber { get; set; }

        [DisplayName("Type")]
        public int SupplierType { get; set; }
        public AddressViewModel Address { get; set; }

        [DisplayName("IsActive?")]
        public bool IsActive { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
