using Examine;
using HelloBlake.Web.Models;
using System.Collections.Generic;

namespace HelloBlake.Web.Services
{
    public interface ISearchService
    {
        IEnumerable<ISearchResult> GetSearchResults(string searchTerm, out long totalItemCount);
        IEnumerable<SearchResultItem> GetContentSearchResults(string searchTerm, out long totalItemCount);
    }
}
