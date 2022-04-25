using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class HealingPotion : Item
    {

        public int AmountToHeal { get; set; }

        public HealingPotion(int id, string name, string nameplural, int amounttoheal) : base(id, name, nameplural)
        {
            AmountToHeal = amounttoheal;
        }
    }
}
