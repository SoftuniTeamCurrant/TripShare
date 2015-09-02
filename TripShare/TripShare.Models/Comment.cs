namespace TripShare.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
