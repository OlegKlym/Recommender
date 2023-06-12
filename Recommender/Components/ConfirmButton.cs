using Xamarin.Forms;

namespace Recommender.Components
{
    public class ConfirmButton : Button
    {
        public ConfirmButton()
        {
            Application.Current.Resources.TryGetValue("ConfirmButton", out var style);

            if (style is Style confirmButtonStyle)
            {
                Style = confirmButtonStyle;
            }
        }
    }
}

