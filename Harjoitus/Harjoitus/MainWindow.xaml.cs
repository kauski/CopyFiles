using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
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
using System.Windows.Forms;
namespace Harjoitus
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputCopyFiles(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                //User selects installation folder
                dialog.Description = "Select installation folder";
                DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    
                    string destinationFolder = dialog.SelectedPath;
                    string sourceDirectory = @".\Tiedostot";

                    //Check whether user wants to start installation to specified folder
                    MessageBoxResult confirmResult = System.Windows.MessageBox.Show($"Do you want to install to the selected folder?\n\nDestination Folder: {destinationFolder}", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (confirmResult == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Copy(sourceDirectory, destinationFolder);
                        }
                        catch (Exception er)
                        {
                            Console.WriteLine($"Error at copying files: {er.Message}");
                        }
                    }
                    else
                    {
                        System.Windows.Application.Current.Shutdown();
                    }
                   
                }

            }
            //Once copying completed, open completed window
            Completed comp = new Completed();
            comp.Show();
            Close();

        }

        private void Copy(string from, string destination)
        {
            //Handle file copying
            Console.WriteLine(@"Copying... {0} ... Please wait ", from);
            FileSystem.CopyDirectory(from, destination, UIOption.AllDialogs);
        }
    }
}