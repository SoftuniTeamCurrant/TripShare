namespace TripShare.Web.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class CommentBindingModel
    {
        [Required]
        [MinLength(5)]
        public string Content { get; set; }
    }
}