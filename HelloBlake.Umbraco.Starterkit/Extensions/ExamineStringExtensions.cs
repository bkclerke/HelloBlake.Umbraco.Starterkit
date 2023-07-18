using System.Text.RegularExpressions;
namespace HelloBlake.Web.Extensions
{
    public static class ExamineStringExtensions
    {
        public static string MakeSearchQuerySafe(this string query)
        {
            return query?.Replace("*", string.Empty).Replace("?", string.Empty);
        }
    }
}
