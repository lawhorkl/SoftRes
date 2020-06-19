using System;
using System.Collections.Generic;

namespace SoftRes.Models
{
    public class Raid
    {
        public List<Raider> Raiders { get; set; } = 
            new List<Raider>();
        public List<Item> Drops { get; set; } =
            new List<Item>();
        public DateTime Time { get; set; } =
            new DateTime();
    }

    public class Raider
    {
        public string Name { get; set; }
        public IPlayerClass Class { get; set; }
    }

    public class Item
    {
        public List<Raider> Reservers { get; set; } = 
            new List<Raider>();
    }
}