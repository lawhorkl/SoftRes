using System.Collections.Generic;

namespace SoftRes.Models
{
    public enum ClassRole
    {
        Tank,
        MeleeDamage,
        RangedDamage,
        Healer
    }

    public interface IPlayerClass
    {
        List<ClassRole> Roles { get; }
    }

    public class Warrior : IPlayerClass
    {
        public List<ClassRole> Roles { get; } = new List<ClassRole> 
        { 
            ClassRole.Tank, 
            ClassRole.MeleeDamage 
        };
    }

    public class Paladin : IPlayerClass
    {
        public List<ClassRole> Roles { get; } = new List<ClassRole> 
        { 
            ClassRole.Healer, 
            ClassRole.Tank, 
            ClassRole.MeleeDamage 
        };
    }

    public class Mage : IPlayerClass
    {
        public List<ClassRole> Roles { get; } = new List<ClassRole> 
        { 
            ClassRole.RangedDamage 
        };
    }

    public class Warlock : IPlayerClass
    {
        public List<ClassRole> Roles { get; } = new List<ClassRole> 
        { 
            ClassRole.RangedDamage 
        };
    }

    public class Druid : IPlayerClass
    {
        public List<ClassRole> Roles { get; } = new List<ClassRole> 
        { 
            ClassRole.Healer, 
            ClassRole.Tank, 
            ClassRole.MeleeDamage,
            ClassRole.RangedDamage 
        };
    }

    public class Shaman : IPlayerClass
    {
        public List<ClassRole> Roles { get; } = new List<ClassRole> 
        { 
            ClassRole.Healer, 
            ClassRole.MeleeDamage,
            ClassRole.RangedDamage 
        };
    }

    public class Rogue : IPlayerClass
    {
        public List<ClassRole> Roles { get; } = new List<ClassRole> 
        { 
            ClassRole.MeleeDamage
        };
    }

    public class Priest : IPlayerClass
    {
        public List<ClassRole> Roles { get; } = new List<ClassRole> 
        { 
            ClassRole.Healer,
            ClassRole.RangedDamage 
        };
    }

    public class Hunter : IPlayerClass
    {
        public List<ClassRole> Roles { get; } = new List<ClassRole> 
        { 
            ClassRole.RangedDamage 
        };
    }
}