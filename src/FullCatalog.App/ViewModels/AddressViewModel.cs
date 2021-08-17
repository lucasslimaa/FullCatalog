using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FullCatalog.App.ViewModels
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(200, ErrorMessage = "The field {0}  needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string Street { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(200, ErrorMessage = "The field {0}  needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(50, ErrorMessage = "The field {0}  needs to have between {2} and {1} characters", MinimumLength = 1)]
        public string Number { get; set; }
        public string ApartmentNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(8, ErrorMessage = "The field {0}  needs to have {1} characters", MinimumLength = 8)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(200, ErrorMessage = "The field {0}  needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "The field {0} is required ")]
        [StringLength(200, ErrorMessage = "The field {0}  needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string State { get; set; }

        [HiddenInput]
        public Guid SupplierId { get; set; }

    }
}
