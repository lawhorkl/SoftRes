using System;
using System.Collections.Generic;

namespace SoftRes.Models
{
    public enum RaidInstance
    {
        MoltenCore,
        BlackwingLair,
        ZulGurub,
        AhnQuiraj
    }

    public class Raid
    {
        public List<Raider> Raiders { get; set; } = 
            new List<Raider>();
        public List<Drop> Drops { get; set; } =
            new List<Drop>();
        public DateTime Time { get; set; } =
            new DateTime();
        public RaidInstance Instance { get; set; }
    }

    public class Raider
    {
        public string Name { get; set; }
        public IPlayerClass Class { get; set; }
    }

    public enum ItemQuality
    {
        Poor,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public class Item
    {
        public int Id;
        public string Name;
        public ItemQuality Quality;
        public RaidInstance Instance;
    }

    public class Drop
    {
        public Item Item { get; set; }
        public List<Raider> Reservers { get; set; } = 
            new List<Raider>();
    }
}