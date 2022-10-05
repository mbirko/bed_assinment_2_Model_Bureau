﻿using model_handin.Models;
using System.ComponentModel.DataAnnotations;

namespace model_handin.DTO
{
    public class JobDTO
    {
        public long JobId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public int Days { get; set; }
        [MaxLength(128)]
        public string? Location { get; set; }
        [MaxLength(2000)]
        public string? Comments { get; set; }
    }
}