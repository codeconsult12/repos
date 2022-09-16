using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HtmlAgilityPack;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.ExtendedProperties;
using System.Web.Http.Cors;

namespace ExportData.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [System.Web.Http.HttpPost]
        public string Hello()
        {
            return "hello world";
        }
        [System.Web.Http.HttpPost]

        [ValidateInput(false)]
        public string ExportExcel(string table)  //FileContentResult
        {

            List<string> s = new List<string>(table.Split(new string[] { "<caption>" }, StringSplitOptions.None));
            List<string> ss = new List<string>(s[1].Split(new string[] { "</caption>" }, StringSplitOptions.None));
            string mainTable = s[0] + ss[1];

            string CritTable = ss[0];


            using (XLWorkbook woekBook = new XLWorkbook())
            {
                HtmlDocument HDCrit = new HtmlDocument();
                HDCrit.LoadHtml(CritTable);
                if (HDCrit.DocumentNode.SelectSingleNode("//table//tbody//tr[1]//td[2]").InnerText == "Profit and Loss - Company")
                {
                    IXLWorksheet workSheet = woekBook.Worksheets.Add(HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText);

                    workSheet.ShowGridLines = false;
                    int i = 1, j = 1;
                    int length = HDCrit.DocumentNode.SelectNodes("//table//tr").Count();

                    foreach (HtmlNode row in HDCrit.DocumentNode.SelectNodes("//table//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("td"))
                        {
                            if (i == length - 1 || i == length)
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = String.Format("{0:M/d/yyyy}", col.InnerText);
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            else
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            j++;
                        }
                        i++;
                    }

                    workSheet.Cell("B5").Style.NumberFormat.Format = "mm/dd/yyyy";
                    workSheet.Cell("B5").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B5").Value);
                    workSheet.Cell("B6").Style.NumberFormat.Format = "mm/dd/yyyy";
                    workSheet.Cell("B6").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B6").Value);
                    workSheet.Range("A1:B6").Style.Border.OutsideBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Thin);

                    workSheet.Range("A8:C8").Merge();
                    workSheet.Range("A9:C9").Merge();
                    workSheet.Range("A10:C10").Merge();
                    workSheet.Cell("A8").Value = workSheet.Cell("B2").Value;
                    workSheet.Cell("A8").Style.Font.FontSize = 14;
                    workSheet.Cell("A8").Style.Font.Bold = true;
                    workSheet.Cell("A8").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A9").Value = workSheet.Cell("B1").Value;
                    workSheet.Cell("A9").Style.Font.FontSize = 14;
                    workSheet.Cell("A9").Style.Font.Bold = true;
                    workSheet.Cell("A9").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A10").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B5").Value) + " - " + String.Format("{0:M/d/yyyy}", workSheet.Cell("B6").Value);
                    workSheet.Cell("A10").Style.Font.FontSize = 10;
                    workSheet.Cell("A10").Style.Font.Bold = true;
                    workSheet.Cell("A10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    HtmlDocument HDData = new HtmlDocument();
                    HDData.LoadHtml(mainTable);

                    i = 12; j = 1;
                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//thead//tr"))
                    {
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText == "")
                            {
                                workSheet.Cell(i, j).Value = "Uncategorized";
                            }
                            else
                            {
                                // workSheet.Cell(i, j).DataType = XLDataType.Text;
                                workSheet.Cell(i, j).SetValue<string>(Convert.ToString(col.InnerText));
                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                            }
                            j++;
                        }
                        i++;
                    }

                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//tbody//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText.Contains("&amp;"))
                            {
                                workSheet.Cell(i, j).Value = col.InnerText.Replace("&amp;", "&");
                                workSheet.Cell(i, j).DataType = XLDataType.Text;

                            }
                            else
                            {
                                workSheet.Cell(i, j).Value = col.InnerText;
                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                            }
                            j++;
                        }
                        foreach (HtmlNode col in row.SelectNodes("td"))
                        {
                            if (col.InnerText.Contains("("))
                            {
                                string number = col.InnerText.Substring(1, col.InnerText.Length - 3).Trim();
                                workSheet.Cell(i, j).Value = 0 - Convert.ToDecimal(number.Replace(",", ""));
                                workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                            }
                            else
                            {
                                if (col.InnerText.Trim() == "-")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = Convert.ToDecimal(col.InnerText.Replace(",", ""));
                                    workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                                }
                            }
                            j++;
                        }
                        i++;
                    }

                    workSheet.Cell("A" + (i - 3)).Style.Alignment.SetIndent(2);
                    workSheet.Cell("A" + (i - 2)).Style.Alignment.SetIndent(1);
                    workSheet.Row(i - 1).Style.Font.Bold = true;
                    workSheet.Row(i - 2).Style.Font.Bold = true;
                    workSheet.Row(i - 3).Style.Font.Bold = true;

                    var excelTable = workSheet.Range(12, 1, i - 1, j - 1).CreateTable();
                    excelTable.Theme = XLTableTheme.TableStyleLight16;

                    workSheet.Columns().AdjustToContents();  // Adjust column width
                    workSheet.Rows().AdjustToContents();     // Adjust row heights
                    MemoryStream stream = new MemoryStream();

                    woekBook.SaveAs(Server.MapPath("./") + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx");
                    return "/Home/" + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx";
                }
                else if (HDCrit.DocumentNode.SelectSingleNode("//table//tbody//tr[1]//td[2]").InnerText.Trim() == "Profit and Loss - Project")
                {
                    IXLWorksheet workSheet = woekBook.Worksheets.Add(HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText);

                    workSheet.ShowGridLines = false;
                    int i = 1, j = 1;
                    int length = HDCrit.DocumentNode.SelectNodes("//table//tr").Count();

                    foreach (HtmlNode row in HDCrit.DocumentNode.SelectNodes("//table//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("td"))
                        {
                            if (i == length - 1 || i == length)
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = String.Format("{0:M/d/yyyy}", col.InnerText);
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            else
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            j++;
                        }
                        i++;
                    }

                    workSheet.Cell("B6").Style.NumberFormat.Format = "mm/dd/yyyy";
                    workSheet.Cell("B6").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B6").Value);
                    workSheet.Cell("B7").Style.NumberFormat.Format = "mm/dd/yyyy";
                    workSheet.Cell("B7").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B7").Value);

                    workSheet.Range("A1:B7").Style.Border.OutsideBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Thin);

                    workSheet.Range("A9:C9").Merge();
                    workSheet.Range("A10:C10").Merge();
                    workSheet.Range("A11:C11").Merge();
                    workSheet.Cell("A9").Value = workSheet.Cell("B2").Value;
                    workSheet.Cell("A9").Style.Font.FontSize = 14;
                    workSheet.Cell("A9").Style.Font.Bold = true;
                    workSheet.Cell("A9").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A10").Value = workSheet.Cell("B1").Value;
                    workSheet.Cell("A10").Style.Font.FontSize = 14;
                    workSheet.Cell("A10").Style.Font.Bold = true;
                    workSheet.Cell("A10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A11").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B6").Value) + " - " + String.Format("{0:M/d/yyyy}", workSheet.Cell("B7").Value);
                    workSheet.Cell("A11").Style.Font.FontSize = 10;
                    workSheet.Cell("A11").Style.Font.Bold = true;
                    workSheet.Cell("A11").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    HtmlDocument HDData = new HtmlDocument();
                    HDData.LoadHtml(mainTable);

                    i = 13; j = 1;
                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//thead//tr"))
                    {
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText == "")
                            {
                                workSheet.Cell(i, j).Value = "Uncategorized";
                            }
                            else if (col.InnerText.Contains("&amp;"))
                            {
                                workSheet.Cell(i, j).Value = col.InnerText.Replace("&amp;", "&");
                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                            }
                            else if (col.InnerText.Contains("Totals"))
                            {
                                workSheet.Cell(i, j).Value = "Total Amount";
                            }
                            else
                            {
                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                                workSheet.Cell(i, j).SetValue<string>(Convert.ToString(col.InnerText));
                            }
                            j++;
                        }
                        i++;
                    }

                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//tbody//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText.Contains("&amp;"))
                            {
                                workSheet.Cell(i, j).Value = col.InnerText.Replace("&amp;", "&");
                                workSheet.Cell(i, j).DataType = XLDataType.Text;

                            }
                            else
                            {
                                workSheet.Cell(i, j).Value = col.InnerText;
                                workSheet.Cell(i, j).DataType = XLDataType.Text;

                            }
                            j++;
                        }

                        foreach (HtmlNode col in row.SelectNodes("td"))
                        {
                            if (col.InnerText.Contains("("))
                            {
                                string number = col.InnerText.Substring(1, col.InnerText.Length - 2).Replace(" ", ""); ;
                                workSheet.Cell(i, j).Value = 0 - Convert.ToDecimal(number.Replace(",", ""));
                                workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                            }
                            else
                            {
                                if (col.InnerText == "-")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = Convert.ToDecimal(col.InnerText.Replace(",", ""));
                                    workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                                }
                            }
                            j++;
                        }

                        i++;
                    }
                    workSheet.Cell("A" + (i - 3)).Style.Alignment.SetIndent(2);
                    workSheet.Cell("A" + (i - 2)).Style.Alignment.SetIndent(1);
                    workSheet.Row(i - 1).Style.Font.Bold = true;
                    workSheet.Row(i - 2).Style.Font.Bold = true;
                    workSheet.Row(i - 3).Style.Font.Bold = true;

                    var excelTable = workSheet.Range(13, 1, i - 1, j - 1).CreateTable();
                    excelTable.Theme = XLTableTheme.TableStyleLight16;

                    workSheet.Columns().AdjustToContents();  // Adjust column width
                    workSheet.Rows().AdjustToContents();     // Adjust row heights
                    MemoryStream stream = new MemoryStream();

                    woekBook.SaveAs(Server.MapPath("./") + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx");
                    return "/Home/" + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx";
                }
                else if (HDCrit.DocumentNode.SelectSingleNode("//table//tbody//tr[1]//td[2]").InnerText.Trim() == "Balance Sheet")
                {
                    IXLWorksheet workSheet = woekBook.Worksheets.Add(HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText);

                    workSheet.ShowGridLines = false;
                    int i = 1, j = 1;
                    int length = HDCrit.DocumentNode.SelectNodes("//table//tr").Count();
                    foreach (HtmlNode row in HDCrit.DocumentNode.SelectNodes("//table//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("td"))
                        {
                            if (i == length)
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = String.Format("{0:M/d/yyyy}", col.InnerText);
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            else
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            j++;
                        }

                        i++;
                    }
                    workSheet.Cell("B3").Style.NumberFormat.Format = "mm/dd/yyyy";
                    workSheet.Cell("B3").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B3").Value);
                    workSheet.Range("A1:B3").Style.Border.OutsideBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Thin);

                    workSheet.Range("A5:C5").Merge();
                    workSheet.Range("A6:C6").Merge();
                    workSheet.Range("A7:C7").Merge();
                    workSheet.Cell("A5").Value = workSheet.Cell("B2").Value;
                    workSheet.Cell("A5").Style.Font.FontSize = 14;
                    workSheet.Cell("A5").Style.Font.Bold = true;
                    workSheet.Cell("A5").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A6").Value = workSheet.Cell("B1").Value;
                    workSheet.Cell("A6").Style.Font.FontSize = 14;
                    workSheet.Cell("A6").Style.Font.Bold = true;
                    workSheet.Cell("A6").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A7").Value = "As of " + String.Format("{0:M/d/yyyy}", workSheet.Cell("B3").Value);
                    workSheet.Cell("A7").Style.Font.FontSize = 10;
                    workSheet.Cell("A7").Style.Font.Bold = true;
                    workSheet.Cell("A7").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    HtmlDocument HDData = new HtmlDocument();
                    HDData.LoadHtml(mainTable);

                    i = 9; j = 1;
                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//thead//tr"))
                    {
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText == "")
                            {
                                workSheet.Cell(i, j).Value = "Uncategorized";
                            }
                            else
                            {

                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                                workSheet.Cell(i, j).SetValue<string>(Convert.ToString(col.InnerText));
                                workSheet.Cell(i, j).Style.Font.Bold = true;
                            }
                            j++;
                        }
                        i++;
                    }

                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//tbody//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText.Contains("&amp;"))
                            {
                                workSheet.Cell(i, j).Value = col.InnerText.Replace("&amp;", "&");
                                workSheet.Cell(i, j).Style.Font.Bold = true;
                            }
                            else
                            {
                                if (col.InnerText == "Current Assets")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(1);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Total Current Assets")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(1);
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Medium);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Bank Accounts")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(2);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Total Bank Accounts")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(2); workSheet.Cell(i, j).Style.Font.Bold = true;
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Medium);

                                }
                                else if (col.InnerText == "Other Current Assets")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(2); workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Total Other Current Assets")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(2); workSheet.Cell(i, j).Style.Font.Bold = true;
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Medium);
                                }
                                if (col.InnerText == "Liabilities")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(1); workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Total Liabilities")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(1); workSheet.Cell(i, j).Style.Font.Bold = true;
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Medium);
                                }
                                else if (col.InnerText == "Current Liabilities")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(2); workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Total Current Liabilities")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(2); workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Accounts Payable" && j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(3); workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Total Accounts Payable")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(3); workSheet.Cell(i, j).Style.Font.Bold = true;
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Medium);
                                }
                                else if (col.InnerText == "Other Current Liabilities")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(3); workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Total Other Current Liabilities")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(3); workSheet.Cell(i, j).Style.Font.Bold = true;
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Medium);
                                }
                                if (col.InnerText == "Equity")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(1); workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "Total Equity")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(1); workSheet.Cell(i, j).Style.Font.Bold = true;
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Medium);
                                }
                                else if (col.InnerText == "Net Income")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetIndent(2);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else if (col.InnerText == "TOTAL LIABILITIES AND EQUITY")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    // workSheet.Cell(i, j).Style.Alignment.SetIndent(2);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Medium);
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.BottomBorder = XLBorderStyleValues.Thick;// .BorderAround(ExcelBorderStyle.Medium);
                                }
                                else if (col.InnerText == "TOTAL ASSETS")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    // workSheet.Cell(i, j).Style.Alignment.SetIndent(2);
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Medium);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                    workSheet.Range("A" + i + ":c" + i).Style.Border.BottomBorder = XLBorderStyleValues.Thick;// .BorderAround(ExcelBorderStyle.Medium);
                                }
                                else
                                {
                                    int number = 0;
                                    bool isNumber = int.TryParse(col.InnerText, out number);
                                    if (isNumber)
                                    {
                                        if (Convert.ToInt32(col.InnerText) >= 10000 && Convert.ToInt32(col.InnerText) < 11000)
                                        {
                                            workSheet.Cell(i, j).Value = col.InnerText;
                                            workSheet.Cell(i, j).Style.Font.Bold = true;
                                            workSheet.Cell(i, j).Style.Alignment.SetIndent(3);
                                        }
                                        else if (Convert.ToInt32(col.InnerText) >= 11000 && Convert.ToInt32(col.InnerText) < 20000)
                                        {
                                            workSheet.Cell(i, j).Value = col.InnerText;
                                            workSheet.Cell(i, j).Style.Font.Bold = true;
                                            workSheet.Cell(i, j).Style.Alignment.SetIndent(3);
                                        }
                                        else if (Convert.ToInt32(col.InnerText) >= 20000 && Convert.ToInt32(col.InnerText) < 21000)
                                        {
                                            workSheet.Cell(i, j).Value = col.InnerText;
                                            workSheet.Cell(i, j).Style.Font.Bold = true;
                                            workSheet.Cell(i, j).Style.Alignment.SetIndent(4);
                                        }
                                        else if (Convert.ToInt32(col.InnerText) >= 21000 && Convert.ToInt32(col.InnerText) < 30000)
                                        {
                                            workSheet.Cell(i, j).Value = col.InnerText;
                                            workSheet.Cell(i, j).Style.Font.Bold = true;
                                            workSheet.Cell(i, j).Style.Alignment.SetIndent(4);
                                        }
                                        else if (Convert.ToInt32(col.InnerText) >= 30000 && Convert.ToInt32(col.InnerText) < 50000)
                                        {
                                            workSheet.Cell(i, j).Value = col.InnerText;
                                            workSheet.Cell(i, j).Style.Font.Bold = true;
                                            workSheet.Cell(i, j).Style.Alignment.SetIndent(3);
                                        }
                                        else
                                        {
                                            workSheet.Cell(i, j).Value = col.InnerText;
                                            workSheet.Cell(i, j).Style.Font.Bold = true;
                                        }
                                    }
                                    else
                                    {
                                        workSheet.Cell(i, j).Value = col.InnerText;
                                        workSheet.Cell(i, j).Style.Font.Bold = true;
                                    }
                                }
                            }
                            j++;
                        }
                        //                        if (row.SelectNodes("td//a") == null)
                        {
                            foreach (HtmlNode col in row.SelectNodes("td"))
                            {
                                if (col.InnerText.Contains("("))
                                {
                                    string number = col.InnerText.Substring(1, col.InnerText.Length - 1);
                                    workSheet.Cell(i, j).Value = 0 - Convert.ToDecimal(number.Replace(",", ""));
                                    workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                                }
                                else
                                {
                                    if (col.InnerText == "-")
                                    {
                                        workSheet.Cell(i, j).Value = col.InnerText;
                                        workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                    }
                                    else
                                    {
                                        if (col.InnerText != "")
                                        {
                                            if (col.InnerText.Contains("$"))
                                            {
                                                workSheet.Cell(i, j).Value = col.InnerText;
                                                workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                                workSheet.Cell(i, j).Style.NumberFormat.Format = "$ #,##0;$ (#,##0)";
                                            }
                                            else
                                            {
                                                workSheet.Cell(i, j).Value = Convert.ToDecimal(col.InnerText.Replace(",", ""));
                                                workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                                            }
                                        }
                                        else
                                        {
                                            workSheet.Cell(i, j).Value = col.InnerText;
                                            workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                                        }
                                    }
                                }
                                j++;
                            }
                        }
                        //                       else
                        //                       {
                        //                           foreach (HtmlNode col in row.SelectNodes("td//a"))
                        //                           {
                        //                               workSheet.Cell(i, j).Value = Convert.ToInt32(col.InnerText.Replace(",",""));
                        //                               j++;
                        //                           }
                        //                       }
                        i++;
                    }
                    var excelTable = workSheet.Range(9, 1, i - 1, j - 1).CreateTable();
                    excelTable.Theme = XLTableTheme.TableStyleLight16;

                    workSheet.Columns().AdjustToContents();  // Adjust column width
                    workSheet.Rows().AdjustToContents();     // Adjust row heights
                    MemoryStream stream = new MemoryStream();

                    woekBook.SaveAs(Server.MapPath("./") + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx");
                    return "/Home/" + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx";
                }
                else if (HDCrit.DocumentNode.SelectSingleNode("//table//tbody//tr[1]//td[2]").InnerText.Trim() == "Account summary by vendor")
                {
                    IXLWorksheet workSheet = woekBook.Worksheets.Add(HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText);

                    workSheet.ShowGridLines = false;
                    int i = 1, j = 1;

                    int length = HDCrit.DocumentNode.SelectNodes("//table//tr").Count();

                    foreach (HtmlNode row in HDCrit.DocumentNode.SelectNodes("//table//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("td"))
                        {
                            if (i == length - 1 || i == length)
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = String.Format("{0:M/d/yyyy}", col.InnerText);
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            else
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            j++;
                        }
                        i++;
                    }

                    workSheet.Cell("B7").Style.NumberFormat.Format = "mm/dd/yyyy";
                    workSheet.Cell("B7").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B7").Value);
                    workSheet.Cell("B8").Style.NumberFormat.Format = "mm/dd/yyyy";
                    workSheet.Cell("B8").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B8").Value);

                    workSheet.Range("A1:B8").Style.Border.OutsideBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Thin);

                    workSheet.Range("A10:C10").Merge();
                    workSheet.Range("A11:C11").Merge();
                    workSheet.Range("A12:C12").Merge();
                    workSheet.Cell("A10").Value = workSheet.Cell("B2").Value;
                    workSheet.Cell("A10").Style.Font.FontSize = 14;
                    workSheet.Cell("A10").Style.Font.Bold = true;
                    workSheet.Cell("A10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A11").Value = workSheet.Cell("B1").Value;
                    workSheet.Cell("A11").Style.Font.FontSize = 14;
                    workSheet.Cell("A11").Style.Font.Bold = true;
                    workSheet.Cell("A11").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A12").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B7").Value) + " - " + String.Format("{0:M/d/yyyy}", workSheet.Cell("B8").Value);
                    workSheet.Cell("A12").Style.Font.FontSize = 10;
                    workSheet.Cell("A12").Style.Font.Bold = true;
                    workSheet.Cell("A12").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    HtmlDocument HDData = new HtmlDocument();
                    HDData.LoadHtml(mainTable);

                    i = 14; j = 1;
                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//thead//tr"))
                    {
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText == "")
                            {
                                workSheet.Cell(i, j).Value = "Uncategorized";
                            }
                            else
                            {

                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                                workSheet.Cell(i, j).SetValue<string>(Convert.ToString(col.InnerText));
                            }
                            j++;
                        }
                        i++;
                    }
                    int cnt = 0;
                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//tbody//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText.Contains("&amp;"))
                            {
                                workSheet.Cell(i, j).Value = col.InnerText.Replace("&amp;", "&");
                                workSheet.Cell(i, j).DataType = XLDataType.Text;

                            }
                            else
                            {
                                workSheet.Cell(i, j).Value = col.InnerText;
                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                            }
                            j++;
                        }
                        foreach (HtmlNode col in row.SelectNodes("td"))
                        {
                            if (col.InnerText.Contains("("))
                            {
                                string number = col.InnerText.Substring(1, col.InnerText.Length - 1);
                                workSheet.Cell(i, j).Value = 0 - Convert.ToDecimal(number.Replace(",", ""));
                                workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                            }
                            else
                            {
                                if (col.InnerText.Trim() == "-")
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = Convert.ToDecimal(col.InnerText.Replace(",", ""));
                                    workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                                }
                            }
                            j++;
                        }
                        if (!workSheet.Cell(i, 1).IsEmpty() && !workSheet.Cell(i, 2).IsEmpty())
                        {
                            workSheet.Range(i, 1, i, j - 1).Style.Border.TopBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Thin);
                            workSheet.Range(i, 1, i, j - 1).Style.Font.Bold = true;
                        }
                        if (!workSheet.Cell(i, 1).IsEmpty() && !workSheet.Cell(i, 2).IsEmpty())
                        {
                            if (cnt > 0)
                            {
                                workSheet.Rows(i - cnt, i - 1).Group();
                                workSheet.Rows(i - cnt, i - 1).Collapse();
                                cnt = 0;
                            }
                        }
                        else
                        {
                            cnt++;
                        }

                        i++;
                    }
                    if (cnt > 0)
                    {
                        workSheet.Rows(i - cnt, i - 2).Group();
                        workSheet.Rows(i - cnt, i - 2).Collapse();
                    }
                    workSheet.Row(i - 1).Style.Font.Bold = true;
                    workSheet.Cell(i - 1, 1).Value = "Total Expenses";

                    var excelTable = workSheet.Range(14, 1, i - 1, j - 1).CreateTable();
                    excelTable.Theme = XLTableTheme.TableStyleLight16;

                    workSheet.Columns().AdjustToContents();  // Adjust column width
                    workSheet.Rows().AdjustToContents();     // Adjust row heights
                    MemoryStream stream = new MemoryStream();

                    woekBook.SaveAs(Server.MapPath("./") + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx");
                    return "/Home/" + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx";
                }
                else if (HDCrit.DocumentNode.SelectSingleNode("//table//tbody//tr[1]//td[2]").InnerText.Trim() == "Vendor Expense Summary")
                {
                    IXLWorksheet workSheet = woekBook.Worksheets.Add(HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText);

                    workSheet.ShowGridLines = false;
                    int i = 1, j = 1;
                    int length = HDCrit.DocumentNode.SelectNodes("//table//tr").Count();

                    foreach (HtmlNode row in HDCrit.DocumentNode.SelectNodes("//table//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("td"))
                        {
                            if (i == length - 1 || i == length)
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = String.Format("{0:M/d/yyyy}", col.InnerText);
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            else
                            {
                                if (j == 1)
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                    workSheet.Cell(i, j).Style.Font.Bold = true;
                                }
                                else
                                {
                                    workSheet.Cell(i, j).Value = col.InnerText;
                                    workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                }
                            }
                            j++;
                        }
                        i++;
                    }

                    workSheet.Cell("B6").Style.NumberFormat.Format = "mm/dd/yyyy";
                    workSheet.Cell("B6").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B6").Value);
                    workSheet.Cell("B7").Style.NumberFormat.Format = "mm/dd/yyyy";
                    workSheet.Cell("B7").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B7").Value);
                    workSheet.Range("A1:B7").Style.Border.OutsideBorder = XLBorderStyleValues.Medium;// .BorderAround(ExcelBorderStyle.Thin);

                    workSheet.Range("A9:C9").Merge();
                    workSheet.Range("A10:C10").Merge();
                    workSheet.Range("A11:C11").Merge();
                    workSheet.Cell("A9").Value = workSheet.Cell("B2").Value;
                    workSheet.Cell("A9").Style.Font.FontSize = 14;
                    workSheet.Cell("A9").Style.Font.Bold = true;
                    workSheet.Cell("A9").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A10").Value = workSheet.Cell("B1").Value;
                    workSheet.Cell("A10").Style.Font.FontSize = 14;
                    workSheet.Cell("A10").Style.Font.Bold = true;
                    workSheet.Cell("A10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    workSheet.Cell("A11").Value = String.Format("{0:M/d/yyyy}", workSheet.Cell("B6").Value) + " - " + String.Format("{0:M/d/yyyy}", workSheet.Cell("B7").Value);
                    workSheet.Cell("A11").Style.Font.FontSize = 10;
                    workSheet.Cell("A11").Style.Font.Bold = true;
                    workSheet.Cell("A11").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    HtmlDocument HDData = new HtmlDocument();
                    HDData.LoadHtml(mainTable);

                    i = 13; j = 1;
                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//thead//tr"))
                    {
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText == "")
                            {
                                workSheet.Cell(i, j).Value = "Uncategorized";
                            }
                            else
                            {
                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                                workSheet.Cell(i, j).SetValue<string>(Convert.ToString(col.InnerText));
                            }
                            j++;
                        }
                        i++;
                    }

                    foreach (HtmlNode row in HDData.DocumentNode.SelectNodes("//table//tbody//tr"))
                    {
                        j = 1;
                        foreach (HtmlNode col in row.SelectNodes("th"))
                        {
                            if (col.InnerText.Contains("&amp;"))
                            {
                                workSheet.Cell(i, j).Value = col.InnerText.Replace("&amp;", "&");
                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                            }
                            else
                            {
                                workSheet.Cell(i, j).Value = col.InnerText;
                                workSheet.Cell(i, j).DataType = XLDataType.Text;
                            }
                            j++;
                        }
                        //                        if (row.SelectNodes("td//a") == null)
                        {
                            foreach (HtmlNode col in row.SelectNodes("td"))
                            {
                                if (col.InnerText.Contains("("))
                                {
                                    string number = col.InnerText.Substring(1, col.InnerText.Length - 1);
                                    workSheet.Cell(i, j).Value = 0 - Convert.ToDecimal(number.Replace(",", ""));
                                    workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                                }
                                else
                                {
                                    if (col.InnerText != "0")
                                    {
                                        if (col.InnerText == "-")
                                        {
                                            workSheet.Cell(i, j).Value = col.InnerText;
                                            workSheet.Cell(i, j).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                        }
                                        else
                                        {
                                            workSheet.Cell(i, j).Value = Convert.ToDecimal(col.InnerText.Replace(",", ""));
                                            workSheet.Cell(i, j).Style.NumberFormat.Format = "#,##0.00;(#,##0.00)";
                                        }
                                    }
                                }
                                j++;
                            }
                        }
                        //                       else
                        //                       {
                        //                           foreach (HtmlNode col in row.SelectNodes("td//a"))
                        //                           {
                        //                               workSheet.Cell(i, j).Value = Convert.ToInt32(col.InnerText.Replace(",",""));
                        //                               j++;
                        //                           }
                        //                       }
                        i++;
                    }
                    workSheet.Row(i - 1).Style.Font.Bold = true;
                    workSheet.Cell(i - 1, 1).Value = "Total Expenses";

                    var excelTable = workSheet.Range(13, 1, i - 1, j - 1).CreateTable();
                    excelTable.Theme = XLTableTheme.TableStyleLight16;

                    workSheet.Columns().AdjustToContents();  // Adjust column width
                    workSheet.Rows().AdjustToContents();     // Adjust row heights

                    woekBook.SaveAs(Server.MapPath("./") + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx");
                    return "/Home/" + HDCrit.DocumentNode.SelectSingleNode("//table//tr//td[2]").InnerText + ".xlsx";
                }
                return null;
            }






            /*
                woekBook.Worksheets.Add(dtProduct);
                using (MemoryStream stream = new MemoryStream())
                {
                    woekBook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductDetails.xlsx");
                }
            }*/
        }

    }
}