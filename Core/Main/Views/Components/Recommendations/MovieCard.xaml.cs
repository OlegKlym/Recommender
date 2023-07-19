using Xamarin.Forms;

namespace Recommender.Components
{
    public partial class MovieCard : Grid
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(MovieCard), string.Empty);

        public static readonly BindableProperty YearProperty =
            BindableProperty.Create(nameof(Year), typeof(string), typeof(MovieCard), string.Empty);

        public static readonly BindableProperty MainGenreProperty =
            BindableProperty.Create(nameof(MainGenre), typeof(string), typeof(MovieCard), string.Empty);

        public static readonly BindableProperty AdditionalGenreProperty =
            BindableProperty.Create(nameof(AdditionalGenre), typeof(string), typeof(MovieCard), string.Empty);

        public static readonly BindableProperty PopularityProperty =
            BindableProperty.Create(nameof(Popularity), typeof(int), typeof(MovieCard), 0);

        public static readonly BindableProperty AverageRatingProperty =
            BindableProperty.Create(nameof(AverageRating), typeof(double), typeof(MovieCard), 0.0);

        public static readonly BindableProperty ImageUrlProperty =
           BindableProperty.Create(nameof(ImageUrl), typeof(string), typeof(MovieCard), string.Empty);

        public MovieCard()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Year
        {
            get { return (string)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        public string MainGenre
        {
            get { return (string)GetValue(MainGenreProperty); }
            set { SetValue(MainGenreProperty, value); }
        }

        public string AdditionalGenre
        {
            get { return (string)GetValue(AdditionalGenreProperty); }
            set { SetValue(AdditionalGenreProperty, value); }
        }

        public int Popularity
        {
            get { return (int)GetValue(PopularityProperty); }
            set { SetValue(PopularityProperty, value); }
        }

        public double AverageRating
        {
            get { return (double)GetValue(AverageRatingProperty); }
            set { SetValue(AverageRatingProperty, value); }
        }

        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }
    }
}
