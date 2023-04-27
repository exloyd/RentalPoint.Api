using System.ComponentModel;

namespace RentalPoint.Data.Models
{
    public class StatisticsReport
    {
        public decimal PreviousMonth { get; set; }
        public decimal CurrentMonth { get; set; }
        public int Total { get; set; }
        public string Type { get; set; }
    }

    public enum StatisticsReportType
    {
        [Description("Заказы")]
        Orders,
        [Description("Пользователи")]
        Users,
        [Description("Акции")]
        Promos,
        [Description("Штрафы")]
        Penalties
    }
}