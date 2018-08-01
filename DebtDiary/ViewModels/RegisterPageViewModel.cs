﻿using DebtDiary.Core;
using DebtDiary.DataProvider;
using System;
using System.Security;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// Register Page View Model
    /// </summary>
    class RegisterPageViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// First name of a new user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of a new user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Username of a new user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// E-mail of a new user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gender of a new user
        /// </summary>
        public Gender Gender { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command that change current page to Login Page
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// Command that signs the user up
        /// </summary>
        public ICommand SignUpCommand { get; set; }
        #endregion

        #region Default Constructor
        public RegisterPageViewModel()
        {
            // Create commands
            LoginCommand = new RelayCommand(GoToLoginPage);
            SignUpCommand = new RelayParameterizedCommand((parameter) => SignUp(parameter));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method that change current page to Login Page
        /// </summary>
        private void GoToLoginPage()
        {
            IocContainer.Get<ApplicationViewModel>().GoToPageAsync(ApplicationPage.LoginPage);
        }

        /// <summary>
        /// Method that signs the user in
        /// </summary>
        /// <param name="parameter">Parameter of a RelayParameterizedCommand</param>
        private void SignUp(object parameter)
        {
            string password1 = (parameter as IHaveTwoPasswords)?.Password.GetPassword();
            string password2 = (parameter as IHaveTwoPasswords)?.SecondPassword.GetEncryptedPassword();


            User user = new User
            {
                Username = this.Username,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Password = password2,
                Gender = this.Gender,
                RegisterDate = DateTime.Now
            };

            IocContainer.Get<IDebtDiaryDataAccess>().RegisterUser(user);
        }
        #endregion
    }
}
