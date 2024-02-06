using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Home_work_number_2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void Button_Click(object sender, RoutedEventArgs args)
    {
        if (sender is Button btn)
        {
            if (Color.TryParse(btn.Content?.ToString(), out var color))
            {
                rectangle.Fill = new SolidColorBrush(color);
            }
        }
    }
}