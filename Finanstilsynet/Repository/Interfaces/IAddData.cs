using Controllers;
using Models;

namespace Repository.Interfaces
{
    public interface IAddData
    {
        Task AddArticleAsync(Article article);
        Task AddLaptopAsync(Laptops laptop);
        Task AddLaptopAsync(Laptop laptop);
        Task AddPcAsync(Pc pc);

        Task AddPrinterAsync(Printers printer);
    }
}
