using System;
using Recommender.Core.Enums;

namespace Recommender.Core.Models.Requests
{
    public class SignUpRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhotoUrl { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
