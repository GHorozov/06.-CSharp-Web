namespace StudentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using StudentSystem.Models.Enums;

    public class Resource
    {
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ResourceType ResourceType { get; set; }

        [Required]
        public string Url { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public List<License> Licenses { get; set; } = new List<License>();
    }
}
