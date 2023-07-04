using Xamarin.Forms;

namespace Recommender.Components
{
    public class ConfirmButton : Button
    {
        public ConfirmButton()
        {
            AutomationProperties.SetIsInAccessibleTree(this, true);
            AutomationProperties.SetName(this, Text);


            Application.Current.Resources.TryGetValue("ConfirmButton", out var style);
            if (style is Style confirmButtonStyle)
            {
                Style = confirmButtonStyle;
            }
        }
    }
}

