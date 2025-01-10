namespace invoice_api_svc.Application.Features.Classification.Queries.GetAllClassifications
{
    public class GetAllClassificationViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }
}
