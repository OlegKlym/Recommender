using Foundation;
using Recommender.Components;
using Recommender.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(UnderlinedButton), typeof(UnderlinedButtonRenderer))]
namespace Recommender.iOS.Renderers
{
    public class UnderlinedButtonRenderer : ButtonRenderer
    {
        public class CustomButtonRenderer : ButtonRenderer
        {
            protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
            {
                base.OnElementChanged(e);

                if (Control != null)
                {
                    var attributes = new UIStringAttributes
                    {
                        UnderlineStyle = NSUnderlineStyle.Single
                    };

                    var title = Control.TitleLabel?.Text;
                    if (!string.IsNullOrEmpty(title))
                    {
                        var attributedString = new NSAttributedString(title, attributes);
                        Control.SetAttributedTitle(attributedString, UIControlState.Normal);
                    }
                }
            }
        }
    }
}
