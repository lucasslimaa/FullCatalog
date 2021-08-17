using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FullCatalog.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(200, ErrorMessage = "The field {0}  needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(1000, ErrorMessage = "The field {0}  needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string description { get; set; }

        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreateDateTime { get; set; }

        [DisplayName("IsActive?")]
        public bool IsActive { get; set; }
        public SupplierViewModel Supplier { get; set; }
    }
}
