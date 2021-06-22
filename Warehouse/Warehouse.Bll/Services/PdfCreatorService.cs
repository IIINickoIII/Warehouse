using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.IO;
using System.Text;
using Warehouse.Bll.Dtos;
using Warehouse.Bll.Interfaces;

namespace Warehouse.Bll.Services
{
    public class PdfCreatorService : IPdfCreatorService
    {
        public MemoryStream CreateStream(string pdfFileContent)
        {
            var document = new PdfDocument();
            var graphics = document.Pages.Add().Graphics;
            var font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            graphics.DrawString(pdfFileContent, font, PdfBrushes.Black, new PointF(0, 0));

            var stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            return stream;
        }

        public string GenerateInvoiceContentPdf(OrderDto order)
        {
            var documentBody = new StringBuilder();
            documentBody.AppendLine("INVOICE");
            documentBody.AppendLine($"Invoice Number {Guid.NewGuid()}");
            documentBody.AppendLine("Mr./Ms. Default User Name");
            documentBody.AppendLine($"Order: {order.Id}");
            documentBody.AppendLine();
            documentBody.AppendLine("Your Order");
            var itemNumber = 1;
            foreach (var orderItem in order.OrderItems)
            {
                documentBody.Append($"{itemNumber}.\t");
                documentBody.AppendLine($"{orderItem.Item.Name}\t");
                documentBody.Append($"\t\t\tPrice: {orderItem.Price}$\t");
                documentBody.Append($"Quantity: {orderItem.Quantity}pcs.\t");
                documentBody.Append($"Total: {orderItem.SumWithoutDiscount}$\t");
                documentBody.Append($"Discount: {orderItem.Discount}%\t");
                documentBody.AppendLine($"Final: {orderItem.SumWithDiscount}$\t");
                documentBody.AppendLine();
                itemNumber++;
            }

            documentBody.AppendLine();
            documentBody.AppendLine($"Final: {order.TotalSum}$");
            return documentBody.ToString();
        }
    }
}
