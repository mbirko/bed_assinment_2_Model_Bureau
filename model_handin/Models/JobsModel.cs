using System.ComponentModel.DataAnnotations;

namespace model_handin.Models
{
    public class Job
    {
        public Job()
        {
            Models = new List<Model>();
            Expenses = new List<Expense>();
        }
        public long JobId { get; set; }
        [MaxLength(64)]
        public string? Customer { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public int Days { get; set; }
        [MaxLength(128)]
        public string? Location { get; set; }
        [MaxLength(2000)]
        public string? Comments { get; set; }
        public List<Model>? Models { get; set; }
        public List<Expense>? Expenses { get; set; }
    }
}