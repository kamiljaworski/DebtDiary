﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DebtDiary.Core
{
    /// <summary>
    /// User
    /// </summary>
    public class User : Person
    {
        public int Id { get; set; }

        [StringLength(80)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [StringLength(80)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        public DateTime? RegisterDate { get; set; }
        public virtual List<Debtor> Debtors { get; set; } = new List<Debtor>();
        public List<Operation> Operations
        {
            get
            {
                List<Operation> operations = new List<Operation>();
                Debtors.ForEach(x => operations.AddRange(x.Operations));
                return operations;
            }
        }

        /// <summary>
        /// Get chart points of aggregated operations grouped by every day started with users register date
        /// </summary>
        public IEnumerable<decimal> GetChartPoints => Operations.GetChartPoints(RegisterDate.Value);
    }
}
