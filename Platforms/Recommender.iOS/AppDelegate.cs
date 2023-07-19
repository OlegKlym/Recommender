using Foundation;
using UIKit;
using Xamarin.Forms;

namespace Recommender.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            Forms.SetFlags(new string[]
            {
                "MediaElement_Experimental",
                "Brush_Experimental",
                "SwipeView_Experimental"
            });
            Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
