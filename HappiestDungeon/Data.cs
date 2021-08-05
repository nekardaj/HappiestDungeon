using System;
using System.Collections.Generic;

namespace HappiestDungeon
{
    static class Data
    {
        public static readonly List<int>[] Map = new List<int>[] //static layout, some nodes miss links that can be generated
        {
            new List<int>(){1,2}, new List<int>(){3}, new List<int>(){4,5}, new List<int>(){6,7},
            new List<int>(){},new List<int>(){8},new List<int>(){9},new List<int>(){9},
            new List<int>(){9} //boss node is implicitly created in map generation
        };
        public static readonly List<int>[] ToBeGenerated = new List<int>[] //holds nodes that should have some nodes added(node number,poential links)
        {
            new List<int>(){4,7,8},
        };
        


    }
}