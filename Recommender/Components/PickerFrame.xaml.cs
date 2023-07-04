using System.Collections;
using Xamarin.Forms;

namespace Recommender.Components
{
    public partial class PickerFrame : Frame
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create
          (nameof(Title), typeof(string), typeof(PickerFrame));

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create
          (nameof(ItemsSource), typeof(IEnumerable), typeof(PickerFrame));

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create
          (nameof(SelectedItem), typeof(object), typeof(PickerFrame));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public PickerFrame()
        {
            InitializeComponent();
        }
    }
}
