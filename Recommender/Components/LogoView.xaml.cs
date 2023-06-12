using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoView : Grid
    {
        private readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Grid), string.Empty);

        private readonly BindableProperty  SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(Grid), string.Empty);

        public LogoView()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => GetValue(TextProperty).ToString();
            set => SetValue(TextProperty,value);
        }

        public string Source
        {
            get => GetValue(SourceProperty).ToString();
            set => SetValue(SourceProperty, value);
        }
    }
}