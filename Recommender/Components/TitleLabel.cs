using Xamarin.Forms;

namespace Recommender.Components
{
    public class TitleLabel : Label
    {
        public TitleLabel()
        {
            AutomationProperties.SetIsInAccessibleTree(this, true);
            AutomationProperties.SetName(this, Text);

            Application.Current.Resources.TryGetValue("TitleLabel", out var style);
            if (style is Style titleLabelStyle)
            {
                Style = titleLabelStyle;
            }
        }
    }
}
