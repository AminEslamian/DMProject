namespace DMProject
{
    public partial class MainPage : ContentPage
    {
        int isOutputVisible; // for managing the output box visibility
        int questionNo = 1; // for managing the question number
        public MainPage() 

        {
            InitializeComponent();
        }

        private void OnSendClicked(object sender, EventArgs e)
        {
            string testCase1 = "4 2\nShiraz\nTehran\nIsfahan\nMashhad\nShiraz Tehran\nMashhad Isfahan\nMashhad";
            string testCase2 = "5 4\nA\nB\nC\nD\nE\nE C 136.81\nD B 12.74\nC B 14.63\nB A 60.48\nA D 45.63\nA E 514.73\nA\nC";

            string message = messageEditor.Text;
            // Manage the input here => probably the function:
            // --
            // display the second box:
            outputEditor.Text = "Output: " + message; // Just an example of output

        }

        private void onChangedQuestion(object sender, EventArgs e)
        {
            if (questionNo == 1)
                questionNo = 2;
            else if (questionNo == 2)
                questionNo = 1;
            else
                throw new ArgumentException("No sucha question");
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
