using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoView : Grid
    {
        private BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(Grid), string.Empty);
        public LogoView()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => GetValue(TextProperty).ToString();
            set => SetValue(TextProperty,value);
        }
    }
}