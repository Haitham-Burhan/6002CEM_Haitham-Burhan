namespace Notes
{
    public partial class addnote : ContentPage
    {
        public event EventHandler<Note> NoteAdded;

        public addnote()
        {
            InitializeComponent();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            var note = new Note
            {
                Subject = subjectEntry.Text,
                Notes = notesEntry.Text,
                IsTop = IsTopCheckBox.IsChecked,
                date = datePicker.Date
            };

            NoteAdded?.Invoke(this, note);
            Navigation.PopModalAsync();
        }

        private void DiscardButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
