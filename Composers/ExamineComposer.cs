using HelloBlake.Web.Components;
using HelloBlake.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Composing;

namespace HelloBlake.Web.Composers
{
    public class ExamineComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<ISearchService, SearchService>();
            builder.Components().Append<ExamineComponents>();
        }
    }
}
