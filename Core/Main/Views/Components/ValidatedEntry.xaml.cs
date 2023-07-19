using System;
using System.Linq;
using FluentValidation;
using PropertyChanged;
using Xamarin.Forms;

namespace Recommender.Components
{
    [SuppressPropertyChangedWarnings]
    public partial class ValidatedEntry : StackLayout, IDisposable
    {
        public static readonly BindableProperty EntryPlaceholderProperty = BindableProperty.Create
            (nameof(EntryPlaceholder), typeof(string), typeof(ValidatedEntry));

        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create
            (nameof(EntryText), typeof(string), typeof(ValidatedEntry), defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnEntryTextPropertyChanged);

        public static readonly BindableProperty IsPasswordEntryProperty = BindableProperty.Create
           (nameof(IsPasswordEntry), typeof(bool), typeof(ValidatedEntry));

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create
           (nameof(MaxLength), typeof(short), typeof(ValidatedEntry));

        public static readonly BindableProperty ValidatorProperty = BindableProperty.Create
            (nameof(Validator), typeof(IValidator), typeof(ValidatedEntry));

        public static readonly BindableProperty ValidationTextProperty = BindableProperty.Create
            (nameof(ValidationText), typeof(string), typeof(ValidatedEntry), string.Empty);

        public static readonly BindableProperty ValidationTextColorProperty = BindableProperty.Create
            (nameof(ValidationTextColor), typeof(Color), typeof(ValidatedEntry), Color.Red);

        public string EntryPlaceholder
        {
            get => (string)GetValue(EntryPlaceholderProperty);
            set => SetValue(EntryPlaceholderProperty, value);
        }

        public string EntryText
        {
            get => (string)GetValue(EntryTextProperty);
            set => SetValue(EntryTextProperty, value);
        }

        public bool IsPasswordEntry
        {
            get => (bool)GetValue(IsPasswordEntryProperty);
            set => SetValue(IsPasswordEntryProperty, value);
        }

        public short MaxLength
        {
            get => (short)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public IValidator Validator
        {
            get { return (IValidator)GetValue(ValidatorProperty); }
            set { SetValue(ValidatorProperty, value); }
        }

        public string ValidationText
        {
            get { return (string)GetValue(ValidationTextProperty); }
            set { SetValue(ValidationTextProperty, value); }
        }

        public Color ValidationTextColor
        {
            get { return (Color)GetValue(ValidationTextColorProperty); }
            set { SetValue(ValidationTextColorProperty, value); }
        }

        public ValidatedEntry()
        {
            InitializeComponent();

            EntryView.Unfocused += EntryView_Unfocused;
        }

        public void Dispose()
        {
            EntryView.Unfocused -= EntryView_Unfocused;
        }

        private void EntryView_Unfocused(object sender, FocusEventArgs e)
        {
            ValidateEntry();
        }

        private void ValidateEntry()
        {
            if (Validator != null)
            {
                var validationContext = new ValidationContext<string>(EntryView.Text);
                var validationResult = Validator.Validate(validationContext);

                if(validationResult.IsValid)
                {
                    ValidationText = string.Empty;
                }
                else
                {
                    ValidationText = validationResult.Errors.FirstOrDefault()?.ErrorMessage;
                }
            }
        }

        private static void OnEntryTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ValidatedEntry entryFrame)
            {
                AutomationProperties.SetIsInAccessibleTree(entryFrame, true);
                AutomationProperties.SetName(entryFrame, entryFrame.EntryText);
            }
        }
    }
}
