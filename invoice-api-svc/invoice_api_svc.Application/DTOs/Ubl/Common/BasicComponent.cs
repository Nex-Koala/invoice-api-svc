using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
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
