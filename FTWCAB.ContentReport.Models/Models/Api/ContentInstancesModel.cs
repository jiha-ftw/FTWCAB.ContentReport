namespace FTWCAB.ContentReport.Models.Models.Api
{
    public class ContentInstancesModel
    {
        public int Pages { get; set; }
        public int TotalCount { get; set; }

        public required IEnumerable<ContentInstanceModel> Instances { get; set; }
    }

    public class ContentInstanceModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EditLink { get; set; }
        public string? ParentContentTypeName { get; set; }
        public string? ParentEditLink { get; set; }
        public string? ParentName { get; set; }
    }
}
