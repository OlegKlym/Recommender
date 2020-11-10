using Android.Content;
using Recommender.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(DefaultButtonRenderer))]
namespace Recommender.Droid.Renderers
{
    public class DefaultButtonRenderer : ButtonRenderer
    {
        public DefaultButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetAllCaps(false);
            }
        }
    }
}