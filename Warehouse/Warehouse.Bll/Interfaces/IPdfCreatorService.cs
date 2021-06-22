using System.IO;
using Warehouse.Bll.Dtos;

namespace Warehouse.Bll.Interfaces
{
    public interface IPdfCreatorService
    {
        MemoryStream CreateStream(string pdfFileContent);

        string GenerateInvoiceContentPdf(OrderDto order);
    }
}
