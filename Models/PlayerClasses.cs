using System.Collections.Generic;

namespace SoftRes.Models
{
    public interface IPlayerClass
    {
        ClassRole Role { get; }
        ClassSpec Spec { get; }
        List<ClassSpec> Specs { get; }
    }

    public class Warrior : IPlayerClass
    {
        public ClassRole Role { get; }

        public ClassSpec Spec { get; }

        public List<ClassSpec> Specs { get; } = new List<ClassSpec>
        {
            ClassSpec.ProtectionWarrior,
            ClassSpec.FuryWarrior,
            ClassSpec.ArmsWarrior
        };
    }

    public class Paladin : IPlayerClass
    {
        public ClassRole Role { get; }

        public ClassSpec Spec { get; }

        public List<ClassSpec> Specs { get; } = new List<ClassSpec>
        {
            ClassSpec.ProtectionPaladin,
            ClassSpec.RetributionPaladin,
            ClassSpec.HolyPaladin
        };
    }

    public class Mage : IPlayerClass
    {
        public ClassRole Role { get; }

        public ClassSpec Spec { get; }

        public List<ClassSpec> Specs { get; } = new List<ClassSpec>
        {
            ClassSpec.FrostMage,
            ClassSpec.ArcaneMage,
            ClassSpec.FireMage
        };
    }

    public class Warlock : IPlayerClass
    {
        public ClassRole Role { get; }

        public ClassSpec Spec { get; }

        public List<ClassSpec> Specs { get; } = new List<ClassSpec>
        {
            ClassSpec.AfflictionWarlock,
            ClassSpec.DemonologyWarlock,
            ClassSpec.DestructionWarlock
        };
    }

    public class Druid : IPlayerClass
    {
        public ClassRole Role { get; }

        public ClassSpec Spec { get; }

        public List<ClassSpec> Specs { get; } = new List<ClassSpec>
        {

        };
    }

    public class Shaman : IPlayerClass
    {
        public ClassRole Role { get; }

        public ClassSpec Spec { get; }

        public List<ClassSpec> Specs { get; } = new List<ClassSpec>
        {
            ClassSpec.RestorationShaman,
            ClassSpec.EnhancementShaman,
            ClassSpec.ElementalShaman
        };
    }

    public class Rogue : IPlayerClass
    {
        public ClassRole Role { get; }

        public ClassSpec Spec { get; }

        public List<ClassSpec> Specs { get; } = new List<ClassSpec>
        {
            ClassSpec.AssasinationRogue,
            ClassSpec.CombatRogue,
            ClassSpec.SubeltyRogue
        };
    }

    public class Priest : IPlayerClass
    {
        public ClassRole Role { get; }

        public ClassSpec Spec { get; }

        public List<ClassSpec> Specs { get; } = new List<ClassSpec>
        {
            ClassSpec.HolyPriest,
            ClassSpec.DisciplinePriest,
            ClassSpec.ShadowPriest
        };
    }

    public class Hunter : IPlayerClass
    {
        public ClassRole Role { get; }

        public ClassSpec Spec { get; }

        public List<ClassSpec> Specs { get; } = new List<ClassSpec>
        {
            ClassSpec.SurvivalHunter,
            ClassSpec.MarksmanshipHunter,
            ClassSpec.BeastMasteryHunter
        };
    }

    public enum ClassRole
    {
        Tank,
        MeleeDamage,
        RangedDamage,
        Healer
    }

    public enum ClassSpec
    {
        ProtectionWarrior,
        ArmsWarrior,
        FuryWarrior,
        ProtectionPaladin,
        RetributionPaladin,
        HolyPaladin,
        BalanceDruid,
        FeralTankDruid,
        FeralCatDruid,
        RestorationDruid,
        FireMage,
        ArcaneMage,
        FrostMage,
        ShadowPriest,
        HolyPriest,
        DisciplinePriest,
        AfflictionWarlock,
        DestructionWarlock,
        DemonologyWarlock,
        MarksmanshipHunter,
        SurvivalHunter,
        BeastMasteryHunter,
        RestorationShaman,
        EnhancementShaman,
        ElementalShaman,
        CombatRogue,
        SubeltyRogue,
        AssasinationRogue,
    }
}