using Android.Content;
using Recommender.Components;
using Recommender.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomCollectionView), typeof(CustomCollectionViewRenderer))]
namespace Recommender.Droid.Renderers
{
    public class CustomCollectionViewRenderer : CollectionViewRenderer
    {
        public CustomCollectionViewRenderer(Context context) : base(context)
        {
        }

        // public override bool OnInterceptTouchEvent(MotionEvent? ev)
        // {
        //     if (Element is CustomCollectionView element && element.IsScrollEnabled)
        //     {
        //         return true;
        //     }
        //
        //     return false;
        // }
        //
        // public override bool OnTouchEvent(MotionEvent? e)
        // {
        //     if (Element is CustomCollectionView element && element.IsScrollEnabled)
        //     {
        //         return base.OnInterceptTouchEvent(e);
        //     }
        //
        //     return false;
        // }
    }
}