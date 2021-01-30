using System.Collections.Generic;

namespace Elvenar.StaticData
{
    public class ResearchTechnologies
    {
        public static List<Research> ResearchList { get; set; }

        public class Research
        {
            public string id { get; set; }
            public string race { get; set; }
            public string[] childrenIds { get; set; }
            public int level { get; set; }
            public string __class__ { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public int maxSP { get; set; }
            public Reward[] rewards { get; set; }
            public string[] parentIds { get; set; }
            public string category { get; set; }
            public int section { get; set; }
            public float expectedProductionBoost { get; set; }
            public Requirements requirements { get; set; }
            public float score { get; set; }
            public int premiumMax { get; set; }
            public int sharedAssets { get; set; }
            public Gate gate { get; set; }
            public string featureFlag { get; set; }
        }

        public class Requirements
        {
            public Resources resources { get; set; }
            public string __class__ { get; set; }
        }

        public class Resources
        {
            public int money { get; set; }
            public string __class__ { get; set; }
            public int supplies { get; set; }
            public int marble { get; set; }
            public int steel { get; set; }
            public int planks { get; set; }
            public int crystal { get; set; }
            public int scrolls { get; set; }
            public int silk { get; set; }
            public int elixir { get; set; }
            public int magic_dust { get; set; }
            public int gems { get; set; }
            public int dwarfs_granite { get; set; }
            public int dwarfs_copper { get; set; }
            public int fairies_ambrosia { get; set; }
            public int fairies_soma { get; set; }
        }

        public class Gate
        {
            public string completedProvinces { get; set; }
            public Rewards rewards { get; set; }
            public string __class__ { get; set; }
        }

        public class Rewards
        {
            public Resources1 resources { get; set; }
            public string __class__ { get; set; }
        }

        public class Resources1
        {
            public string spell_supply_production_boost_1 { get; set; }
            public string __class__ { get; set; }
            public string spell_neighborly_help_boost_1 { get; set; }
            public string spell_good_production_boost_1 { get; set; }
        }

        public class Reward
        {
            public string value { get; set; }
            public string type { get; set; }
            public string __class__ { get; set; }
        }
    }
}
