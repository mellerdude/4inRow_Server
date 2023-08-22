using System.ComponentModel.DataAnnotations;

namespace Final_Project_4inRow_Server.Models
{

    public enum Country
    {
        Canada,
        France,
        Germany,
        Israel,
        UnitedKingdom,
        UnitedStates,
        Yuguslavia
    }

    public class Credentials
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "ERROR! Name must be between 2 and 50 characters.")]
        public string Name { get; set; } = default!;

        [StringLength(4, MinimumLength = 1, ErrorMessage = "ERROR! Password must be between 1 and 4 characters.")]
        public string Password { get; set; } = default!;

        [StringLength(10, MinimumLength = 8, ErrorMessage = "ERROR! Phone number must be between 8 and 10 characters.")]
        public string Phone { get; set; } = default!;
        [Display(Name = "Country")]

        public Country SelectedCountry { get; set; }

    }
}
