using Application.Contracts.Infrastructure;
using Application.Features.FeatureTransaccion.Dto;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.Services
{
    public class ReportService : IReportService
    {
        public byte[] GenerarEstadoDeCuentaPDF(EstadoCuentaDto data)
        {
            var documentPDF = Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(pageSize: PageSizes.A4);
                    pagina.Margin(1, Unit.Centimetre);
                    pagina.DefaultTextStyle(x => x.FontSize(12).Light().FontFamily(Fonts.Arial));

                    pagina.Header().Text("Estado de cuenta de tarjeta")
                    .Bold().FontSize(24).FontColor(Colors.Black).AlignCenter();

                    pagina.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Row(row =>
                        {
                            row.Spacing(10);

                            row.RelativeItem().AlignLeft().Column(col1 =>
                            {
                                col1.Item().Text($"Titular: {data.Titular}").SemiBold();
                                col1.Item().Text($"Número: {data.Numero}").SemiBold();
                                col1.Item().Text($"Fecha: {data.FechaVencimiento.ToShortDateString()}").SemiBold();
                            });

                            row.RelativeItem().AlignRight().Column(col2 =>
                            {
                                col2.Item().Text($"Pago minimo: {data.PorcentajePagoMinimo.ToString()}%").SemiBold();
                                col2.Item().Text($"Interes: {data.PorcentajeInteres.ToString()}%").SemiBold();
                            });
                        });

                        col.Item()
                        .Container()
                        .PaddingVertical(10)
                        .Border(2)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(col =>
                            {
                                col.RelativeColumn();
                                col.RelativeColumn();
                                col.RelativeColumn();
                                col.RelativeColumn();
                            });

                            table.ExtendLastCellsToTableBottom();

                            table.Header(head =>
                            {
                                head.Cell().Border(2).Padding(4).Text("FECHA").SemiBold();
                                head.Cell().Border(2).Padding(4).Text("TIPO").SemiBold();
                                head.Cell().Border(2).Padding(4).AlignRight().Text("MONTO").SemiBold();
                                head.Cell().Border(2).Padding(4).AlignRight().Text("DESCRIPCION").SemiBold();
                            });
                            foreach (var item in data.ListaComprasMes)
                            {
                                table.Cell().BorderBottom(1).Padding(4).Text(item.Fecha.ToString("dd/MM/yyyy HH:mm:ss"));
                                table.Cell().BorderBottom(1).Padding(4).Text(item.Tipo);
                                table.Cell().BorderBottom(1).Padding(4).AlignRight().Text($"$ {Math.Round(item.Monto, 2)}");
                                table.Cell().BorderBottom(1).Padding(4).Text(item.Descripcion);
                            }

                            table.Cell().ColumnSpan(4).AlignRight().Border(1).Padding(4).Text($"Total $ {Math.Round(data.SaldoTotalComprasMes, 2)}").SemiBold();
                        });

                        col.Item()
                        .Text($"Interes bonificable: ${data.InteresBonificable}");
                        col.Item()
                        .Text($"Cuota minima: ${data.CuotaMinima}");
                        col.Item()
                        .Text($"Total de contado a pagar: ${data.TotalContadoAPagar}");
                        col.Item()
                        .Text($"Total de contado + interes bonificable: ${data.TotalContadoMasInteresBonificable}");

                        
                    });
                });
            }).GeneratePdf();
            return documentPDF;
        }

        public byte[] GenerarReporteExcel(List<TransaccionDto> data)
        {
            using (var libro = new XLWorkbook())
            {
                var worksheet = libro.Worksheets.Add("ReporteExcel");
                #region Headers
                worksheet.Cell(1, 1).Value = "Fecha";
                worksheet.Cell(1, 2).Value = "Tipo";
                worksheet.Cell(1, 3).Value = "Monto";
                worksheet.Cell(1, 4).Value = "Descripción";
                #endregion
                // Estilo encabezados
                var headersRange = worksheet.Range("1:1");
                headersRange.Style.Font.SetBold();
                headersRange.Style.Fill.BackgroundColor = XLColor.LightSkyBlue;

                int rowNumber = 2;
                foreach (var item in data)
                {
                    #region Value rows
                    worksheet.Cell(rowNumber, 1).Value = item.Fecha.ToString();
                    worksheet.Cell(rowNumber, 2).Value = item.Tipo;
                    worksheet.Cell(rowNumber, 3).Value = item.Monto;
                    worksheet.Cell(rowNumber, 3).Style.NumberFormat.Format = "_($* #,##0.00_);_($* (#,##0.00);_($* \"\" - _);_(@_)";
                    worksheet.Cell(rowNumber, 4).Value = item.Descripcion;
                    #endregion
                    rowNumber++;
                }
                worksheet.ColumnsUsed().AdjustToContents();
                using (var memoStream = new MemoryStream())
                {
                    libro.SaveAs(memoStream);
                    return memoStream.ToArray();
                }
            }
        }
    }
}
