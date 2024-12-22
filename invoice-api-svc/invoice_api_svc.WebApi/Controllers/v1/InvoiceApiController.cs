using invoice_api_svc.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace invoice_api_svc.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvoiceApiController : ControllerBase
    {
        private readonly ClientDbContext _dbContext;

        public InvoiceApiController(ClientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves sales-related data (Invoices, Credit Notes, Debit Notes).
        /// Sequence: OrderEntryHeader -> ReceivableInvoiceHeader -> ReceivableInvoiceDetail.
        /// </summary>
        [HttpGet("sales")]
        public async Task<IActionResult> GetSalesData(string invoiceNumber = null)
        {
            // Fetch from OrderEntryHeader
            var orderEntryData = await _dbContext.OrderEntryHeaders
                .Where(o => invoiceNumber == null || o.ORDERID == invoiceNumber) // Update to match actual property
                .ToListAsync();

            if (orderEntryData.Any())
                return Ok(orderEntryData);

            // Fetch from ReceivableInvoiceHeader
            var receivableHeaderData = await _dbContext.ReceivableInvoiceHeaders
                .Where(r => invoiceNumber == null || r.INVNUMBER == invoiceNumber) // Update to match actual property
                .ToListAsync();

            if (receivableHeaderData.Any())
                return Ok(receivableHeaderData);

            // Fetch from ReceivableInvoiceDetail
            var receivableDetailData = await _dbContext.ReceivableInvoiceDetails
                .Where(r => invoiceNumber == null || r.INVNUMBER == invoiceNumber) // Update to match actual property
                .ToListAsync();

            if (receivableDetailData.Any())
                return Ok(receivableDetailData);

            return NotFound("No sales data found in Order Entry or Receivables.");
        }

        /// <summary>
        /// Retrieves purchase-related data (Invoices, Allowances, Charges).
        /// Sequence: PurchaseInvoiceHeader -> PurchaseInvoiceDetail.
        /// </summary>
        [HttpGet("purchases")]
        public async Task<IActionResult> GetPurchaseData(string invoiceNumber = null)
        {
            // Fetch from PurchaseInvoiceHeader
            var purchaseHeaderData = await _dbContext.PurchaseInvoiceHeaders
                .Where(p => invoiceNumber == null || p.INVNUMBER == invoiceNumber)
                .ToListAsync();

            if (purchaseHeaderData.Count > 0)
                return Ok(purchaseHeaderData);

            // Fetch from PurchaseInvoiceDetail
            var purchaseDetailData = await _dbContext.PurchaseInvoiceDetails
                .Where(p => invoiceNumber == null || p.INVNUMBER == invoiceNumber)
                .ToListAsync();

            if (purchaseDetailData.Count > 0)
                return Ok(purchaseDetailData);

            return NotFound("No purchase data found in Purchase Invoices.");
        }

        /// <summary>
        /// Retrieves self-billing data.
        /// Sequence: PurchaseInvoiceHeader -> PurchaseInvoiceDetail.
        /// </summary>
        [HttpGet("self-billing")]
        public async Task<IActionResult> GetSelfBillingData(string invoiceNumber = null)
        {
            // Fetch from PurchaseInvoiceHeader
            var selfBillingHeaderData = await _dbContext.PurchaseInvoiceHeaders
                .Where(p => invoiceNumber == null || p.INVNUMBER == invoiceNumber)
                .ToListAsync();

            if (selfBillingHeaderData.Count > 0)
                return Ok(selfBillingHeaderData);

            // Fetch from PurchaseInvoiceDetail
            var selfBillingDetailData = await _dbContext.PurchaseInvoiceDetails
                .Where(p => invoiceNumber == null || p.INVNUMBER == invoiceNumber)
                .ToListAsync();

            if (selfBillingDetailData.Count > 0)
                return Ok(selfBillingDetailData);

            return NotFound("No self-billing data found.");
        }
    }
}
