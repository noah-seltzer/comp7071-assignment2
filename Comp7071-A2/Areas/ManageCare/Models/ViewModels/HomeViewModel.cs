using System;
using System.Collections.Generic;

namespace Comp7071_A2.Areas.ManageCare.Models.ViewModels
{
    public class HomeViewModel
    {
        public int TotalEmployees { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalServices { get; set; }
        public int UpcomingSchedules { get; set; }
        public decimal MonthlyRevenue { get; set; }
        
        public List<Schedule> RecentSchedules { get; set; } = new();
    }
} 