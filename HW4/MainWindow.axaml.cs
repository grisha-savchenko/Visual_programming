using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.IO;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;


namespace HW4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
        public void TapOnSquare(object sender, TappedEventArgs e)
{
        if (e.Source is Control listBoxItem && listBoxItem.DataContext is FileSystemEntry entry)
        {
            // Perform the action when a FileSystemEntry is tapped
            // For example, navigate to the directory if it's a directory
            if (entry.IsDirectory)
            {
                var viewModel = DataContext as FileSystemViewModel;
                if (viewModel != null)
                {
                    int len = viewModel.CurrentDirectory.Length;
                    string newPath = Path.Combine(viewModel.CurrentDirectory, entry.Name);
                    if(entry.Name == ".." && viewModel.CurrentDirectory[len-2] == ':' && viewModel.CurrentDirectory[len-1] == '\\')
                        viewModel.LoadLogicalDrives();
                    else
                        viewModel.LoadFileSystemEntries(newPath);
                }
            }
            else
            {
                var viewModel = DataContext as FileSystemViewModel;
                if (viewModel != null)
                {
                    string newPath = Path.Combine(viewModel.CurrentDirectory, entry.Name);
                    viewModel.LoadFileSystemEntries(newPath);
                }
            }
        }
}
    }
}
