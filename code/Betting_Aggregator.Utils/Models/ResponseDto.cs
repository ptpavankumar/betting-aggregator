using System.Collections.Generic;

namespace Betting_Aggregator.Utils
{
    public class ResponseDto
    {
        public string Type { get; set; }
        public List<ResponseDetail> Details { get; set; }
    }

    public class ResponseDetail
    {
        public string Name { get; set; }
        public List<string> Messages { get; set; }
    }
}