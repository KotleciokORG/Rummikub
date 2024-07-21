using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Rummikub.ViewModels;
using Rummikub.Views;

namespace Rummikub;

public class App : Application
{
    public static MainViewModel MVMObject { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            MVMObject = new MainViewModel();
            desktop.MainWindow = new MainWindow
            {
                DataContext = MVMObject
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}