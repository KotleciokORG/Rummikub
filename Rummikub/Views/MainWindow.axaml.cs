using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Rummikub.src;
using TestData;

namespace Rummikub.Views;

public partial class MainWindow : Window
{
    public MyEventHandler EH;

    public MainWindow()
    {
        InitializeComponent();
        EH = new MyEventHandler();
    }

    private T? FindParent<T>(Control? control) where T : class
    {
        while (control != null)
        {
            if (control is T t) return t;

            control = (Control)control.Parent;
        }

        return null;
    }


    private void BlockPressedEvent(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is Block block
                                    && FindParent<ItemsControl>(border).DataContext is object parentGroup)
            EH.BlockPressedHandler(block, parentGroup, e);
        else
            throw new Exception("Sender controller does not have border -> block\n");
    }

    public void PassButtonEvent(object sender, RoutedEventArgs args)
    {
        EH.PassButtonHandler();
    }

    public void DrawBlockEvent(object sender, RoutedEventArgs args)
    {
        EH.DrawBlockHandler();
    }

    public void MakeNewSuccSetEvent(object sender, RoutedEventArgs args)
    {
        EH.MakeNewSet(Test.nastepne);
    }

    public void MakeNewSameSetEvent(object sender, RoutedEventArgs args)
    {
        EH.MakeNewSet(Test.tesame);
    }
    
}