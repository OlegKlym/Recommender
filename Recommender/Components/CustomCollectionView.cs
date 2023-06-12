using Xamarin.Forms;

namespace Recommender.Components
{
    public class CustomCollectionView : CollectionView
    {
        private static readonly BindableProperty IsScrollEnabledProperty = 
            BindableProperty.Create(nameof(IsScrollEnabled), typeof(bool), typeof(CustomCollectionView), true);

        public bool IsScrollEnabled
        {
            get => (bool) GetValue(IsScrollEnabledProperty);
            set => SetValue(IsScrollEnabledProperty, value);
        }
    }
}