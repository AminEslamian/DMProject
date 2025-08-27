namespace DMProject
{
    public partial class MainPage : ContentPage
    {
        int isOutputVisible; // for managing the output box visibility
        public MainPage() 

        {
            InitializeComponent();
        }

        private void OnSendClicked(object sender, EventArgs e)
        {
            string message = messageEditor.Text;
            // Manage the input here => probably the function:
            // --
            // display the second box:
            outputEditor.Text = "Output: " + message; // Just an example of output

        }
        
        private void OnChangeThemeClicked(object sender, EventArgs e)
        {
            if (Application.Current.RequestedTheme == AppTheme.Light)
                Application.Current.UserAppTheme = AppTheme.Dark;
            else
                Application.Current.UserAppTheme = AppTheme.Light;
        }

    }

}
