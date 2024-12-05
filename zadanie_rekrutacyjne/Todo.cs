using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace zadanie_rekrutacyjne
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueTime { get; set; }
        [Column("iscomplete")]
        public bool isComplete {  get; set; }
        public double percent_done { get; set; }
    }
}