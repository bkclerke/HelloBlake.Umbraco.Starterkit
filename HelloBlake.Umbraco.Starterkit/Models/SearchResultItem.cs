using Umbraco.Cms.Core.Models.PublishedContent;

namespace HelloBlake.Web.Models
{
    public class SearchResultItem
    {
        public IPublishedContent PublishedItem { get; init; }
        public float Score { get; init; }
    }
}
