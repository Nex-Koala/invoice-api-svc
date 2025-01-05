namespace invoice_api_svc.Application.Features.UomMappings.Queries.GetAllUomMappings
{
    public class UomMappingViewModel
    {
        public int Id { get; set; }
        public string LhdnUomCode { get; set; } = default!;
        public int UomId { get; set; }

    }
}
