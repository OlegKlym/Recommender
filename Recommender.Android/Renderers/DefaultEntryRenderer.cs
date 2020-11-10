using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;
using Xamarin.Forms.Platform.Android;

namespace Recommender.Droid.Renderers
{
    public class DefaultEntryRenderer : MaterialEntryRenderer
    {
        public DefaultEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.EditText.Background = null;
                Control.EditText.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}
