// DisplayList.xaml.cs
using Microsoft.Maui.Controls;

namespace Notes
{
    public partial class DisplayList : ContentPage
    {
        public DisplayList()
        {
            InitializeComponent();
        }

        private void OnIsTopCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (BindingContext is Note note)
            {
                // Update the IsTop property of the bound Note object
                note.IsTop = e.Value;
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Ensure BindingContext is set
            if (BindingContext == null)
                BindingContext = new Note(); // Assuming a default empty note
        }
    }
}