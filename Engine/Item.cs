﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
        // Healingpotion, Weapon base class
        public Item(int id, string name, string nameplural)
        {
            ID = id;
            Name = name;
            NamePlural = nameplural;
        }
    }
}
