using IniParser;
using IniParser.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace DeloDatabaseSwitcher
{
    public partial class MainWindow : Window
    {
        public string Path { get; set; }

        private FileIniDataParser parser;
        private IniData data;

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
            }

            LoadAndParseIniFile();
        }

        private void LoadAndParseIniFile()
        {
            try
            {
                parser = new FileIniDataParser();
                data = parser.ReadFile(Path);

                ServerBox.Text = data["Database"]["ServerName"];
                DatabaseBox.Text = data["Database"]["Owner"];
                UsernameBox.Text = data["Database"]["LogId"];
                PwdBox.Text = data["Database"]["LogPassword"];

                AppSettings.Default.officepath = Path;
                AppSettings.Default.Save();
                PathLabel.Text = Path ?? "(file not selected)";
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot open selected file. Try to open another one.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                PathLabel.Text = Path ?? "(file not selected)";
            }
        }

        private void GithubLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Emestie/DeloDatabaseSwitcher");
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            //save new path and reload ini and parse it

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".ini";
            ofd.Filter = "OFFICE.INI|OFFICE.INI";
            if (ofd.ShowDialog() == true)
            {
                Path = ofd.FileName;
                LoadAndParseIniFile();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Path != null)
            {
                data["Database"]["ServerName"] = ServerBox.Text;
                data["Database"]["Owner"] = DatabaseBox.Text;
                data["Database"]["LogId"] = UsernameBox.Text;
                data["Database"]["LogPassword"] = PwdBox.Text;
                try
                {
                    parser.WriteFile(Path, data);
                    SaveOkBox.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot save file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
