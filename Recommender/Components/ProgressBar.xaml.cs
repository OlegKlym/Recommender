using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressBar : Grid
    {
        private readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(int), typeof(Grid), propertyChanged: ValuePropertyChanged);
        
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public ProgressBar()
        {
            InitializeComponent();
        }

        private static void ValuePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
        }
    }
}