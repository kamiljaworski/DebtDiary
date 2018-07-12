﻿using DebtDiary.Core;

namespace DebtDiary.ViewModels.Application
{
    /// <summary>
    /// ApplicationViewModel class storing all aplication state
    /// </summary>
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        #region Private Members
        /// <summary>
        /// Private CurrentPage in the application
        /// </summary>
        private ApplicationPage _currentPage = ApplicationPage.LoginPage;
        #endregion

        #region Public Properties
        /// <summary>
        /// CurrentPage in the application
        /// </summary>
        public ApplicationPage CurrentPage { get => _currentPage; set => _currentPage = value; }
        #endregion
    }
}
