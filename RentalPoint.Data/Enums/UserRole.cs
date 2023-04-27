using System.ComponentModel;

namespace RentalPoint.Data.Enums
{
    public enum UserRole
    {
        [Description("Пользователь")]
        User,
        [Description("Администратор")]
        Admin
    }
}