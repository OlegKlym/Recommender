using Android.App;
using Android.Content;
using Android.Support.Design.Internal;
using Java.Util;
using Recommender.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(BottomTabbedPageRenderer))]
namespace Recommender.Droid.Renderers
{
    public class BottomTabbedPageRenderer : TabbedPageRenderer
    {
        public BottomTabbedPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (ViewGroup != null && ViewGroup.ChildCount > 0)
            {
                
            }
        }
    }
}