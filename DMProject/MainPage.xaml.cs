// IMPORTANT: Used GPT for writing the logic of the fucntions (how to solve questions)

namespace DMProject
{
    public partial class MainPage : ContentPage
    {
        int isOutputVisible { get; set; } // for managing the output box visibility
        int questionNo { get; set; } = 1; // for managing the question number
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
        private void OnMessageEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            // Enable submit only if text is not empty/whitespace
            submitButton.IsEnabled = !string.IsNullOrWhiteSpace(e.NewTextValue);
            clearButton.IsEnabled = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }


        private void onChangedQuestion(object sender, EventArgs e)
        {
            string question1 = "The least number of train stations";
            string question2 = "The shortest path to the destination station";
            if (questionNo == 1)

            {
                questionNo = 2;
                headerLable.Text = "input: " + question2;
            }
            else if (questionNo == 2)
            {
                questionNo = 1;
                headerLable.Text = "input: " + question1;
            }
            else
                throw new ArgumentException("No sucha question");

            // Clear the input box:
            messageEditor.Text = "";
        }

        private void onPastedQuestion (object sender, EventArgs e)
        {
            string testCase1 = "4 2\nShiraz\nTehran\nIsfahan\nMashhad\nShiraz Tehran\nMashhad Isfahan\nMashhad";
            string testCase2 = "5 4\nA\nB\nC\nD\nE\nE C 136.81\nD B 12.74\nC B 14.63\nB A 60.48\nA D 45.63\nA E 514.73\nA\nC";

            if (questionNo == 1) messageEditor.Text = testCase1;
            else if (questionNo == 2) messageEditor.Text = testCase2;
            else throw new ArgumentException("unexcpected question no.");

        }

        private void onClearInputClicked(object sender, EventArgs e)
        {
            messageEditor.Text = "";
        }


        private void OnExitClicked(object sender, EventArgs e)
        {
        #if ANDROID
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid()); 
        #elif IOS
            // Apple discourages programmatic exit — you can only send app to background:
            UIKit.UIApplication.SharedApplication.PerformSelector(
                new ObjCRuntime.Selector("suspend"), null, 0);
        #elif WINDOWS
            Application.Current.Quit(); 
        #elif MACCATALYST
                    Application.Current.Quit();
        #endif
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
        #if WINDOWS || MACCATALYST
                    var exePath = Environment.ProcessPath;
                    System.Diagnostics.Process.Start(exePath);
                    Application.Current.Quit();
        #endif
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
