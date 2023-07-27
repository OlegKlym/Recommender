using Recommender.Pages;
using Xamarin.Forms.Xaml;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : StyledPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            
            profilePhoto.Source = "https://scontent.flwo4-2.fna.fbcdn.net/v/t1.0-9/119587546_3381757061889356_7286345077182768041_n.jpg?_nc_cat=109&ccb=2&_nc_sid=09cbfe&_nc_ohc=elVsdWMXsqsAX9t46UC&_nc_ht=scontent.flwo4-2.fna&oh=50a031480c75bec806d92b4a7f0fb6b7&oe=5FD688E7";
        }
    }
}