namespace HorseRaceSimulation
{
    public partial class MainForm : Form
    {
        private const int NumberOfHorses = 5;
        private List<ProgressBar> horses = new List<ProgressBar>();
        private CancellationTokenSource cancellationTokenSource;

        public object Controls { get; private set; }
        public object MessageBox { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            InitializeHorses();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void InitializeHorses()
        {
            for (int i = 0; i < NumberOfHorses; i++)
            {
                ProgressBar progressBar = new ProgressBar();
                progressBar.Minimum = 0;
                progressBar.Maximum = 100;
                progressBar.Value = 0;
                progressBar.Width = 200;
                progressBar.Height = 20;
                progressBar.Margin = new Padding(0, 5, 0, 5);
                horses.Add(progressBar);
                Controls(progressBar);
            }
        }

        private void Controls(ProgressBar progressBar)
        {
            throw new NotImplementedException();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();
            await StartRaceAsync();
        }

        private async Task StartRaceAsync()
        {
            Random random = new Random();

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < NumberOfHorses; i++)
            {
                tasks.Add(Task.Run(() => RunHorse(i, random)));
            }

            await Task.WhenAny(Task.WhenAll(tasks), Task.Delay(20000));

            cancellationTokenSource.Cancel();

            ShowResults();
        }

        private void RunHorse(int horseIndex, Random random)
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested && horses[horseIndex].Value < horses[horseIndex].Maximum)
            {
                int step = random.Next(1, 6);
                horses[horseIndex].Invoke((MethodInvoker)(horses[horseIndex].Value += step));
                Thread.Sleep(random.Next(50, 200));
            }
        }

        private void ShowResults()
        {
            string results = "Results:\n";

            for (int i = 0; i < NumberOfHorses; i++)
            {
                results += $"Horse {i + 1}: {horses[i].Value}%\n";
            }

            MessageBox.Show(results);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            ShowResults();
        }
    }
}
