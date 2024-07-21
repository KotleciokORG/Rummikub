using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Rummikub.src;

public partial class Player(string name) : ObservableObject
{
    [ObservableProperty] private Block_Group _hand = new();
    [ObservableProperty] private Block? _holdedBlock;
    [ObservableProperty] private bool _is_active;
    [ObservableProperty] private string _name = name;
    [ObservableProperty] private int _points;

    //thats moving the block
    public void Grab_Block(Block_Group group, Block block)
    {
        if (HoldedBlock != null) throw new InvalidOperationException("Cannot grab block now! One is grabbed");
        ; //exception
        HoldedBlock = block;

        group.Remove(HoldedBlock);
    }

    public void Put_Block(Block_Group group, int index)
    {
        if (HoldedBlock == null) throw new InvalidOperationException("Cannot put block now! None is grabbed");
        ; //exception
        group.Insert(HoldedBlock, index);
        HoldedBlock = null;
    }

    public void SuckOnBlocks(Block_Group group)
    {
        while(group.Is_Empty() == false)
        {
            Grab_Block(group, group.Find(0));
            Put_Block(Hand,0);
        }
        
    }


    public string Description()
    {
        return string.Format("{0} with {1} points is {2} : {3} with {4} in hand", Name, Points.ToString(),
            Is_active ? "active" : "inactive", Hand.Description(),
            HoldedBlock == null ? "none" : HoldedBlock.Description());
    }
}