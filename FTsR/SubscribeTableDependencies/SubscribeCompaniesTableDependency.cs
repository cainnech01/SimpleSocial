using TableDependency.SqlClient;
using FTsR.Models;
using FTsR.Hubs;
using TableDependency.SqlClient.Base.EventArgs;

namespace FTsR.SubscribeTableDependencies
{
    public class SubscribeCompaniesTableDependency
    {
        SqlTableDependency<Companies> tableDependency;
        DashboardHub dashboardHub;

        public SubscribeCompaniesTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }

        public void SubscribeTableDependency()
        {
            string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=FTsR;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            tableDependency = new SqlTableDependency<Companies>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }
        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Companies> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendCompanies();
            }
        }
        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Companies)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}
