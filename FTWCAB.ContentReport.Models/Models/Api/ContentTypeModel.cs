namespace FTWCAB.ContentReport.Models.Api
{
    public class ContentTypeGroupViewModel
    {
        public ContentTypeGroupViewModel(string label, IEnumerable<ContentTypeViewModel> options)
        {
            Label = label;
            Options = options;
        }

        public string Label { get; set; }
        public IEnumerable<ContentTypeViewModel> Options { get; set; }
    }

    public class ContentTypeViewModel
    {
        public ContentTypeViewModel(int id, string name, string fullName)
        {
            Id = id;
            Name = name;
            FullName = fullName;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; }
    }
}
