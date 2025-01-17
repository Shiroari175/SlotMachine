using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine
{
    public enum Symbol
    {
        Default = 0,
        Cherry = 1,
        Plain = 2,
        Watermelon = 3,
        Bell = 4,
        BAR = 5,
        Seven = 6,
    }

    internal static class Reel
    {
        public static Symbol[] arrayReel1 = {
            Symbol.Default,
            Symbol.Cherry,
            Symbol.Plain,
            Symbol.Seven,
            Symbol.Watermelon,
            Symbol.Cherry,
            Symbol.Plain,
            Symbol.BAR,
            Symbol.Cherry,
            Symbol.Watermelon,
            Symbol.Bell,
            Symbol.Plain,
            Symbol.Watermelon,
            Symbol.Bell,
            Symbol.Plain,
            Symbol.Watermelon,
            Symbol.Seven,
            Symbol.Watermelon,
            Symbol.Bell,
            Symbol.Plain,
            Symbol.Watermelon,
        };

        public static Symbol[] arrayReel2 = {
            Symbol.Default,
            Symbol.Cherry,
            Symbol.Plain,
            Symbol.Cherry,
            Symbol.Seven,
            Symbol.Watermelon,
            Symbol.Bell,
            Symbol.Plain,
            Symbol.Cherry,
            Symbol.Bell,
            Symbol.Plain,
            Symbol.Cherry,
            Symbol.Bell,
            Symbol.Plain,
            Symbol.Watermelon,
            Symbol.BAR,
            Symbol.Bell,
            Symbol.Plain,
            Symbol.Watermelon,
            Symbol.BAR,
            Symbol.Cherry,
        };

        public static Symbol[] arrayReel3 = {
            Symbol.Default,
            Symbol.Watermelon,
            Symbol.Plain,
            Symbol.Seven,
            Symbol.Cherry,
            Symbol.Bell,
            Symbol.Watermelon,
            Symbol.Plain,
            Symbol.Bell,
            Symbol.Watermelon,
            Symbol.Plain,
            Symbol.Bell,
            Symbol.Watermelon,
            Symbol.Plain,
            Symbol.Cherry,
            Symbol.Cherry,
            Symbol.Bell,
            Symbol.BAR,
            Symbol.Plain,
            Symbol.Cherry,
            Symbol.Plain,
        };

    }
}
