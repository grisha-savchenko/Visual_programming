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
            if (sender is ListBox listBox && listBox.DataContext is FileSystemViewModel collection)
            {
                if (e.Source is Control control && control.DataContext is FileSystemEntry file)
                {
                    var viewModel = DataContext as FileSystemViewModel;
                    if (viewModel != null)
                    {
                        string newPath = Path.Combine(viewModel.CurrentDirectory, file.Name);
                        viewModel.LoadFileSystemEntries(newPath);
                    }
                }
            }
        }
    //     public void     TapOnSquare(object sender, TappedEventArgs e)
    //     {
    //         if (e.Source is ListBoxItem listBoxItem && listBoxItem.DataContext is FileSystemEntry entry)
    //         {
    //             if (entry.IsDirectory)
    //             {
    //                 var viewModel = DataContext as FileSystemViewModel;
    //                 if (viewModel != null)
    //                 {
    //                     string newPath = Path.Combine(viewModel.CurrentDirectory, entry.Name);
    //                     viewModel.LoadFileSystemEntries(newPath);
    //                 }
    //             }
    //         }
    //     }
    }
}
