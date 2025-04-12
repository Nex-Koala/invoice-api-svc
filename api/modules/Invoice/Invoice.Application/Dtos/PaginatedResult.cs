using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos;

public class PaginatedResult<T>
{
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public List<T> Data { get; set; }
}
