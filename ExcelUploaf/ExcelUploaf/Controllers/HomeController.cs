
using ClosedXML.Excel;
using ExcelUploaf.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExcelUploaf.Controllers
{
    public class HomeController : Controller
    {
        DirectoryEntities db = new DirectoryEntities();
        // GET: InsuranceCertificate  
        public ActionResult Index()
        {
            var Contacts = db.Contacts.ToList();
            return View(Contacts);
        }

        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Contacts");
            dt.Columns.AddRange(new DataColumn[14] { new DataColumn("FirstName"),
                                                     new DataColumn("LastName"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("Title"),
                                                     new DataColumn("Agency"),
                                                     new DataColumn("Sub_Agency"),
                                                     new DataColumn("OfficePhone"),
                                                     new DataColumn("MobilePhone"),
                                                     new DataColumn("Street1"),
            new DataColumn("Street2"),
            new DataColumn("City"),
            new DataColumn("State"),
            new DataColumn("Zip"),
            new DataColumn("Country")});



            var contacts= from contact in db.Contacts select 
                          contact;

            foreach (var contact in contacts)
            {
                dt.Rows.Add(contact.FirstName, contact.LastName, contact.Email, contact.Title,contact.Agency,contact.Sub_Agency, contact.OfficePhone, 
                    contact.MobilePhone, contact.Street1, contact.Street2, contact.City, contact.State, contact.Zip, contact.Country);
            }

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
        }

        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                if (postedFile != null && postedFile.ContentLength > (1024 * 1024 * 50))  // 50MB limit  
                {
                    ModelState.AddModelError("postedFile", "Your file is to large. Maximum size allowed is 50MB !");
                }

                else
                {
                    string filePath = string.Empty;
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls": //For Excel 97-03.  
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //For Excel 07 and above.  
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }

                    try
                    {
                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath);

                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.  
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();

                                    //Read Data from First Sheet.  
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();
                                }
                            }
                        }

                        conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name.  
                                sqlBulkCopy.DestinationTableName = "InsuranceCertificate";
                                con.Open();
                                sqlBulkCopy.WriteToServer(dt);
                                con.Close();
                                return Json("File uploaded successfully");
                            }
                        }
                    }

                    //catch (Exception ex)  
                    //{  
                    //    throw ex;  
                    //}  
                    catch (Exception e)
                    {
                        return Json("error" + e.Message);
                    }
                    //return RedirectToAction("Index");  
                }
            }
            //return View(postedFile);  
            return Json("no files were selected !");
        }

    }
}