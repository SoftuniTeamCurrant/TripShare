namespace TripShare.Web.Models.ViewModels
{
    using System.Web.Http;
    using TripShare.Models;
    using System;
    using System.Linq.Expressions;
    using System.Collections;
    using System.Collections.Generic;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool isDriver { get; set; }

        public static Expression<Func<User, UserViewModel>> Create
        {
            get
            {
                return p => new UserViewModel()
                {
                    Id = p.Id,
                    UserName = p.UserName,
                    Email = p.Email,
                    isDriver = p.isDriver,

                };
            }
        }
    }
}