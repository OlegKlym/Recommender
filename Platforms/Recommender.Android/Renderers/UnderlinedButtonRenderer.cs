using Android.Content;
using Recommender.Components;
using Recommender.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(UnderlinedButton), typeof(UnderlinedButtonRenderer))]
namespace Recommender.Droid.Renderers
{
    public class UnderlinedButtonRenderer : ButtonRenderer
    {
        public UnderlinedButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.PaintFlags |= Android.Graphics.PaintFlags.UnderlineText;
            }
        }
    }
}
