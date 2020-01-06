using System.ComponentModel.DataAnnotations;
using Betting_Aggregator.Utils;
using Newtonsoft.Json;

namespace Betting_Aggregator.API.Dtos
{
    public class LeagueDTO
    {
        [Required] [MaxLength(255)]
        public string Name { get; set; }

        [RequiredEnum(ErrorMessage = "The SportType field is required")]
        public SportType SportType { get; set; }

        public bool HasTables { get; set; }

        public bool HasTopList { get; set; }
    }

    public enum SportType
    {
        Soccer = 1,
        Tennis,
        Handball,
        IceHockey,
        AmericanFoodball,
        Futsal,
        TableTennis,
        RugbyUnion,
        AustralianRule,
        Boxing,
        Floorball,
        WaterPolo
    }
}