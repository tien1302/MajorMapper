using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TableDependency.SqlClient;
using WebClient.Hubs;

namespace WebClient.SubscribeTableDependencies
{
    public class SubscribeNotificationTableDependency : ISubscribeTableDependency
    {
        MajorMapperContext _dbContext = new MajorMapperContext();
        SqlTableDependency<Notification> tableDependency;
        NotificationHub notificationHub;

        public SubscribeNotificationTableDependency(NotificationHub notificationHub)
        {
            this.notificationHub = notificationHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Notification>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();

        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Notification)} SqlTableDependency error: {e.Error.Message}");
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Notification> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                Notification notification = e.Entity;
                var consultantId = "";
                using (var context = new MajorMapperContext()) 
                {
                    List<Account> account = (from f in context.Accounts
                                    join s in context.Slots on f.Id equals s.ConsultantId
                                    join b in context.Bookings on s.Id equals b.SlotId
                                    join a in context.Notifications on b.Id equals a.BookingId
                                    where a.Id == notification.Id
                                    select f).ToList();
                    consultantId = account.ToList().FirstOrDefault().Id.ToString();
                }
                await notificationHub.SendNotificationToClient(notification.Title,notification.NotificationContent, consultantId);
                await notificationHub.SendNotifications(consultantId);
            }
        }
    }
}
