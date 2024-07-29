namespace Notes;

public partial class NotePage : ContentPage
{
    public NotePage()
    {
        InitializeComponent();
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as NoteViewModel;
        if (viewModel != null)
        {
            await viewModel.SaveNoteAsync();
        }
        await Navigation.PopAsync();
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as NoteViewModel;
        if (viewModel != null)
        {
            await viewModel.DeleteNoteAsync();
        }
        await Navigation.PopAsync();
    }
}
