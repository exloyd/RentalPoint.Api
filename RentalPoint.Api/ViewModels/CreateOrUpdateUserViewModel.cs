using RentalPoint.Data.Enums;

namespace RentalPoint.ViewModels
{
    public class CreateOrUpdateUserViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}