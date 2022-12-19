using Examine;
using Examine.Search;
using HelloBlake.Web.Extensions;
using HelloBlake.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;

namespace HelloBlake.Web.Services
{
    public class SearchService : ISearchService
    {
        private readonly IExamineManager _examineManager;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IProfiler _profiler;

        public SearchService(IExamineManager examineManager, IUmbracoContextAccessor umbracoContextAccessor, IProfiler iprofiler)
        {
            _examineManager = examineManager;
            _umbracoContextAccessor = umbracoContextAccessor;
            _profiler = iprofiler;
        }
        public IEnumerable<SearchResultItem> GetContentSearchResults(string searchTerm, out long totalItemCount)
        {
            var pageOfResults = GetSearchResults(searchTerm, out totalItemCount);
            var items = new List<SearchResultItem>();
            if (pageOfResults != null && pageOfResults.Any())
            {
                foreach (var item in pageOfResults)
                {
                    if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
                    {
                    }
                    var page = umbracoContext.Content.GetById(int.Parse(item.Id));
                    if (page != null)
                    {
                        items.Add(new SearchResultItem()
                        {
                            PublishedItem = page,
                            Score = item.Score
                        });
                    }
                }
            }
            return items;
        }

        public IEnumerable<ISearchResult> GetSearchResults(string searchTerm, out long totalItemCount)
        {
            totalItemCount = 0;
            if (_examineManager.TryGetIndex(Constants.UmbracoIndexes.ExternalIndexName, out var index))
            {
                searchTerm = searchTerm.MakeSearchQuerySafe();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return Array.Empty<ISearchResult>();
                }
                var searcher = index.Searcher;
                var fieldToSearchLang = "contents" + "_" + CultureInfo.CurrentCulture.ToString().ToLower();
                var fieldToSearchInvariant = "contents";
                var hideFromSitemap = "noIndex";
                var fieldsToSearch = new[] { fieldToSearchLang, fieldToSearchInvariant };
                var criteria = searcher.CreateQuery(IndexTypes.Content);
                IBooleanOperation examineQuery;

                if (searchTerm.Contains(" "))
                {
                    string[] terms = searchTerm.Split(' ');
                    examineQuery = criteria.GroupedOr(fieldsToSearch, terms);
                }
                else
                {
                    examineQuery = criteria.GroupedOr(fieldsToSearch, searchTerm.MultipleCharacterWildcard());
                }

                //var examineQuery = criteria.GroupedOr(fieldsToSearch, searchTerm.MultipleCharacterWildcard()); // wildcard search
                //var examineQuery = criteria.GroupedOr(fieldsToSearch, searchTerm.Fuzzy(0.2f)); // fuzzy search
                examineQuery.Not().Field(hideFromSitemap, 1.ToString());
                
                using (_profiler.Step("Examine query"))
                {
                    var results = examineQuery.Execute();
                    totalItemCount = results.TotalItemCount;
                    if (results.Any())
                    {
                        Debug.WriteLine(criteria.ToString());
                        return results;
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }
            }
            return Enumerable.Empty<ISearchResult>();
        }
    }
}
