using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Rummikub.src;

namespace Rummikub.ViewModels;

public partial class MainViewModel : ObservableObject
{

    [ObservableProperty] private Game _rummikubGame;

    public MainViewModel()
    {
        RummikubGame = new Game();
        //Gener data
        RummikubGame.AddPlayer("Kacper");
        RummikubGame.AddPlayer("Eliza");
        RummikubGame.AddPlayer("Maciek");
        
        RummikubGame.Start();
        
    }
}