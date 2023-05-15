using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Controls.Primitives;
using System.Windows.Shell;

namespace sample_project_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public class Record
        {
            public string PatientId;
            public string Notes;
        }
        private static string configFileName = "config.json";
        private bool isDialogOpen = false;
        

        



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            isDialogOpen = true;
            //Create the custom dialog window
            Window dialog = new Window()
            {
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ShowInTaskbar = false,
                AllowsTransparency = true,
                Background = Brushes.White,
                Foreground = Brushes.LightGray
            };
           

            string hexColorCode = "#FF908FAB"; // Example hex color code for red
            BrushConverter brushConverter = new BrushConverter();
            Brush brush = (Brush)brushConverter.ConvertFrom(hexColorCode);
            // Create the controls
            TextBlock promptText = new TextBlock()
            {
                Text = "Close Application",
                Margin = new Thickness(10),
                FontSize = 16,
                Foreground = brush,
                FontWeight = FontWeights.Bold
            };

            PasswordBox passwordBox = new PasswordBox()
            {
                Margin = new Thickness(0,10,0,10),
                Width = 200,
                Height = 30,                
            };
            string hexColorCode1 = "#FF2D7243"; // Example hex color code for red
            BrushConverter brushConverter1 = new BrushConverter();
            Brush? brush1 = brushConverter.ConvertFrom(hexColorCode1) as Brush;
            Button proceedButton = new Button()
            {
                Content = "Proceed",
                Width = 80,
                Height = 30,
                Margin = new Thickness(0),
                IsDefault = true,
                Foreground = Brushes.White,
                Background = brush1

            };

            // Handle the button click event
            proceedButton.Click += (s, args) =>
            {
                if (passwordBox.Password == "123")
                {
                    // Close the application
                    Application.Current.Shutdown();
                }
                else
                {
                    // Password is incorrect, do nothing or show an error message
                }

                // Close the dialog
                dialog.Close();
            };

            // Create the main layout panel
            StackPanel panel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            StackPanel panel1 = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Add the controls to the panel
            panel.Children.Add(promptText);
            panel1.Children.Add(passwordBox);
            panel1.Children.Add(proceedButton);
            panel.Children.Add(panel1);

            Border border = new Border()
            {
               Background = Brushes.LightGray,
                CornerRadius = new CornerRadius(20),
                Padding = new Thickness(10),
                Child = panel
            };
            dialog.Content = border;
            // Set the panel as the content of the dialog
           


            bool isClosing = false; // Flag to track if the dialog is closing

            // Handle the Closing event of the dialog's window
            dialog.Closing += (s, args) =>
            {
                isClosing = true;
            };

            // Add a preview mouse down event handler to the dialog's window
            dialog.Deactivated += (s, args) =>
            {
                if (!isClosing)
                {
                    // Close the dialog
                    dialog.Close();
                }

            };
            // Show the dialog
            dialog.ShowDialog();
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredPassword = passwordTextBox.Text.Trim();

            if (enteredPassword == "123")
            {
                // Close the application if the correct password is entered
                Application.Current.Shutdown();
            }
            else
            {
                // Close the pop-up if the wrong password is entered
                passwordPopup.IsOpen = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ContextMenu contextMenu = button.ContextMenu;

            if (contextMenu != null)
            {
                contextMenu.PlacementTarget = button;
                contextMenu.IsOpen = true;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Record record = new Record();
            record.PatientId = PatientTextBox.Text == "3-UG-1234" ? string.Empty : PatientTextBox.Text;
            record.Notes = NotesTextBox.Text;
            File.WriteAllText(configFileName, JsonConvert.SerializeObject(record));
        }
        private void PatientTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "3-UG-1234")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void PatientTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "3-UG-1234";
                textBox.Foreground = Brushes.Gray;
            }
        }
        private void NotesTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Notes about patient")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void NotesTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Notes about patient";
                textBox.Foreground = Brushes.Gray;
            }
        }
        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!NotesTextBox.IsMouseOver && !PatientTextBox.IsMouseOver)
            {
                Keyboard.ClearFocus();
            }
        }
    }
}
