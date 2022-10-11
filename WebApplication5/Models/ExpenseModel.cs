﻿using System.ComponentModel.DataAnnotations.Schema;

namespace model_handin.Models
{
    public class Expense
    {
        public long ExpenseId { get; private set; }
        public long ModelId { get; set; }
        public long JobId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string? Text { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal amount { get; set; }
    }
}
