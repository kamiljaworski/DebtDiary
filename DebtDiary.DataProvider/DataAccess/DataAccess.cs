﻿using DebtDiary.Core;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DebtDiary.DataProvider
{
    public class DataAccess : IDataAccess, IDisposable
    {
        // TODO: passing dbContext as constructor parameter
        private DebtDiaryDbContext dbContext = new DebtDiaryDbContext();

        /// <summary>
        /// Create new user
        /// </summary>
        public void CreateAccount(User user)
        {
            try
            {
                dbContext.Users.Add(user);
                SaveChanges();
            }
            catch (SqlException exception)
            {
                throw new NoInternetConnectionException("There was a problem with database connection", exception);
            }
        }

        /// <summary>
        /// Check if username is already taken
        /// </summary>
        public bool IsUsernameTaken(string username)
        {
            try
            {
                // Look for user with this username
                User user = dbContext.Users.FirstOrDefault(x => x.Username == username);

                // If there is no user like that return true
                if (user == null)
                    return true;

                // If there is one return false
                return false;

            }
            catch (SqlException exception)
            {
                throw new NoInternetConnectionException("There was a problem with database connection", exception);
            }
        }

        /// <summary>
        /// Check if email is already taken
        /// </summary>
        public bool IsEmailTaken(string email)
        {
            try
            {
                // Look for user with this email
                User user = dbContext.Users.FirstOrDefault(x => x.Email == email);

                // If there is no user like that return true
                if (user == null)
                    return true;

                // If there is one return false
                return false;
            }
            catch (SqlException exception)
            {
                throw new NoInternetConnectionException("There was a problem with database connection", exception);
            }
        }

        /// <summary>
        /// Get the user from database
        /// </summary>
        public User GetUser(string username, string hashedPassword)
        {
            try
            {
                return dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
            }
            catch (SqlException exception)
            {
                throw new NoInternetConnectionException("There was a problem with database connection", exception);
            }
        }

        /// <summary>
        /// Save database changes done in the application runtime
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (SqlException exception)
            {
                throw new NoInternetConnectionException("There was a problem with database connection", exception);
            }
        }

        /// <summary>
        /// Dispose DataAccess object
        /// </summary>
        public void Dispose() => dbContext.Dispose();
    }
}
