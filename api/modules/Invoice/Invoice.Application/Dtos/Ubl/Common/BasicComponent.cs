using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexKoala.WebApi.Invoice.Application.Dtos.Ubl.Common
{
    public class BasicComponent
    {
        public string _ { get; set; }
    }

    public class BasicComponent<T>
    {
        public T _ { get; set; }
    }
}
