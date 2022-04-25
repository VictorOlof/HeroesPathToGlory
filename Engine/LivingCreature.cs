using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class LivingCreature
    {
        public int CurrentHitPoints { get; set; }
        public int MaximumHitPoints { get; set;}


        // Player and monster base class
        public LivingCreature(int currenthitpoints, int maximumhitpoints)
        {
            CurrentHitPoints = currenthitpoints;
            MaximumHitPoints = maximumhitpoints;
;
        }
    }
}
