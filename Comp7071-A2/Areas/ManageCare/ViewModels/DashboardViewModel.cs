using Comp7071_A2.Areas.ManageCare.Models;

namespace Comp7071_A2.Areas.ManageCare.ViewModels
{
    public class DashboardViewModel
    {
        public int CustomerCount { get; set; }
        public int EmployeeCount { get; set; }
        public int ServiceCount { get; set; }
        public int ScheduleCount { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<Customer> RecentCustomers { get; set; } = [];
        public List<Service> PopularServices { get; set; } = [];
        public List<Invoice> RecentInvoices { get; set; } = [];
        public List<Schedule> UpcomingSchedules { get; set; } = [];
    }
}