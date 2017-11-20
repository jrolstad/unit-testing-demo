﻿using System;

namespace myservice.mvc.Application.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string FullName { get; set; }
    }
}