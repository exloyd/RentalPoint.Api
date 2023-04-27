using System.ComponentModel;

namespace RentalPoint.Data.Enums
{
    public enum OrderStatus
    {
        [Description("Создан")]
        Created,
        [Description("Выдан")]
        Issued,
        [Description("Возвращен")]
        Returned
    }
}