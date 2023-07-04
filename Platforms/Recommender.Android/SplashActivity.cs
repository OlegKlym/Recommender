using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace Recommender.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]    
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            StartActivity(new Intent(Application.Context, typeof (MainActivity)));
        }
    }
}