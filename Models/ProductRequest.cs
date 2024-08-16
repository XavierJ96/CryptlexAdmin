using System.ComponentModel.DataAnnotations;

namespace CryplexAdmin.Models
{
    public class ProductRequest
    {
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Description { get; set; }


        [Required]
        public string LicenseTemplateId { get; set; }

    }
}

