using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower
{
    public static class Globals
    {
        public static int Money;
        public static int Health;

        public const uint TAG_NONE =  0;
        public const uint TAG_ENEMY = (1 << 0);
        public const uint TAG_TOWER_PLOT = (1 << 1);
        public const uint TAG_PLAYER = (1 << 2);
        public const uint TAG_TOWER = (1 << 3);
        public const uint TAG_SOLID = (1 << 4);
        public const uint TAG_SHOP = (1 << 5);
    }
}
