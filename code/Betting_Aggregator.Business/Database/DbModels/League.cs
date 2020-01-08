namespace Betting_Aggregator.Business.Database.DbModels
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasTable { get; set; }
        public bool HasTopList { get; set; }
    }
}
