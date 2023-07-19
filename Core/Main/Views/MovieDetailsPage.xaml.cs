using System.Collections.Generic;
using Recommender.Core.Models;
using Recommender.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailsPage : StyledPage
    {
        public MovieDetailsPage()
        {
            InitializeComponent();

            var personsList = new List<PersonModel>
            {
                new PersonModel { FullName = "Brad Pitt", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg"},
                new PersonModel { FullName = "Brad Pitt2", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg"},
                new PersonModel { FullName = "Brad Pitt3", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg"},
                new PersonModel { FullName = "Brad Pitt4", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg"},
                new PersonModel { FullName = "Brad Pitt5", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg"},
                new PersonModel { FullName = "Brad Pitt6", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg"},
                new PersonModel { FullName = "Brad Pitt7", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg"},
                new PersonModel { FullName = "Brad Pitt8", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg"},
                new PersonModel { FullName = "Brad Pitt9", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4c/Brad_Pitt_2019_by_Glenn_Francis.jpg"},
            };

            //BindableLayout.SetItemsSource(actorsList, personsList);
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (sender is ImageButton imageButton)
            {
                var fileSource = imageButton.Source as FileImageSource;

                switch(fileSource.File)
                {
                    case "not_seen.png":
                        imageButton.Source = "like.png";
                        imageButton.BackgroundColor = Color.Green;
                        break;
                    case "like.png":
                        imageButton.Source = "sceptic.png";
                        imageButton.BackgroundColor = Color.Orange;
                        break;
                    case "sceptic.png":
                        imageButton.Source = "dislike.png";
                        imageButton.BackgroundColor = Color.FromHex("#D7413C");
                        break;
                    case "dislike.png":
                        imageButton.Source = "not_seen.png";
                        imageButton.BackgroundColor = Color.LightGray;
                        break;
                }
            }
        }

        void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopModalAsync();
            });
        }
    }
}