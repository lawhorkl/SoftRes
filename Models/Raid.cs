using System;
using System.Collections.Generic;

namespace SoftRes.Models
{
    public class Raid
    {
        public List<Raider> Raiders { get; set; }
        public List<Item> Drops { get; set; }
        public DateTime Time { get; set; }
    }

    public class Raider
    {
        public string Name { get; set; }
        public IPlayerClass Class { get; set; }
    }

    public class Item
    {
        public List<Raider> Reservers { get; set; }
    }
}