using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using GameStore.Models.Repositories;
using GameStore.Models.Grid;
using GameStore.Models.DTOs;
using GameStore.Models.Query;
using GameStore.Models.ViewModels;
using GameStore.Models.ExtensionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GameStore.Data;
using GameStore.Models.ViewModels.Reports;
using GameStore.Data.UtilityClasses;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using Syncfusion.Pdf.Grid;

namespace GameStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminReportController : Controller
    {

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        public AdminReportController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            SelectValueViewModel vm = new SelectValueViewModel();
            vm.SelectValue = "customer";

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public ActionResult GenerateReport(string id)
        {
            //Generate a new PDF document.
            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            IEnumerable<object> dataTable = _storeContext.Customers.ToList();
            string fileName = "CustomerReport.pdf";

            if (id == "customer")
            {
                List<CustomerReportViewModel> customerList = new List<CustomerReportViewModel>();

                foreach(Customer item in dataTable)
                {
                    string fullName = "";
                    if(!String.IsNullOrEmpty(item.FirstName))
                    {
                        fullName = item.FirstName;
                    }
                    if(!String.IsNullOrEmpty(item.LastName))
                    {
                        fullName += " " + item.LastName;
                    }
                    customerList.Add(new CustomerReportViewModel(item.Nickname, item.Email, fullName));
                }
                dataTable = customerList;
                fileName = "MemberListReport.pdf";
            }
            else if (id == "game")
            {
                List<GameReportViewModel> gameList = new List<GameReportViewModel>();
                dataTable = _storeContext.Games.ToList();

                foreach (Game item in dataTable)
                {
                    gameList.Add(new GameReportViewModel(item.Name, (double)item.Price));
                }
                dataTable = gameList;
                fileName = "GameListReport.pdf";
            }



            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 10));
            //Write the PDF document to stream
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            doc.Close(true);
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);
        }
    }
}
