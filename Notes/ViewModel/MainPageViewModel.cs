using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notes
{
    public partial class MainPage : ContentPage
    {
        private NoteViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new NoteViewModel();
            BindingContext = _viewModel;
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            var addNotePage = new addnote();
            addNotePage.NoteAdded += async (s, note) =>
            {
                await _viewModel.SaveNoteAsync(note);
            };
            await Navigation.PushModalAsync(addNotePage);
        }

        private async void OnDeleteClickedx(object sender, EventArgs e)
        {
            var selectedNotes = _viewModel.Notes.Where(note => note.IsSelected).ToList();
            foreach (var selectedNote in selectedNotes)
            {
                await _viewModel.DeleteNoteAsync(selectedNote.Id);
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedNote = (Note)e.SelectedItem;
            var displayListPage = new DisplayList();
            displayListPage.BindingContext = selectedNote;
            await Navigation.PushAsync(displayListPage);

            ((ListView)sender).SelectedItem = null;
        }
    }
}
