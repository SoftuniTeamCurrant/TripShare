namespace TripShare.Web.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using TripShare.Models;

    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int TripId { get; set; }

        public DateTime PostedOn { get; set; }

        public static Expression<Func<Comment, CommentViewModel>> Create
        {
            get
            {
                return p => new CommentViewModel()
                {
                    Id = p.Id,
                    Author = p.Author.UserName,
                    Content = p.Content,
                    AuthorId = p.AuthorId,
                    TripId = p.TripId,
                    PostedOn = p.PostedOn
                };
            }
        }
    }
}