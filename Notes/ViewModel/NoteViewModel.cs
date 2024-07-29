using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using SQLite;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Notes.ViewModel;

namespace Notes
{
    public class NoteViewModel : BaseViewModel
    {
        private HttpClient _httpClient;
        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public NoteViewModel()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-functions-key", AppSettings.AzureFunctionKey);
            Notes = new ObservableCollection<Note>();
            LoadNotesAsync();
        }

        public async Task LoadNotesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Note>>($"{AppSettings.AzureFunctionUrl}notes");
            if (response != null)
            {
                Notes.Clear();
                foreach (var note in response)
                {
                    Notes.Add(note);
                }
            }
        }

        public async Task SaveNoteAsync(Note note)
        {
            var response = await _httpClient.PostAsJsonAsync($"{AppSettings.AzureFunctionUrl}notes", note);
            response.EnsureSuccessStatusCode();
            await LoadNotesAsync(); // Refresh the notes after saving
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            var response = await _httpClient.DeleteAsync($"{AppSettings.AzureFunctionUrl}notes/{noteId}");
            response.EnsureSuccessStatusCode();
            await LoadNotesAsync(); // Refresh the notes after deleting
        }
    }
}