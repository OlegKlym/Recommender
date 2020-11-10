using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseViewPage : ContentPage
    {
        private BindableProperty PageContentProperty =
            BindableProperty.Create(nameof(PageContent), typeof(View), typeof(ContentPage));
        
        public BaseViewPage()
        {
            InitializeComponent();
        }

        public View PageContent
        {
            get => (View)GetValue(PageContentProperty);
            set => SetValue(PageContentProperty, value);
        }
    }
}