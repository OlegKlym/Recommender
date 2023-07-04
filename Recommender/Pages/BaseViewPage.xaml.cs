using Xamarin.Forms;

namespace Recommender.Views
{
    [ContentProperty(nameof(PageContent))]
    public partial class BaseViewPage : ContentPage
    {
        private readonly BindableProperty PageContentProperty =
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