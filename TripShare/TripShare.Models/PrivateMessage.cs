namespace TripShare.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PrivateMessage
    {
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        [Required]
        public DateTime? SendDate { get; set; }
    }
}
