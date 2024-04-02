using Avalonia;
using Avalonia.Controls.Primitives;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Input;

namespace HW9;

public class MySlider : TemplatedControl
{
    public static readonly StyledProperty<bool> IsActivatedProperty =
            AvaloniaProperty.Register<MySlider, bool>("IsActivated");
    public bool IsActivated
    {
        get { return GetValue(IsActivatedProperty); }
        set { SetValue(IsActivatedProperty, value); }
    }

    public void CloseButtonAction()
    {
        IsActivated = false;
    }

    public void RectangleTappedAction()
    {
        IsActivated = true;
    }
}