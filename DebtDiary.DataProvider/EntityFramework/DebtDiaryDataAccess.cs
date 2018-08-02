﻿using DebtDiary.Core;
using System;
using System.Linq;

namespace DebtDiary.DataProvider
{
    /// <summary>
    /// Application data access class which use Entity Framework database
    /// </summary>
    public class DebtDiaryDataAccess : IDebtDiaryDataAccess, IDisposable
    {
        /// <summary>
        /// Entity Framework database context
        /// </summary>
        private DebtDiaryDbContext dbContext = new DebtDiaryDbContext();

        /// <summary>
        /// Method that create new user in the database
        /// </summary>
        /// <param name="user">New user to sign up</param>
        public void CreateAccount(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that check if account with this username can be added to the database
        /// </summary>
        /// <param name="username">New user's username</param>
        /// <returns><see cref="bool"/> false if this username exist in the database and true if not</returns>
        public bool IsUsernameAvailable(string username)
        {
            // Check if there is a user with this username in database
            var result = dbContext.Users.Where(x => x.Username == username);

            // If user with this username doesnt exist return true
            if (result == null || result.Count() == 0)
                return true;

            // If yes return false
            return false;
        }

        /// <summary>
        /// Method that check if account with this e-mail can be added to the database
        /// </summary>
        /// <param name="email">New user's e-mail</param>
        /// <returns><see cref="bool"/> false if this e-mail exist in the database and true if not</returns>
        public bool IsEmailAvailable(string email)
        {
            // Check if there is a user with this email in database
            var result = dbContext.Users.Where(x => x.Email == email);

            // If user with this email doesnt exist return true
            if (result == null || result.Count() == 0)
                return true;

            // If yes return false
            return false;
        }

        /// <summary>
        /// IDisposable interface implementation method
        /// </summary>
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}