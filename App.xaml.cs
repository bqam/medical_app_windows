using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Newtonsoft.Json;
using static sample_project_wpf.MainWindow;
using System.Windows.Media;

namespace sample_project_wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string configFileName = "config.json";
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Create the main window
            MainWindow = new MainWindow();
            TextBox textBox = MainWindow.FindName("PatientTextBox") as TextBox;
            TextBox notesTextBox = MainWindow.FindName("NotesTextBox") as TextBox;
            if (File.Exists(configFileName))
            {
                string text = File.ReadAllText(configFileName);
                try
                {
                    var record = JsonConvert.DeserializeObject<Record>(text);
                    if (string.IsNullOrEmpty(record.PatientId) == false)
                    {
                        textBox.Text = record.PatientId;
                        textBox.Foreground = Brushes.Black;

                    }

                    if (string.IsNullOrEmpty(record.Notes) == false)
                    {
                        notesTextBox.Text = record.Notes;
                        notesTextBox.Foreground = Brushes.Black;

                    }
                }
                catch (Exception ex)
                {

                }
            }
            
            // Show the main window
            MainWindow.Show();
        }
    }
    
}
