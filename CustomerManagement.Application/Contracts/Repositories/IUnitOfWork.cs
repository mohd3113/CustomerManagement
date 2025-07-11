namespace CustomerManagement.Application.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICommunicationTemplateRepository CommunicationTemplateRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        Task SaveChanges();
    }
}