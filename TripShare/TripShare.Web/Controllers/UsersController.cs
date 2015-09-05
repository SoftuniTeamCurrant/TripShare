namespace TripShare.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Models.BindingModels;
    using TripShare.Web.Models.ViewModels;

    public class UsersController : BaseApiController
    {
        //GET api/users
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var usersCollection = this.Data.Users.All().Select(UserViewModel.Create);

            return this.Ok(usersCollection);
        }

        //GET api/users/{userId}/trips
        [Route("api/users/{username}/trips")]
        [HttpGet]
        public IHttpActionResult GetUserTrips(string username)
        {
            var user = this.Data.Users.All()
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return this.BadRequest();
            }

            var userTrips = this.Data.Trips.All()
                .Where(t => t.DriverId == user.Id)
                .OrderBy(t => t.DepartureDate)
                .Select(TripViewModel.Create);


            return this.Ok(userTrips);
        }

        //GET api/users/search?Username
        [Route("api/users/search")]
        [HttpGet]
        [ActionName("search")]
        public IHttpActionResult SearchUser(
            [FromUri]UserSearchBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Binding model cannot be null");
            }

            if (model.Username == null)
            {
                return this.NotFound();
            }

            var user = this.Data.Users.All()
                .Where(u => u.UserName == model.Username)
                .Select(UserViewModel.Create)
                .FirstOrDefault();

            return this.Ok(user);
        }

    }
}
