using Xamarin.Forms;

namespace Recommender.Helpers
{
    public static class PageHelper
    {
        public static Page GetParentPage(Element element)
        {
            var parent = element.Parent;
            while (parent != null)
            {
                if (parent is Page page)
                {
                    return page;
                }

                parent = parent.Parent;
            }

            return null;
        }
    }
}
