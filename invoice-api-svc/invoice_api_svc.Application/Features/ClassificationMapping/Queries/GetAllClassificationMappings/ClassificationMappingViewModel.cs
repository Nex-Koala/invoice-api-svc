namespace invoice_api_svc.Application.Features.ClassificationMapping.Queries.GetAllClassificationMappings
{
    public class ClassificationMappingViewModel
    {
        public int Id { get; set; }
        public string LhdnClassificationCode { get; set; } = default!;
        public int ClassificationId { get; set; }

    }
}
