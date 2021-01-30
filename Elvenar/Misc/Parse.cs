using System.Collections.Generic;

namespace Elvenar.Misc
{
    public class ServerRequestVO
    {
        public List<object> requestData { get; set; }
        public string requestClass { get; set; }
        public string requestMethod { get; set; }
        public int requestId { get; set; }
        public string __clazz__ { get; set; }
    }

    public class ServerResponseVO
    {
        public object responseData { get; set; }
        public string requestClass { get; set; }
        public string requestMethod { get; set; }
        public int requestId { get; set; }
        public string __class__ { get; set; }
    }

    public class StartupVO
    {
        public City_PopulationVO city_population { get; set; }
        public GuildVO guild { get; set; }
        public CityResourceVO resources { get; set; }
        public SeasonalEventVO[] seasonal_events { get; set; }
        public CityUserDataVO user_data { get; set; }
        public string season { get; set; }
        public string __class__ { get; set; }
    }

    public class City_PopulationVO
    {
        public int currentPopulation { get; set; }
        public int currentPopulationDemand { get; set; }
        public string __class__ { get; set; }
    }

    public class GuildVO
    {
        public int id { get; set; }
        public string name { get; set; }
        public GuildBannerVO banner { get; set; }
        public GuildMembershipVO[] members { get; set; }
        public bool invitation_allowed { get; set; }
        public bool application_allowed { get; set; }
        public string member_acquisition_type { get; set; }
        public int created_at { get; set; }
        public int rank { get; set; }
        public int points { get; set; }
        public int fellowship_rank { get; set; }
        public int fellowship_points { get; set; }
        public string __class__ { get; set; }
    }

    public class GuildBannerVO
    {
        public string shapeId { get; set; }
        public int shapeColor { get; set; }
        public string symbolId { get; set; }
        public int symbolColor { get; set; }
        public string __class__ { get; set; }
    }

    public class GuildMembershipVO
    {
        public int role_id { get; set; }
        public int rank { get; set; }
        public string score { get; set; }
        public GuildMemberVO player { get; set; }
        public int joined_at { get; set; }
        public bool hasAncientWonder { get; set; }
        public string __class__ { get; set; }
    }

    public class GuildMemberVO
    {
        public int player_id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string race { get; set; }
        public string __class__ { get; set; }
    }

    public class CityResourceVO
    {
        public Resources resources { get; set; }
        public Strategy_PointsVO strategy_points { get; set; }
        public string __class__ { get; set; }
    }

    public class Resources
    {
        public int marble { get; set; }
        public int planks { get; set; }
        public int steel { get; set; }
        public int crystal { get; set; }
        public int scrolls { get; set; }
        public int silk { get; set; }
        public int elixir { get; set; }
        public int magic_dust { get; set; }
        public int gems { get; set; }
        public int relic_marble { get; set; }
        public int relic_planks { get; set; }
        public int relic_steel { get; set; }
        public int relic_crystal { get; set; }
        public int relic_scrolls { get; set; }
        public int relic_silk { get; set; }
        public int relic_elixir { get; set; }
        public int relic_magic_dust { get; set; }
        public int relic_gems { get; set; }
        public int premium { get; set; }
        public int supplies { get; set; }
        public int population { get; set; }
        public int money { get; set; }
        public int blueprint { get; set; }
        public int spell_teleport_1 { get; set; }
        public string __class__ { get; set; }
    }

    public class Strategy_PointsVO
    {
        public int baseSP { get; set; }
        public int maxSP { get; set; }
        public int currentSP { get; set; }
        public int nextSpIn { get; set; }
        public int producingTime { get; set; }
        public string __class__ { get; set; }
    }

    public class SeasonalEventVO
    {
        public int eventId { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public string name { get; set; }
        public string state { get; set; }
        public int remainingTime { get; set; }
        public string __class__ { get; set; }
    }

    public class CityUserDataVO
    {
        public int player_id { get; set; }
        public string city_name { get; set; }
        public string user_name { get; set; }
        public string race { get; set; }
        public string portrait_id { get; set; }
        public GuildInfoVO guild_info { get; set; }
        public TechnologySectionVO technologySection { get; set; }
        public string __class__ { get; set; }
    }
    
    public class GuildInfoVO
    {
        public int id { get; set; }
        public string name { get; set; }
        public GuildBannerVO banner { get; set; }
        public uint fontColor { get; set; }
    }

    public class TechnologySectionVO
    {
        public string guestRace { get; set; }
        public int index { get; set; }
        public string description { get; set; }
        public uint fontColor { get; set; }
    }

    public class PlayerRankingListVO
    {
        public int length { get; set; }
        public PlayerRankingVO[] rankings { get; set; }
        public int pageIndex { get; set; }
        public string category { get; set; }
        public string __class__ { get; set; }
    }

    public class PlayerRankingVO
    {
        public BasePlayerVO player { get; set; }
        public GuildExtendedInfoVO guildInfo { get; set; }
        public int rank { get; set; }
        public int points { get; set; }
        public string __class__ { get; set; }
    }

    public class BasePlayerVO
    {
        public int player_id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string __class__ { get; set; }
    }

    public class GuildRankingListVO
    {
        public int length { get; set; }
        public GuildRankingVO[] rankings { get; set; }
        public int pageIndex { get; set; }
        public string category { get; set; }
        public string __class__ { get; set; }
    }

    public class GuildRankingVO
    {
        public GuildExtendedInfoVO guild_info { get; set; }
        public int rank { get; set; }
        public int points { get; set; }
        public int member_count { get; set; }
        public string __class__ { get; set; }
    }

    public class GuildExtendedInfoVO
    {
        public string leaderName { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public GuildBannerVO banner { get; set; }
        public string __class__ { get; set; }
    }

    public class OtherPlayerCityVO
    {
        public OtherPlayerVO other_player { get; set; }
        public string city_name { get; set; }
        public OtherPlayerCityMapVO city_map { get; set; }
        public int technologySection { get; set; }
        public string __class__ { get; set; }
    }

    public class OtherPlayerVO
    {
        public ProvinceLocationVO location { get; set; }
        public int rank { get; set; }
        public string city_name { get; set; }
        public GuildInfoVO guild_info { get; set; }
        public int player_id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string race { get; set; }
        public string __class__ { get; set; }
    }

    public class ProvinceLocationVO
    {
        public int r { get; set; }
        public int q { get; set; }
        public string __class__ { get; set; }
    }

    public class OtherPlayerCityMapVO
    {
        public ResearchPhaseVO[] ancientWonderResearchPhases { get; set; }
    }
    public class ResearchPhaseVO
    {
        public int investedKnowledgePoints { get; set; }
        public int requiredKnowledgePoints { get; set; }
        public ResearchContributionVO[] contributions { get; set; }
        public int playerId { get; set; }
        public string entityBaseName { get; set; }
        public bool isFavourite { get; set; }
        public string __class__ { get; set; }
    }

    public class ResearchContributionVO
    {
        public int rank { get; set; }
        public PlayerVO player { get; set; }
        public int knowledgePoints { get; set; }
        public ResearchContributionRewardVO reward { get; set; }
        public string __class__ { get; set; }
    }

    public class PlayerVO
    {
        public int player_id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string __class__ { get; set; }
    }

    public class ResearchContributionRewardVO
    {
        public string icon { get; set; }
        public RewardVO[] rewards { get; set; }
        public string __class__ { get; set; }
    }

    public class RewardVO
    {
        public string type { get; set; }
        public string subType { get; set; }
        public int amount { get; set; }
        public string __class__ { get; set; }
    }
}
