﻿using System;

namespace Recommender.Core.Models.Responses
{
    public class SignUpResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public int Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
