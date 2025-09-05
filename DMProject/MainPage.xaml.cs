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

        private string question1(string input)
        {
            // Split input into lines
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 2)
                return "ERROR: Invalid input!";

            // Read first line for station and rail counts
            string[] firstLine = lines[0].Trim().Split(' ');
            if (firstLine.Length < 2)
                return "ERROR: Invalid input!";

            if (!int.TryParse(firstLine[0], out int m) || !int.TryParse(firstLine[1], out int n))
                return "ERROR: Invalid numbers!";

            if (lines.Length < m + n + 2)
                return "ERROR: Not enough lines!";

            // Read stations
            var stations = new string[m];
            for (int i = 0; i < m; i++)
                stations[i] = lines[i + 1].Trim();

            // Build the graph
            var graph = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            foreach (var st in stations)
                graph[st] = new List<string>();

            for (int i = 0; i < n; i++)
            {
                string[] tokens = lines[m + 1 + i].Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 2)
                    continue;

                string a = tokens[0];
                string b = tokens[1];

                if (!graph.ContainsKey(a)) graph[a] = new List<string>();
                if (!graph.ContainsKey(b)) graph[b] = new List<string>();

                graph[a].Add(b);
                graph[b].Add(a);
            }

            // Start station
            string start = lines[m + n + 1].Trim();
            if (!graph.ContainsKey(start))
                return "ERROR: Invalid start station!";

            // BFS for shortest path
            var distance = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var st in graph.Keys)
                distance[st] = -1;

            var queue = new Queue<string>();
            distance[start] = 0;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                foreach (var neighbor in graph[current])
                {
                    if (distance[neighbor] == -1)
                    {
                        distance[neighbor] = distance[current] + 1;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            // Build output
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < m; i++)
            {
                if (i > 0) sb.AppendLine();
                sb.Append($"{stations[i]} {distance[stations[i]]}");
            }

            return sb.ToString();
        }

        private string question2(string input)
        {
            // Split input into lines
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 3)
                return "ERROR: Invalid input!";

            // Read first line for station and rail counts
            string[] firstLine = lines[0].Trim().Split(' ');
            if (firstLine.Length < 2)
                return "ERROR: Invalid input!";

            if (!int.TryParse(firstLine[0], out int m) || !int.TryParse(firstLine[1], out int n))
                return "ERROR: Invalid numbers!";

            if (lines.Length < m + n + 3)
                return "ERROR: Not enough lines!";

            // Read stations
            var stations = new string[m];
            for (int i = 0; i < m; i++)
                stations[i] = lines[i + 1].Trim();

            // Build weighted graph
            var graph = new Dictionary<string, List<(string, double)>>(StringComparer.OrdinalIgnoreCase);
            foreach (var st in stations)
                graph[st] = new List<(string, double)>();

            for (int i = 0; i < n; i++)
            {
                string[] tokens = lines[m + 1 + i].Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 3)
                    continue;

                string a = tokens[0];
                string b = tokens[1];
                if (!double.TryParse(tokens[2], out double w))
                    continue;

                if (!graph.ContainsKey(a)) graph[a] = new List<(string, double)>();
                if (!graph.ContainsKey(b)) graph[b] = new List<(string, double)>();

                graph[a].Add((b, w));
                graph[b].Add((a, w));
            }

            // Start and destination stations
            string start = lines[m + n + 1].Trim();
            string dest = lines[m + n + 2].Trim();

            if (!graph.ContainsKey(start) || !graph.ContainsKey(dest))
                return "-1";

            // Dijkstra’s algorithm with double
            var dist = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            var prev = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var pq = new SortedSet<(double, string)>();

            foreach (var st in graph.Keys)
                dist[st] = double.MaxValue;

            dist[start] = 0.0;
            pq.Add((0.0, start));

            while (pq.Count > 0)
            {
                var (d, u) = pq.Min;
                pq.Remove(pq.Min);

                if (u == dest) break; // Early exit

                foreach (var (v, w) in graph[u])
                {
                    double nd = d + w;
                    if (nd < dist[v])
                    {
                        if (dist[v] != double.MaxValue)
                            pq.Remove((dist[v], v));

                        dist[v] = nd;
                        prev[v] = u;
                        pq.Add((nd, v));
                    }
                }
            }

            if (dist[dest] == double.MaxValue)
                return "-1";

            // Reconstruct path
            var path = new Stack<string>();
            string cur = dest;
            while (cur != null && prev.ContainsKey(cur))
            {
                path.Push(cur);
                cur = prev[cur];
            }
            path.Push(start);

            // Output: distance first, then path
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(dist[dest].ToString("F2"));  // always prints with 2 decimal digit
            sb.Append(string.Join(" ", path));

            return sb.ToString();
        }



        private void OnSendClicked(object sender, EventArgs e)
        {
            string message = messageEditor.Text;
            // Manage the input here => probably the function:
            string output;
            if (questionNo == 1) { output = question1(message); }
            else {  output = question2(message); }
            
            // display the second box:
            outputEditor.Text = output; // Just an example of output

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
            outputEditor.Text = "";
        }

        private void onPastedQuestion (object sender, EventArgs e)
        {
            string testCase1 = "4 2\nShiraz\nTehran\nIsfahan\nMashhad\nShiraz Tehran\nMashhad Isfahan\nMashhad";
            string testCase2 = "5 6\nA\nB\nC\nD\nE\nE C 136.81\nD B 12.74\nC B 14.63\nB A 60.48\nA D 45.63\nA E 514.73\nA\nC";

            if (questionNo == 1) messageEditor.Text = testCase1;
            else if (questionNo == 2) messageEditor.Text = testCase2;
            else throw new ArgumentException("unexcpected question no.");

        }

        private void onClearInputClicked(object sender, EventArgs e)
        {
            messageEditor.Text = "";
            outputEditor.Text = "";
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
