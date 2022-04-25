using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Player : LivingCreature
    {
        // Status
        public int Gold { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level
        {
            get { return ((ExperiencePoints / 100) + 1); }
        }
        public int CurrentMana { get; set; }
        public int MaximumMana { get; set; }
        public int MaxFuel { get; set; }
        public int CurrentFuel { get; set; }

        // Inventory
        public List<InventoryItem> Inventory { get; set;}
        public List<PlayerQuest> Quests { get; set; }

        // Location
        public List<int> CurrentMapCoordinates { get; set; }
        public int CurrentLocationId {
            get {
                int x = CurrentMapCoordinates[0];
                int y = CurrentMapCoordinates[1];
                return Universe.map[y][x];
            }
        }
        public Location CurrentLocation
        {
            get
            {
                int x = CurrentMapCoordinates[0];
                int y = CurrentMapCoordinates[1];
                return Universe.LocationByID(Universe.map[y][x]);
            }
        }
        
        public Player(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints, int currentMana, int maximumMana, int maxFuel, int currentFuel) : base(currentHitPoints, maximumHitPoints)
        {
            Gold = gold;
            ExperiencePoints = experiencePoints;
            CurrentMana = currentMana;
            MaximumMana = maximumMana;
            MaxFuel = maxFuel;
            CurrentFuel = currentFuel;
            
            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();

            //Console.WriteLine(Universe.map);

            CurrentMapCoordinates = FindStartLocation();

            
        }
        public List<int> FindStartLocation()
        {
            for (int row = 0; row < Universe.map.Count; row++)
            {
                for (int col = 0; col < Universe.map[row].Count; col++)
                {
                    if (Universe.map[row][col] == 1)
                    {
                        return new List<int> { col, row };
                    }
                }
            }
            return null;
        }
        public List<int> GetUpCoordinates()
        {
            int x = CurrentMapCoordinates[0];
            int y = CurrentMapCoordinates[1];

            return new List<int> { x, y - 1 };
        }
        public bool CheckIfUpLocationExists()
        {
            int y = CurrentMapCoordinates[1];

            // Location top of map?
            if (y <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<int> GetDownCoordinates()
        {
            int x = CurrentMapCoordinates[0];
            int y = CurrentMapCoordinates[1];

            return new List<int> { x, y + 1 };
        }
        public bool CheckIfDownLocationExists()
        {
            int y = CurrentMapCoordinates[1];

            // Location top of map?
            if (y >= Universe.map.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if(location.ItemRequierdToEnter == null)
            {
                //there is no req item for this location so return true
                return true;
            }

            foreach(InventoryItem ii in Inventory)
            {
                if(ii.Details.ID == location.ItemRequierdToEnter.ID)
                {
                    return true;
                }
            }

            //we didnt fint the req item in inv
            return false;

        }

        public bool HasThisQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in Quests)
            {
                if(playerQuest.Details.ID == quest.ID)
                {
                    return true;
                }
            }

            return false ;
        }

        public bool CompletedThisQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in Quests)
            {
                if(playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }

            return false;
        }

        public bool HasAllQuestCompletionItems(Quest quest)
        {
            // se if the player has all the items needed for completion (quest)
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                bool foundItemInPlayersInventory = false;


                //check each item in inv and see if if its enough
                foreach (InventoryItem ii in Inventory)
                {
                    if(ii.Details.ID == qci.Details.ID) // The player has the item in their inventory
                    {
                        foundItemInPlayersInventory = true;

                        if(ii.Quantity < qci.Quantity)  // The player does not have enough of this item to complete the quest
                        {
                            return true;
                        }
                    }
                }
                if(!foundItemInPlayersInventory)
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                foreach (InventoryItem ii in Inventory)
                {
                    if(ii.Details.ID == qci.Details.ID)
                    {
                        // Subtract the quantity from the player's inventory that was needed to complete the quest
                        ii.Quantity -= qci.Quantity;
                        break;
                    }
                }
            }
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == itemToAdd.ID)
                {
                    // They have the item in their inventory, so increase the quantity by one
                    ii.Quantity++;

                    return; // We added the item, and are done, so get out of this function
                }
            }

            // They didn't have the item, so add it to their inventory, with a quantity of 1
            Inventory.Add(new InventoryItem(itemToAdd, 1));
        }

        public void MarkQuestCompleted(Quest quest)
        {
            // Find the quest in the player's quest list
            foreach (PlayerQuest pq in Quests)
            {
                if (pq.Details.ID == quest.ID)
                {
                    // Mark it as completed
                    pq.IsCompleted = true;

                    return; // We found the quest, and marked it complete, so get out of this function
                }
            }
        }
    }
}
