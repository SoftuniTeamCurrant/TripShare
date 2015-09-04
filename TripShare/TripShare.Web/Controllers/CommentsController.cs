namespace TripShare.Web.Controllers
{
    using System.Web.Http;
    using System.Linq;
    using TripShare.Web.Models.ViewModels;
    using TripShare.Data;

    [Authorize]
    public class CommentsController : ApiController
    {
        public CommentsController(ITripShareData data)
        {
            this.Data = data;
        }

        public ITripShareData Data { get; private set; }

        // GET api/comments/{id}
        [HttpGet]
        public IHttpActionResult GetComments(int id)
        {
            var comments = this.Data.Trips.Find(id).Comments.AsQueryable().Select(CommentViewModel.Create);

            return this.Ok(comments);
        }
    }
}