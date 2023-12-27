﻿using Backend.Controllers;

namespace Backend.Services
{
    public class People2Service : IPeopleService
    {
        public bool Validate(People people)
        {
            if (string.IsNullOrEmpty(people.Name) || people.Name.Length > 100)
            {
                return false;
            }

            return true;
        }
    }
}
