using CreditCardExpenseControl.API.Models;
using CreditCardExpenseControl.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardExpenseControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _IReportService;

        public ReportsController(IReportService IReportService)
        {
            _IReportService = IReportService;
        }

        [HttpGet("{year}")]
        public ActionResult<IEnumerable<ReportModel>> GetTransactionsReportByYear(int year)
        {
            return _IReportService.GetTransactionsReportByYear(year);
        }
    }
}