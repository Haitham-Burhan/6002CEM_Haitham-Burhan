using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
// MainPage.xaml.cs
using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Notes
{
    public partial class MainPage : ContentPage
    {
        // Collection to hold notes
        ObservableCollection<Note> notes = new ObservableCollection<Note>();
        bool isSelectionMode = false;


        public MainPage()
        {
            InitializeComponent();
            SortAndSetNotes();
            // Set the ItemsSource of the ListView to the notes collection
            notesListView.ItemsSource = notes; // This line might be causing the issue
        }



        private void SortAndSetNotes()
        {
            // Sort the notes collection based on IsTop using LINQ's OrderByDescending method
            var sortedNotes = notes.OrderByDescending(n => n.IsTop).ToList();

            // Set the sorted collection as the ItemsSource of the ListView
            notesListView.ItemsSource = sortedNotes;
        }
        //private void OnItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item == null)
        //        return;

        //    // Toggle selection
        //    var note = (Note)e.Item;
        //    note.IsSelected = !note.IsSelected;
        //    ((ListView)sender).SelectedItem = null; // Deselect item
        //}

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            // Toggle selection
            var note = (Note)e.Item;
            note.IsSelected = !note.IsSelected;

            // Notify the ListView that its items have changed
            ((ListView)sender).ItemsSource = null;
            ((ListView)sender).ItemsSource = notes;

            //((ListView)sender).SelectedItem = null; // Deselect item
        }


        private async void OnAddClicked(object sender, EventArgs e)
        {
            // Open a new window for adding notes
            var addNotePage = new addnote();

            // Handle the result when the new window closes
            addNotePage.NoteAdded += (s, note) =>
            {
                // Add the note to the collection
                notes.Add(note);
                
            };

            await Navigation.PushModalAsync(addNotePage);
        }

        private void OnDeleteClickedx(object sender, EventArgs e)
        {
            // Implement your delete note logic here
            //await DisplayAlert("Delete Note", "Implement your delete note logic here", "OK");
            // Toggle selection mode
            //isSelectionMode = !isSelectionMode;

            // Delete selected items
            if (!isSelectionMode)
            {
                var selectedNotes = notes.Where(note => note.IsSelected).ToList();
                foreach (var selectedNote in selectedNotes)
                {
                    notes.Remove(selectedNote);
                }
            }
        }

        private ObservableCollection<Note> selectedNotes = new ObservableCollection<Note>();


        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            // Get the selected note
            var selectedNote = (Note)e.SelectedItem;

            // Open a new window to display the selected note
            var displayListPage = new DisplayList();

            // Pass the selected note to the new page
            displayListPage.BindingContext = selectedNote;

            await Navigation.PushAsync(displayListPage);
           
            // Deselect the item
            ((ListView)sender).SelectedItem = null;
        }


    }
}
