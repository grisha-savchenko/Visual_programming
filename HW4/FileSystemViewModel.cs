using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;


namespace HW4
{
   public class FileSystemViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FileSystemEntry> _fileSystemEntries;
        private FileSystemEntry _selectedEntry;

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
            LoadFileSystemEntries(Directory.GetCurrentDirectory());
        }

        public void LoadFileSystemEntries(string directoryPath)
        {
            CurrentDirectory = directoryPath;

            FileSystemEntries = new ObservableCollection<FileSystemEntry>();

            // Добавление ".." в качестве первого элемента, если это не корневая директория
            if (Directory.GetParent(directoryPath) != null)
            {
                FileSystemEntries.Add(new FileSystemEntry
                {
                    Name = "..",
                    Icon = new Bitmap("./Assets/dir_up.jpg"),
                    IsDirectory = true
                });
            }

            // Загрузка файлов и директорий
            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                FileSystemEntries.Add(new FileSystemEntry
                {
                    Name = Path.GetFileName(directory),
                    Icon = new Bitmap("./Assets/papka.png"),
                    IsDirectory = true
                });
            }

            foreach (var file in Directory.GetFiles(directoryPath))
            {
                FileSystemEntries.Add(new FileSystemEntry
                {
                    Name = Path.GetFileName(file),
                    Icon = new Bitmap("./Assets/file.png"),
                    IsDirectory = false
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
    }
}