using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TestData;

namespace Rummikub.src;

public partial class Game : ObservableObject
{
    //ghen is a major group on board
    [ObservableProperty] private ObservableCollection<Set_of_Blocks> _blockSetGhen;
    
    
    
    //[ObservableProperty] private Player _activePlayer;
    //generated code
    //needed instead of ObservableProperty for changing setter
    private Player _activePlayer;
    public Player ActivePlayer
    {
        get => _activePlayer;
        set
        {
            if (!EqualityComparer<Player>.Default.Equals(_activePlayer, value))
            {
                Player oldValue = _activePlayer;
                OnNameChanging(value);
                OnNameChanging(oldValue, value);
                OnPropertyChanging();
                _activePlayer = value;

                _old_hand_size = _activePlayer.Hand.Blocks.Count;
                
                OnNameChanged(value);
                OnNameChanged(oldValue, value);
                OnPropertyChanged();
            }
        }
    }

    partial void OnNameChanging(Player value);
    partial void OnNameChanged(Player value);

    partial void OnNameChanging(Player oldValue, Player newValue);
    partial void OnNameChanged(Player oldValue, Player newValue);
    //end of generated code
    
    
    private List<Player> _playerGhen { get; set; }
    private int _active_Player_Index;
    public bool CanBePassed { get; set; }
    public bool CanDraw { get; set; }
    private int _old_hand_size;
    
    public Game()
    {
        BlockSetGhen = new ObservableCollection<Set_of_Blocks>();
        _playerGhen = new List<Player>();
        _active_Player_Index = 0;
    }

    public void UpdatePassInfo()
    {
        if (_old_hand_size > ActivePlayer.Hand.Blocks.Count) CanBePassed = true;
    }

    public void GenerateBoard()
    {
        BlockSetGhen.Clear();
        var rand = new Random();
        for (int i = 0; i < rand.Next(5, 10); i++)
        {
            AddBlockSet(Test.GenerujRandomSet());
        }
    }

    public void Start()
    {
        ActivePlayer = _playerGhen[_active_Player_Index];
        GenerateBoard();
        CanBePassed = false;
        CanDraw = true;
    }
    
    public void EndTour()
    {
        
        foreach (Set_of_Blocks InterestingSet in BlockSetGhen)
        {
            if (InterestingSet.Correct == false)
            {
                ActivePlayer.SuckOnBlocks(InterestingSet);
            }
        }

    }
    public void NextPlayer()
    {
        EndTour();
        _active_Player_Index++;
        _active_Player_Index %= _playerGhen.Count;
        ActivePlayer = _playerGhen[_active_Player_Index];
        CanBePassed = false;
        CanDraw = true;
    }

    public void AddBlockSet(Set_of_Blocks BSet)
    {
        BlockSetGhen.Add(BSet);
    }

    public void AddPlayer(string name)
    {
        var Gracz = new Player(name);
        Gracz.Hand = Test.GenerujRandomGroup(10);
        _playerGhen.Add(Gracz);
    }
    

    public void Draw()
    {
        foreach (var ghen in BlockSetGhen)
        {
            Console.Write(ghen.Description());
            Console.WriteLine();
        }

        foreach (var player in _playerGhen)
        {
            Console.Write(player.Description());
            Console.WriteLine();
        }
    }
}