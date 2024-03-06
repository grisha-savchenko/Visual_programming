using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using Avalonia.Media;
using System;
using System.Runtime;


namespace HW4
{
    public class FileSystemViewModel : INotifyPropertyChanged
    {

        private Bitmap iconFile = new Bitmap("D:\\repo\\HW4\\Assets\\file.png");
        private Bitmap iconDirectory = new Bitmap("D:\\repo\\HW4\\Assets\\papka.png");
        private Bitmap iconDirectoryUp = new Bitmap("D:\\repo\\HW4\\Assets\\dir_up.jpg");
        private Bitmap iconDisk = new Bitmap("D:\\repo\\HW4\\Assets\\disk.png");
        private ObservableCollection<FileSystemEntry> _fileSystemEntries;
        private FileSystemEntry _selectedEntry;

        private Bitmap _img;

        public void MyViewModel(string Img_path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Img_path);
            if (dirInfo.Exists)
                return;
            string path = dirInfo.FullName;
            try
            {
                Img = new Bitmap(path);
            }
            catch (Exception e)
            {

            }
        }

        public Bitmap Img
        {
            get => _img;
            set
            {
                _img = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FileSystemEntry> FileSystemEntries
        {
            get => _fileSystemEntries;
            set
            {
                _fileSystemEntries = value;
                OnPropertyChanged();
            }
        }

        public FileSystemEntry SelectedEntry
        {
            get => _selectedEntry;
            set
            {
                _selectedEntry = value;
                OnPropertyChanged();
            }
        }

        private string _currentDirectory;

        public string CurrentDirectory
        {
            get { return _currentDirectory; }
            set { SetField(ref _currentDirectory, value); }
        }

        public FileSystemViewModel()
        {
            LoadLogicalDrives();
        }


        public void LoadLogicalDrives()
        {
            FileSystemEntries = new ObservableCollection<FileSystemEntry>();

            CurrentDirectory = "";

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    FileSystemEntries.Add(new FileSystemEntry
                    {
                        Name = drive.Name,
                        Icon = iconDisk,
                        IsDirectory = true,
                        FullName = drive.Name
                    });
                }
            }
        }

        public void LoadFileSystemEntries(string directoryPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            if (!dirInfo.Exists)
                return;
            string path = dirInfo.FullName;
            CurrentDirectory = path;


            FileSystemEntries = new ObservableCollection<FileSystemEntry>();

            // Добавление ".." в качестве первого элемента, если это не корневая директория
                FileSystemEntries.Add(new FileSystemEntry
                {
                    Name = "..",
                    Icon = iconDirectoryUp,
                    IsDirectory = true,
                    FullName = dirInfo.FullName + "\\" + Path.GetFileName("..")
                });

            // Загрузка файлов и директорий
            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                FileSystemEntries.Add(new FileSystemEntry
                {
                    Name = Path.GetFileName(directory),
                    Icon = iconDirectory,
                    IsDirectory = true,
                    FullName = dirInfo.FullName + "\\" + Path.GetFileName(directory)
                });
            }

            foreach (var file in Directory.GetFiles(directoryPath))
            {
                FileSystemEntries.Add(new FileSystemEntry
                {
                    Name = Path.GetFileName(file),
                    Icon = iconFile,
                    IsDirectory = false,
                    FullName = dirInfo.FullName + "\\" +  Path.GetFileName(file)
                });
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class FileSystemEntry
    {
        public string Name { get; set; }
        public Bitmap Icon { get; set; }
        public bool IsDirectory { get; set; }
        public string FullName { get; set; }

    }
}