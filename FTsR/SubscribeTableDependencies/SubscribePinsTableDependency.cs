﻿using TableDependency.SqlClient;
using FTsR.Models;
using FTsR.Hubs;
using TableDependency.SqlClient.Base.EventArgs;

namespace FTsR.SubscribeTableDependencies
{
    public class SubscribePinsTableDependency
    {
        SqlTableDependency<Pin> tableDependency;
        DataHub dataHub;

        public SubscribePinsTableDependency(DataHub dataHub)
        {
            this.dataHub = dataHub;
        }

        public void SubscribeTableDependency()
        {
            string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=FTsR;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            tableDependency = new SqlTableDependency<Pin>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }
        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Pin> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dataHub.SendPins();
            }
        }
        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Pin)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}
