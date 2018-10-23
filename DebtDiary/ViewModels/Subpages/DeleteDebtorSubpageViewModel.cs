﻿using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class DeleteDebtorSubpageViewModel : BaseViewModel
    {
        #region Private Fields

        private User _loggedUser = null;
        private Debtor _selectedDebtor = null;
        private IHavePassword _password = null;
        #endregion

        #region Public Properties

        public Color AvatarColor { get; set; } = Color.Green;
        public string Initials => Helpers.GetInitials(FirstName, LastName);
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleteDebtorRunning { get; set; } = false;

        public FormMessage PasswordMessage { get; set; } = FormMessage.None;

        public ICommand DeleteDebtorCommand { get; set; }
        #endregion

        #region Constructor

        public DeleteDebtorSubpageViewModel()
        {
            _loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;
            _selectedDebtor = IocContainer.Get<IApplicationViewModel>().SelectedDebtor;

            FirstName = _selectedDebtor.FirstName;
            LastName = _selectedDebtor.LastName;
            AvatarColor = _selectedDebtor.AvatarColor;

            DeleteDebtorCommand = new RelayParameterizedCommand(async (parameter) => await DeleteDebtorAsync(parameter));
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Add new debtor to the database and UI
        /// </summary>
        private async Task DeleteDebtorAsync(object parameter)
        {
            await RunCommandAsync(() => IsDeleteDebtorRunning, async () =>
            {
                if (!(parameter is IHavePassword))
                    return;

                _password = (IHavePassword)parameter;

                // Validate entered data
                if (await ValidateDataAsync() == false)
                    return;

                // Add new debtor to ClientDataStore
                _loggedUser.Debtors.Remove(_selectedDebtor);

                // Save changes in the database
                await Task.Run(() => IocContainer.Get<IDataAccess>().SaveChanges());

                // Update list in the ViewModel
                IDebtorsListViewModel debtorsList = IocContainer.Get<IDebtorsListViewModel>();
                debtorsList.Update();

                // Turn off spinning text
                IsDeleteDebtorRunning = false;

                // Show successful dialog window 
                IocContainer.Get<IDialogFacade>().OpenDialog(DialogMessage.DebtorDeleted);

                // Go to summary subpage
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpage(ApplicationSubpage.SummarySubpage);
            });
        }

        /// <summary>
        /// Validate new debtors data
        /// </summary>
        private async Task<bool> ValidateDataAsync()
        {
            await Task.Run(() =>
            {
                // Reset all the form messages properties
                ResetFormMessages();

                // Check if password is empty
                if (_password.Password.IsNullOrEmpty())
                    PasswordMessage = FormMessage.EmptyPassword;

                if (PasswordMessage == FormMessage.None && _password.Password.GetEncryptedPassword() != _loggedUser.Password)
                    PasswordMessage = FormMessage.IncorrectUsername;
            });

            // Check if any problem was found and return right value
            return IsEnteredDataCorrect();
        }

        /// <summary>
        /// Reset all the <see cref="FormMessage"/> properties to <see cref="FormMessage.None"/>
        /// </summary>
        private void ResetFormMessages()
        {
            PasswordMessage = FormMessage.None;
        }

        /// <summary>
        /// Check if all the <see cref="FormMessage"/> properties are set to <see cref="FormMessage.None"/>
        /// </summary>
        private bool IsEnteredDataCorrect()
        {
            // If any of the messages changed it's value return false
            if (PasswordMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }
        #endregion
    }
}