using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using invoice_api_svc.Infrastructure.Persistence.Contexts;
using invoice_api_svc.WebApi.Services;
using System.Linq;
using System.Threading.Tasks;
using invoice_api_svc.WebApi.Helpers;

namespace invoice_api_svc.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvoiceApiController : ControllerBase
    {
        private readonly ClientDbContext _dbContext;
        private readonly TrimStringService _trimStringService;

        public InvoiceApiController(ClientDbContext dbContext)
        {
            _dbContext = dbContext;
            _trimStringService = new TrimStringService();
        }

        /// <summary>
        /// Retrieves sales invoices from Order Entry tables.
        /// Table Reference 
        /// OE => OEINVH (Order Entry Header) and OEINVD (Order Entry Detail)
        /// AR => ARIBH (Receivable Invoice Header) and ARIBD (Receivable Invoice Detail)
        ///       Fields: Item Description (TEXTDESC), Amounts (AMTEXTN), Tax (AMTTAX1)
        /// AP => APIBH (Payable Invoice Header) and APIBD (Payable Invoice Detail)
        ///       NA??
        /// </summary>
        [HttpGet("sales-invoices")]
        public async Task<IActionResult> GetSalesInvoices(
            int page = 1,
            int pageSize = 10,
            decimal? invoiceNumber = null)
        {
            var query = _dbContext.OrderEntryHeaders
                .Include(h => h.OrderEntryDetails)
                .AsQueryable();

            if (invoiceNumber.HasValue)
            {
                query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{invoiceNumber}%"));
            }

            var paginatedResult = await PaginationHelper.PaginateAsync(query, page, pageSize);

            foreach (var header in paginatedResult.Data)
            {
                _trimStringService.TrimStringProperties(header);
                header.OrderEntryDetails = header.OrderEntryDetails
                    .Select(detail => _trimStringService.TrimStringProperties(detail))
                    .ToList();
            }

            return Ok(paginatedResult);
        }


        /// <summary>
        /// Retrieves credit/debit notes from Order Entry tables.
        /// OE => OECRDH (Order Entry Credit/Debit Note Header) and OECRDD (Order Entry Credit/Debit Note Detail)
        /// AR => ARIBH (Receivable Invoice Header) and ARIBD (Receivable Invoice Detail)
        ///       Fields: Credit Details (TEXTDESC), Adjustments (AMTEXTN)
        /// AP => APIBH (Payable Invoice Header) and APIBD (Payable Invoice Detail)
        ///       Fields: Description (TEXTDESC), Tax (AMTTAX1)
        /// </summary>
        [HttpGet("credit-debit-notes")]
        public async Task<IActionResult> GetCreditDebitNotes(
            int page = 1,
            int pageSize = 10,
            decimal? sequenceNumber = null)
        {
            var query = _dbContext.OrderCreditDebitHeaders
                .Include(h => h.OrderCreditDebitDetails)
                .AsQueryable();

            if (sequenceNumber.HasValue)
            {
                query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{sequenceNumber}%"));
            }

            var paginatedResult = await PaginationHelper.PaginateAsync(query, page, pageSize);

            foreach (var note in paginatedResult.Data)
            {
                _trimStringService.TrimStringProperties(note);
                note.OrderCreditDebitDetails = note.OrderCreditDebitDetails
                    .Select(detail => _trimStringService.TrimStringProperties(detail))
                    .ToList();
            }

            return Ok(paginatedResult);
        }

        /// <summary>
        /// Retrieves purchase invoices (self-billing).
        /// PO => POINVH1 (Purchase Invoice Header) and POINVL (Purchase Invoice Line)
        /// AP => APIBH (Accounts Payable Invoice Header) and APIBD (Accounts Payable Invoice Detail)
        /// </summary>
        [HttpGet("purchase-invoices")]
        public async Task<IActionResult> GetPurchaseInvoices(
            int page = 1,
            int pageSize = 10,
            string invoiceNumber = null)
        {
            var query = _dbContext.PurchaseInvoiceHeaders
                .Include(h => h.PurchaseInvoiceDetails)
                .AsQueryable();

            if (!string.IsNullOrEmpty(invoiceNumber))
            {
                query = query.Where(h => EF.Functions.Like(h.INVNUMBER, $"%{invoiceNumber}%"));
            }

            var paginatedResult = await PaginationHelper.PaginateAsync(query, page, pageSize);

            foreach (var invoice in paginatedResult.Data)
            {
                _trimStringService.TrimStringProperties(invoice);
                invoice.PurchaseInvoiceDetails = invoice.PurchaseInvoiceDetails
                    .Select(detail => _trimStringService.TrimStringProperties(detail))
                    .ToList();
            }

            return Ok(paginatedResult);
        }

        /// <summary>
        /// Retrieves purchase credit/debit notes (self-billing).
        /// PO => POCRNH1 (Purchase Credit/Debit Note Header) and POCRNL (Purchase Credit/Debit Note Line)
        /// AP => APIBH (Accounts Payable Invoice Header) and APIBD (Accounts Payable Invoice Detail)
        /// </summary>
        [HttpGet("purchase-credit-debit-notes")]
        public async Task<IActionResult> GetPurchaseCreditDebitNotes(
            int page = 1,
            int pageSize = 10,
            string noteNumber = null)
        {
            var query = _dbContext.PurchaseCreditNoteHeaders
                .Include(h => h.PurchaseCreditDebitNoteDetails)
                .AsQueryable();

            if (!string.IsNullOrEmpty(noteNumber))
            {
                query = query.Where(h => EF.Functions.Like(h.CRNNUMBER, $"%{noteNumber}%"));
            }

            var paginatedResult = await PaginationHelper.PaginateAsync(query, page, pageSize);

            foreach (var note in paginatedResult.Data)
            {
                _trimStringService.TrimStringProperties(note);
                note.PurchaseCreditDebitNoteDetails = note.PurchaseCreditDebitNoteDetails
                    .Select(detail => _trimStringService.TrimStringProperties(detail))
                    .ToList();
            }

            return Ok(paginatedResult);
        }
    }
}
