namespace model_handin.Hubs;

public interface IExpenseNotification
{
    Task Notification(string message);
}