﻿using System.ComponentModel.DataAnnotations;

namespace DebtDiary.Core
{
    public class Operation
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public Debtor Debtor { get; set; }

        [StringLength(140)]
        public string Description { get; set; }
    }
}