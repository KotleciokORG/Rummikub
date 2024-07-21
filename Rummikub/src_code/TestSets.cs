using System;
using System.Collections.ObjectModel;
using Rummikub.src;

namespace TestData;

public static class Test
{
    public static bool tesame(ObservableCollection<Block> set)
    {
        //0 1
        if (set.Count < 2) return true;
        if (!(set[0].Colour != set[1].Colour && set[0].Number == set[1].Number))
            return false;
        if (set.Count == 2) return true;
        if (!(set[0].Colour != set[2].Colour && set[0].Number == set[2].Number
                                             && set[1].Colour != set[2].Colour && set[1].Number == set[2].Number))
            return false;
        if (set.Count == 3) return true;
        if (!(set[0].Colour != set[3].Colour && set[0].Number == set[3].Number
                                             && set[1].Colour != set[3].Colour && set[1].Number == set[3].Number
                                             && set[2].Colour != set[3].Colour && set[2].Number == set[3].Number))
            return false;
        if (set.Count == 4) return true;
        Console.WriteLine("Nie dziala\n");
        return false; //should not happen
    }

    public static bool nastepne(ObservableCollection<Block> set)
    {
        if (set.Count < 2) return true;
        for (var i = 1; i < set.Count; i++)
        {
            if (set[i].Colour != set[i - 1].Colour) return false;

            if (set[i].Number - 1 != set[i - 1].Number) return false;
        }

        return true;
    }

    public static Set_of_Blocks GenerujSuccSet(int from, int to, Block.COLOUR colour)
    {
        var ret = new Set_of_Blocks(nastepne);
        for (var i = from; i <= to; i++)
        {
            var blok = new Block(i, colour);
            ret.Add(blok);
        }

        return ret;
    }

    public static Set_of_Blocks GenerujSameSet(int number, int size)
    {
        var ret = new Set_of_Blocks(tesame);
        for (var i = 0; i < size; i++)
        {
            var blok = new Block(number, (Block.COLOUR)i);
            ret.Add(blok);
        }

        return ret;
    }

    public static Set_of_Blocks GenerujRandomSet()
    {
        var rand = new Random();
        if (rand.Next(0, 2) != 0)
        {
            //succ
            int from = rand.Next(1, 12);
            int to = rand.Next(from, 13);
            return GenerujSuccSet(from, to, (Block.COLOUR)rand.Next(0, 3));
        }
        else
        {
            //same
            return GenerujSameSet(rand.Next(1,13), rand.Next(2,4));
        }
    }

    public static Block RandomBlock()
    {
        var rand = new Random();
        var ret = new Block(rand.Next(1, 13), (Block.COLOUR)rand.Next(0, 4));
        return ret;
    }

    public static Block_Group GenerujRandomGroup(int size)
    {
        var ret = new Block_Group();
        for (var i = 0; i < size; i++)
            ret.Add(RandomBlock());
        return ret;
    }
}