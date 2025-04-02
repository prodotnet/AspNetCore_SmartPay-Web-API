﻿using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore_SmartPay_Web_API.Models
{
    public class User:IdentityUser
    {

        [Required]
        public string FirstName {  get; set; }


        [Required]
        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
