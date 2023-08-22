using System.ComponentModel.DataAnnotations;

namespace Final_Project_4inRow_Server.Models
{
    public class Scores
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int Length { get; set; }

        public string Winner { get; set; } = default!;

        public string GameSequence { get; set; } = default!;

        public DateTime date { get; set; }

    }
}
