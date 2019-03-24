using System.Windows;
using System.Windows.Input;

namespace DeloDatabaseSwitcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Path { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadSettingsAndData();
        }

        private void LoadSettingsAndData()
        {
            var path = AppSettings.Default.officepath;
            if (path != null)
            {
                Path = path;
                PathLabel.Text = Path;
            }

            //load ini and parse it
        }

        private void GithubLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Emestie/DeloDatabaseSwitcher");
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            //save new path and reload ini and parse it
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Server = ServerBox.Text;
            Database = DatabaseBox.Text;
            //save ini here
        }
    }
}
