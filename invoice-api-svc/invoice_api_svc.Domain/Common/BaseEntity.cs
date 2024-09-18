using System;
using System.Collections.Generic;
using System.Text;

namespace invoice_api_svc.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
    }
}
