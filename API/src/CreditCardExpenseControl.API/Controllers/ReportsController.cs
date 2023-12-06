using CreditCardExpenseControl.API.Services;
using CreditCardExpenseControl.Core.Models;
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
        public ActionResult<IEnumerable<ReportModel>> GetReportByYear(int year)
        {
            return _IReportService.GetReportByYear(year);
        }

        [HttpGet("{year}/{month}")]
        public ActionResult<IEnumerable<ReportTransactionModel>> GetReportTransactionsByYearAndMonth(int year, int month)
        {
            return _IReportService.GetReportTransactionsByYearAndMonth(year, month);
        }
    }
}