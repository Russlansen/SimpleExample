namespace SimpleExample.Models
{
    public interface ICustomersContext<T>
    {
        PaginationHandler<T> GetPaginationHandler(PaginationConfig<T> config);
        Customer Get(int id);
        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}