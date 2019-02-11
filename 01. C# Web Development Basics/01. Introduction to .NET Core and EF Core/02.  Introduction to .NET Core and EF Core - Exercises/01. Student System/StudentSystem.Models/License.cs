namespace StudentSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class License
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
