using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace Notes.ViewModel
{
    public class NotePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _noteText;
        public string NoteText
        {
            get => _noteText;
            set
            {
                _noteText = value;
                OnPropertyChanged(nameof(NoteText));
            }
        }

        public NotePageViewModel()
        {
            
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Add commands or other methods as needed
    }
}
