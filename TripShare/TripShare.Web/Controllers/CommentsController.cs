namespace TripShare.Web.Controllers
{
    using System.Web.Http;
    using System.Linq;
    using System;
    using Microsoft.AspNet.Identity;
    using TripShare.Web.Models.ViewModels;
    using TripShare.Web.Models.BindingModels;
    using TripShare.Data;
    using TripShare.Models;

    [Authorize]
    public class CommentsController : ApiController
    {
        public CommentsController(ITripShareData data)
        {
            this.Data = data;
        }

        public ITripShareData Data { get; private set; }

        // GET api/trips/{id}/comments
        [HttpGet]
        [Route("api/trips/{id}/comments")]
        public IHttpActionResult GetComments(int id)
        {
            var trip = this.Data.Trips.Find(id);
            if (trip == null)
            {
                return this.BadRequest("Trip does not excist");
            }

            var comments = trip.Comments.AsQueryable().Select(CommentViewModel.Create);

            return this.Ok(comments);
        }

        // POST api/comments/{id}
        [Route("api/trips/{id}/comments")]
        [HttpPost]
        public IHttpActionResult PostComment(int id, CommentBindingModel model)
        {
            var trip = this.Data.Trips.Find(id);

            if (model == null)
            {
                return this.BadRequest("Model cannot be null (no data in request)");
            } 
            
            if (trip == null)
            {
                return this.BadRequest("Trip does not exist");                
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var comment = new Comment()
            {
                AuthorId = this.User.Identity.GetUserId(),
                Content = model.Content,
                PostedOn = DateTime.Now,
                TripId = id
            };

            trip.Comments.Add(comment);
            this.Data.SaveChanges();

            var data = this.Data.Comments
                .All().Where(t => t.Id == comment.Id)
                .Select(CommentViewModel.Create)
                .FirstOrDefault();

            return this.Ok(data);
        }

        // Delete api/comments/{id}
        [HttpDelete]
        [Route("api/comments/{id}")]
        public IHttpActionResult DeleteComment(int id)
        {
            var comment = this.Data.Comments.Find(id);
            var loggedUserId = this.User.Identity.GetUserId();
            if (comment == null)
            {
                return this.BadRequest("Comment does not exist!");
            }

            if (comment.AuthorId != loggedUserId &&
                comment.Trip.DriverId != loggedUserId)
            {
                return this.Unauthorized();
            }

            this.Data.Comments.Delete(comment);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}