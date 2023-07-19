using Xamarin.Forms;

namespace Recommender.Components
{
    public class UnderlinedButton : Button
    {
        public UnderlinedButton()
        {
            AutomationProperties.SetIsInAccessibleTree(this, true);
            AutomationProperties.SetName(this, Text);

            Application.Current.Resources.TryGetValue("UnderlinedButton", out var style);
            if (style is Style underlinedButtonStyle)
            {
                Style = underlinedButtonStyle;
            }
        }
    }
}
