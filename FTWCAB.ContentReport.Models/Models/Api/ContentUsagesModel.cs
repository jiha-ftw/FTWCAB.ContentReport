namespace FTWCAB.ContentReport.Models.Models.Api
{
    public class ContentUsagesModel
    {
        public int Pages { get; set; }
        public int TotalCount { get; set; }

        public required IEnumerable<ContentUsageModel> Usages { get; set; }
    }

    public class ContentUsageModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EditLink { get; set; }
    }
}
