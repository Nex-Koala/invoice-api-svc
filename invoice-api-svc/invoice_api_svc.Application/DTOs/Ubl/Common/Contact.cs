using invoice_api_svc.Application.DTOs.Ubl.Common;

namespace invoice_api_svc.Application.DTOs.Ubl.Common
{
    public class Contact
    {
        public BasicComponent[] Telephone { get; set; }
        public BasicComponent[] ElectronicMail { get; set; }
    }
}
