namespace Notes;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}
    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://github.com/Haitham-Burhan/6002CEM_Haitham-Burhan");
    }
}