﻿namespace Ca.Scta.Account
{
    public class UserInfoViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public UserInfoViewModel()
        {

        }

        public UserInfoViewModel(AppUser appUser)
        {
            UserName = appUser.UserName;
            Email = appUser.Email;
            EmailConfirmed = appUser.EmailConfirmed;
        }
    }
}