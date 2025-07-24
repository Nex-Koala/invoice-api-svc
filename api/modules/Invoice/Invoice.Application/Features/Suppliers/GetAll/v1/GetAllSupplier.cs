using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NexKoala.Framework.Core.Wrappers;
using NexKoala.WebApi.Invoice.Application.Features.InvoiceDocuments.Get.v1;

namespace NexKoala.WebApi.Invoice.Application.Features.Suppliers.GetAll.v1;
public record GetAllSupplier (Guid UserId) : IRequest<Response<List<SupplierDto>>>;
