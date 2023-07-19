using System.Windows.Input;
using Xamarin.Forms;

namespace Recommender.Components
{
    public partial class LoginForm : StackLayout
    {
        public static readonly BindableProperty EmailProperty =
            BindableProperty.Create(nameof(Email), typeof(string), typeof(LoginForm),
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PasswordProperty =
            BindableProperty.Create(nameof(Password), typeof(string), typeof(LoginForm),
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LoginForm));

        public LoginForm()
        {
            InitializeComponent();
        }

        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}
