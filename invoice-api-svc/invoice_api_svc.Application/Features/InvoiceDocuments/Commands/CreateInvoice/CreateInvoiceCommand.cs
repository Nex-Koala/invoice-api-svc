using invoice_api_svc.Application.Interfaces.Repositories;
using invoice_api_svc.Application.Wrappers;
using AutoMapper;
using invoice_api_svc.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using invoice_api_svc.Application.DTOs.EInvoice;
using System.Collections.Generic;
using System;

namespace invoice_api_svc.Application.Features.Invoices.Commands.CreateInvoice
{
    public partial class CreateInvoiceCommand : IRequest<Response<int>>
    {
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string SupplierName { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Response<int>>
    {
        private readonly IInvoiceDocumentRepositoryAsync _invoiceDocumentRepository;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IInvoiceDocumentRepositoryAsync invoiceDocumentRepository, IMapper mapper)
        {
            _invoiceDocumentRepository = invoiceDocumentRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            // Map command to domain entity
            var invoice = _mapper.Map<InvoiceDocument>(request);

            // Save invoice using repository
            await _invoiceDocumentRepository.AddAsync(invoice);

            // Return the invoice Id wrapped in a Response
            return new Response<int>(invoice.Id);
        }
    }
}
