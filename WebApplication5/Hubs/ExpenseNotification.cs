using Microsoft.AspNetCore.SignalR;

namespace model_handin.Hubs;

public class ExpenseNotification : Hub<IExpenseNotification>
{
    public async Task Notification(string message)
    {
        await Clients.All.Notification(message);
    }
}