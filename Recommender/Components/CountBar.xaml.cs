using Xamarin.Forms;

namespace Recommender.Components
{
    public partial class CountBar : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create
         (nameof(Text), typeof(string), typeof(CountBar));

        public static readonly BindableProperty ValueProperty = BindableProperty.Create
          (nameof(Value), typeof(double), typeof(CountBar),0);


        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public CountBar()
        {
            InitializeComponent();
        }
    }
}
