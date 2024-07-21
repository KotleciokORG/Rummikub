using System;
using Avalonia.Input;
using Rummikub.src;

namespace Rummikub;

public class MyEventHandler
{
    public void PassButtonHandler()
    {
        App.MVMObject.RummikubGame.NextPlayer();
    }
    public void BlockPressedHandler(Block block, object parentGroupObj, PointerPressedEventArgs e)
    {
        var player = App.MVMObject.RummikubGame.ActivePlayer;
        if (block == null) throw new Exception("Null Block");

        if (player.HoldedBlock == null)
        {
            //Grab block
            if (parentGroupObj is Set_of_Blocks parentGroupSet)
            {
                player.Grab_Block(parentGroupSet, block);
                parentGroupSet.Update_correctness();
            }
            else if (parentGroupObj is Block_Group parentGroupGroup)
            {
                player.Grab_Block(parentGroupGroup, block);
            }
        }
        else
        {
            if (parentGroupObj is Set_of_Blocks parentGroupSet)
            {
                player.Put_Block(parentGroupSet, parentGroupSet.FindIndex(block));
                parentGroupSet.Update_correctness();
            }
            else if (parentGroupObj is Block_Group parentGroupGroup)
            {
                player.Put_Block(parentGroupGroup, parentGroupGroup.FindIndex(block));
            }
        }
    }
}