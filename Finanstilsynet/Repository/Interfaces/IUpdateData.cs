using Models;

namespace Repository.Interfaces
{
    public interface IUpdateData
    {
        Task UpdateArticleAsync(Article article);
        Task UpdateLaptopAsync(Laptop laptop);
        Task UpdatePcAsync(Pc pc);
        Task UpdatePrinterAsync(Printer printer);
    }
}
