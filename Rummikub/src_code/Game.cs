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
    [ObservableProperty] private Player _activePlayer;
    
    private List<Player> _playerGhen { get; set; }
    private int _active_Player_Index;
    
    public Game()
    {
        BlockSetGhen = new ObservableCollection<Set_of_Blocks>();
        _playerGhen = new List<Player>();
        _active_Player_Index = 0;
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
        //_active_Player_Index++;
        //_active_Player_Index %= _playerGhen.Count;
        //ActivePlayer = _playerGhen[_active_Player_Index];
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