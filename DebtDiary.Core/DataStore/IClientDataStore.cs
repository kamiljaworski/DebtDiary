﻿namespace DebtDiary.Core
{
    public interface IClientDataStore
    {
        bool IsUserLoggedIn { get; }

        User LoggedUser { get; }

        void LoginUser(User user);

        void LogoutUser();
    }
}
