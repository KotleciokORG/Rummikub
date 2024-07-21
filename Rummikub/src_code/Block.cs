using System;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Rummikub.src;

public partial class Block : ObservableObject //should be fine with observable
{
    public enum COLOUR
    {
        RED,
        BLUE,
        GREEN,
        BLACK
    }

    [ObservableProperty] private IImmutableSolidColorBrush _backgroundColour;
    [ObservableProperty] private IImmutableSolidColorBrush _brushColour;

    private COLOUR _colour;
    [ObservableProperty] private int _number;

    public Block(int number, COLOUR input_colour)
    {
        Colour = input_colour;
        Number = number;
        SetDrawColour(input_colour);
    }

    public COLOUR Colour
    {
        get => _colour;
        set
        {
            SetProperty(ref _colour, value);
            SetDrawColour(value);
        }
    }

    public void SetDrawColour(COLOUR col)
    {
        switch (col)
        {
            case COLOUR.RED:
                BrushColour = new ImmutableSolidColorBrush(Colors.Red);
                BackgroundColour = new ImmutableSolidColorBrush(Colors.PaleVioletRed);
                break;
            case COLOUR.BLACK:
                BrushColour = new ImmutableSolidColorBrush(Colors.Black);
                BackgroundColour = new ImmutableSolidColorBrush(Colors.DarkSlateGray);
                break;
            case COLOUR.BLUE:
                BrushColour = new ImmutableSolidColorBrush(Colors.DarkBlue);
                BackgroundColour = new ImmutableSolidColorBrush(Colors.DodgerBlue);
                break;
            case COLOUR.GREEN:
                BrushColour = new ImmutableSolidColorBrush(Colors.Green);
                BackgroundColour = new ImmutableSolidColorBrush(Colors.LawnGreen);
                break;
        }
    }

    public string Description()
    {
        return Colour + " " + Number;
    }
}

public class Regular_Joker; //not implemented yet

public partial class Block_Group : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Block> _blocks;

    public Block_Group()
    {
        Blocks = new ObservableCollection<Block>();
    }

    public void Add(Block block)
    {
        Blocks.Add(block);
    }

    public bool Is_Empty()
    {
        if (Blocks.Count == 0) return true;
        return false;
    }
    public void Insert(Block block, int index)
    {
        Blocks.Insert(index, block);
    }

    public void Remove(Block block)
    {
        Blocks.Remove(block);
    }

    public void RemoveAt(int index)
    {
        Blocks.RemoveAt(index);
    }

    public Block Find(int index)
    {
        return Blocks[index];
    }

    public int FindIndex(Block block)
    {
        return Blocks.IndexOf(block);
    }

    public void Clear()
    {
        Blocks.Clear();
    }

    public void Randomize(int size)
    {
        Clear();
        var rnd = new Random();
        for (var i = 0; i < size; i++)
        {
            var rndCol = (Block.COLOUR)rnd.Next(0, 4);
            Add(new Block(rnd.Next(1, 14), rndCol));
        }
    }

    public string Description()
    {
        var desc = "";
        foreach (var el in Blocks) desc += el.Description() + " | ";
        return desc;
    }
}

public partial class Set_of_Blocks(Set_of_Blocks.Condition cond) : Block_Group
{
    public delegate bool Condition(ObservableCollection<Block> set);

    [ObservableProperty] private IImmutableSolidColorBrush _backgroundColor;
    private readonly Condition condition = cond;
    public bool Correct { get; set; }

    public string Desc { get; set; }

    //i dont know if primary constructors calls the base() constructor, but i hope so
    //public Set_of_Blocks(Condition cond) : base() {
    //    condition = cond;
    //}  
    public void Update_correctness()
    {
        Correct = condition(Blocks) && Blocks.Count >= 3;
        if (Correct) BackgroundColor = new ImmutableSolidColorBrush(Colors.PaleGreen);
        else BackgroundColor = new ImmutableSolidColorBrush(Colors.PaleVioletRed);
    }

    public new void Add(Block block)
    {
        base.Add(block);
        Update_correctness();
    }

    public new void Insert(Block block, int index)
    {
        base.Insert(block, index);
        Update_correctness();
    }

    public new void Remove(Block block)
    {
        base.Remove(block);
        Update_correctness();
    }

    public new void RemoveAt(int index)
    {
        base.RemoveAt(index);
        Update_correctness();
    }

    public new string Description()
    {
        return base.Description() + (Correct ? "Correct Set\n" : "Incorrect Set\n");
    }


    public Tuple<Set_of_Blocks, Set_of_Blocks> Split(int index)
    {
        var first = new Set_of_Blocks(condition);
        var second = new Set_of_Blocks(condition);

        for (var i = 0; i < Blocks.Count; i++)
            if (i <= index)
                first.Add(Blocks[i]);
            else
                second.Add(Blocks[i]);

        first.Update_correctness();
        second.Update_correctness();

        var ret = new Tuple<Set_of_Blocks, Set_of_Blocks>(first, second);
        ;
        return ret;
    }
}