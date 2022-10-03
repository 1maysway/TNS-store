namespace TNS_store2.Services
{
    public interface IMailSender
    {
        Task sendAsync(string email, string subject, string message);
    }
}
