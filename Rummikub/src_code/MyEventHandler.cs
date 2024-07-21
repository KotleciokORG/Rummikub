using System;
using Avalonia.Input;
using Rummikub.src;
using TestData;

namespace Rummikub;

public class MyEventHandler
{
    private Game RGame = App.MVMObject.RummikubGame;
    public void PassButtonHandler()
    {
        if (RGame.ActivePlayer.HoldedBlock != null)
        {
            RGame.ActivePlayer.Put_Block(RGame.ActivePlayer.Hand,0);
        }
        RGame.UpdatePassInfo();
        if(RGame.CanBePassed)
            RGame.NextPlayer();
    }

    public void MakeNewSet(Set_of_Blocks.Condition cond)
    {
        if (RGame.ActivePlayer.HoldedBlock == null) return;
        Set_of_Blocks fresh = new Set_of_Blocks(cond);
        RGame.ActivePlayer.Put_Block(fresh,0);
        fresh.Update_correctness();
        RGame.BlockSetGhen.Add(fresh);
    }
    public void DrawBlockHandler()
    {
        if (RGame.CanDraw)
        {
            RGame.ActivePlayer.Hand.Add(Test.RandomBlock());
            RGame.CanBePassed = true;
            RGame.CanDraw = false;
        }
    }
    public void BlockPressedHandler(Block block, object parentGroupObj, PointerPressedEventArgs e)
    {
        var player = RGame.ActivePlayer;
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