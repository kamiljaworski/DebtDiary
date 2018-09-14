﻿using DebtDiary.Core;
using System.Globalization;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// View model for each of debtors list item
    /// </summary>
    public class DebtorsListItemViewModel : BaseViewModel
    {
        private Debtor _debtor = null;

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public decimal Debt { get; set; }
        public AvatarColor AvatarColor { get; set; }

        public ICommand OpenDebtorSubpage { get; set; }

        /// <summary>
        /// Debt showed as local currency pattern
        /// </summary>
        public string FormattedDebt
        {
            get
            {
                var numberFormat = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                numberFormat.CurrencyNegativePattern = 8;
                numberFormat.CurrencyPositivePattern = 3;

                return Debt.ToString("C", numberFormat);
            }
        }

        public DebtorsListItemViewModel()
        {
            OpenDebtorSubpage = new RelayCommand(() =>
            {
                IApplicationViewModel applicationViewModel = IocContainer.Get<IApplicationViewModel>();
                applicationViewModel.SelectedDebtor = _debtor;
                applicationViewModel.ChangeCurrentSubpage(ApplicationSubpage.DebtorInfoSubpage);
            });
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="debtor"><see cref="Debtor"/> you want to make <see cref="DebtorsListItemViewModel"/> from</param>
        public DebtorsListItemViewModel(Debtor debtor) : this()
        {
            _debtor = debtor;
            Id = debtor.Id;
            FullName = debtor.FullName;
            Initials = debtor.Initials;
            Debt = debtor.Debt;
            AvatarColor = debtor.AvatarColor;
        }
    }
}
