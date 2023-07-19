using System.Windows.Input;
using Xamarin.Forms;

namespace Recommender.Components
{
    public partial class BottomButtonBar : Grid
    {
        public static readonly BindableProperty LeftButtonTextProperty =
           BindableProperty.Create(nameof(LeftButtonText), typeof(string), typeof(BottomButtonBar));

        public static readonly BindableProperty LeftButtonCommandProperty =
           BindableProperty.Create(nameof(LeftButtonCommand), typeof(ICommand), typeof(BottomButtonBar));

        public static readonly BindableProperty RightButtonTextProperty =
           BindableProperty.Create(nameof(RightButtonText), typeof(string), typeof(BottomButtonBar));

        public static readonly BindableProperty RightButtonCommandProperty =
           BindableProperty.Create(nameof(RightButtonCommand), typeof(ICommand), typeof(BottomButtonBar));

        public BottomButtonBar()
        {
            InitializeComponent();
        }

        public string LeftButtonText
        {
            get { return (string)GetValue(LeftButtonTextProperty); }
            set { SetValue(LeftButtonTextProperty, value); }
        }

        public ICommand LeftButtonCommand
        {
            get { return (ICommand)GetValue(LeftButtonCommandProperty); }
            set { SetValue(LeftButtonCommandProperty, value); }
        }

        public string RightButtonText
        {
            get { return (string)GetValue(RightButtonTextProperty); }
            set { SetValue(RightButtonTextProperty, value); }
        }

        public ICommand RightButtonCommand
        {
            get { return (ICommand)GetValue(RightButtonCommandProperty); }
            set { SetValue(RightButtonCommandProperty, value); }
        }
    }
}
