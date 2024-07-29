                                                    // MainPage.xaml.cs

using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using SQLite;

namespace Notes
{
    public partial class MainPage : ContentPage
    {
        // Collection to hold notes
        ObservableCollection<Note> notes = new ObservableCollection<Note>();
        bool isSelectionMode = false;
        SQLiteConnection database;

        public MainPage()
        {
            InitializeComponent();
            SortAndSetNotes();
            // Set the ItemsSource of the ListView to the notes collection
            notesListView.ItemsSource = notes; // This line might be causing the issue

            // Initialize SQLite database connection
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db");
            database = new SQLiteConnection(dbPath);

            // Create table if it doesn't exist
            database.CreateTable<Note>();

            // Load notes from the database
            LoadNotes();


        }

        private void LoadNotes()
        {
            // Retrieve all notes from the database
            var allNotes = database.Table<Note>().ToList();

            // Clear existing notes
           // notes.Clear();

            // Add each note to the ObservableCollection
            foreach (var note in allNotes)
            {
                notes.Add(note);
            }
        }

        //private void LoadNotes()
        //{
        //    // Retrieve all notes from the database
        //    var allNotes = database.Table<Note>().ToList();

        //    // Clear existing notes
        //    notes.Clear();

        //    // Add each note to the ObservableCollection
        //    foreach (var note in allNotes)
        //    {
        //        notes.Add(note);
        //    }
        //}


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

                // Add the note to the database
                database.Insert(note);

                // Refresh notes from the database
                LoadNotes();

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
                    database.Delete(selectedNote);
                }
                // Refresh notes from the database
                LoadNotes();
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