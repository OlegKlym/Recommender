using System.Windows.Input;
using Xamarin.Forms;

namespace Recommender.Components
{
    public class CustomSearchBar : SearchBar
    {
        public static readonly BindableProperty ClearSearchCommandProperty = BindableProperty.Create
           (nameof(ClearSearchCommand), typeof(ICommand), typeof(CustomSearchBar));

        public ICommand ClearSearchCommand
        {
            get => (ICommand)GetValue(ClearSearchCommandProperty);
            set => SetValue(ClearSearchCommandProperty, value);
        }

        protected override void OnTextChanged(string oldValue, string newValue)
        {
            base.OnTextChanged(oldValue, newValue);

            if(string.IsNullOrEmpty(newValue))
            {
                ClearSearchCommand?.Execute(null);
            }
        }
    }
}
