using System;
using Xamarin.Forms;
using Recommender.Helpers;
using PropertyChanged;

namespace Recommender.Components
{
    [SuppressPropertyChangedWarnings]
    public partial class VideoPlayer : Grid, IDisposable
    {
        private Page parentPage;

        public static readonly BindableProperty UrlProperty =
            BindableProperty.Create(nameof(Url), typeof(string), typeof(VideoPlayer), default(string));

        public static readonly BindableProperty IsLoopingProperty =
            BindableProperty.Create(nameof(IsLooping), typeof(bool), typeof(VideoPlayer), true);

        public static readonly BindableProperty EnableAutoPlayProperty =
            BindableProperty.Create(nameof(EnableAutoPlay), typeof(bool), typeof(VideoPlayer), true);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public bool IsLooping
        {
            get { return (bool)GetValue(IsLoopingProperty); }
            set { SetValue(IsLoopingProperty, value); }
        }

        public bool EnableAutoPlay
        {
            get { return (bool)GetValue(EnableAutoPlayProperty); }
            set { SetValue(EnableAutoPlayProperty, value); }
        }

        public VideoPlayer()
        {
            InitializeComponent();

            BindingContextChanged += OnBindingContextChanged;

            MessagingCenter.Subscribe<App>(this, "AppSleep", (app) => PauseVideo());
            MessagingCenter.Subscribe<App>(this, "AppResume", (app) => PlayVideo());
        }

        public void Dispose()
        {
            BindingContextChanged -= OnBindingContextChanged;
            MessagingCenter.Unsubscribe<App>(this, "AppSleep");
            MessagingCenter.Unsubscribe<App>(this, "AppResume");

            if (parentPage != null)
            {
                parentPage.Appearing -= ParentPage_Appearing;
                parentPage.Disappearing -= ParentPage_Disappearing;
                parentPage = null;
            }
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                parentPage = PageHelper.GetParentPage(this);
                parentPage.Appearing += ParentPage_Appearing;
                parentPage.Disappearing += ParentPage_Disappearing; 
            }
        }

        private void ParentPage_Appearing(object sender, EventArgs e)
        {
            PlayVideo();
        }

        private void ParentPage_Disappearing(object sender, EventArgs e)
        {
            PauseVideo();
        }

        private void PlayVideo()
        {
            if (EnableAutoPlay)
            {
                videoPlayer.Play();
            }
        }

        private void PauseVideo()
        {
            if (videoPlayer != null)
            {
                videoPlayer.Pause();
            }
        }
    }
}
