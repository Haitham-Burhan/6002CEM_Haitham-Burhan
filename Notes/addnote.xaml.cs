// addnote.xaml.cs
using Microsoft.Maui.Controls;
using System;

namespace Notes
{
    public partial class addnote : ContentPage
    {
        // Event raised when a new note is added
        public event EventHandler<Note> NoteAdded;

        public addnote()
        {
            InitializeComponent();
        }
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            // Create a new note object
            var note = new Note
            {
                Subject = subjectEntry.Text, // Get subject from Entry
                Notes = notesEntry.Text,// Get notes content from Editor
                IsTop = IsTopCheckBox.IsChecked,
                date = datePicker.Date
            };

            // Invoke event to pass the note back to the main page
            NoteAdded?.Invoke(this, note);

            // Close the page
            Navigation.PopModalAsync();
        }


        private void DiscardButton_Clicked(object sender, EventArgs e)
        {
            // Close the page without saving
            Navigation.PopModalAsync();
        }
    }
}
