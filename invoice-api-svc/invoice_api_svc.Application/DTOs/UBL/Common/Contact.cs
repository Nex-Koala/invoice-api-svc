namespace invoice_api_svc.Application.DTOs.UBL.Common
{
    public class Contact
    {
        public BasicComponent[] Telephone { get; set; }
        public BasicComponent[] ElectronicMail { get; set; }
    }
}