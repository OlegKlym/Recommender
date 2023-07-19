using System;
using System.Collections.Generic;
using PropertyChanged;
using Xamarin.Forms;

namespace Recommender.Components
{
    [SuppressPropertyChangedWarnings]
    public partial class DatePickerFrame : Frame
    {
        private static readonly List<string> _defaultDateTimes = new List<string>
        {
            "1/1/1900 12:00:00 AM",
            "1/1/1970 12:00:00 AM",
            "1/1/0001 12:00:00 AM"
        };

        public static readonly BindableProperty DateProperty = BindableProperty.Create
         (nameof(Date), typeof(DateTime), typeof(DatePickerFrame), null, propertyChanged: OnDatePropertyChanged);

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create
         (nameof(Placeholder), typeof(string), typeof(DatePickerFrame));

        public static readonly BindableProperty IsNullProperty = BindableProperty.Create
        (nameof(IsNull), typeof(bool), typeof(DatePickerFrame), true);

        public DateTime? Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public bool IsNull
        {
            get => (bool)GetValue(IsNullProperty);
            set => SetValue(IsNullProperty, value);
        }

        public DatePickerFrame()
        {
            InitializeComponent();
        }

        private static void OnDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is DatePickerFrame datePickerFrame)
            {
                if(newValue == null || _defaultDateTimes.Contains(newValue.ToString()))
                {
                    return;
                }

                datePickerFrame.IsNull = false;
            }
        }
    }
}
