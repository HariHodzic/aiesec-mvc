using System.ComponentModel.DataAnnotations;

namespace Aiesec.Data.Model.BusinessModel
{
    public class City
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Postcode { get; set; }
    }
}
