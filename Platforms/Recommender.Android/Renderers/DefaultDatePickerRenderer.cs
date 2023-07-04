using System.ComponentModel;
using Android.Content;
using Recommender.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker), typeof(DefaultDatePickerRenderer))]
namespace Recommender.Droid.Renderers
{
    public class DefaultDatePickerRenderer : DatePickerRenderer
    {
        public DefaultDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if(e.NewElement.Date == e.NewElement.MinimumDate)
                {
                    Control.Text = null;
                }

                Control.Background = null;
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}
