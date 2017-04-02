using System;

namespace BetService.DomainObjects
{
    public class CustomerStatistics
    {
        public String CustomerId {get; set;}

        public uint WonBets {get; set;}

        public uint LostBets {get; set;}

        public uint AvgBet {get; set;}
    }
}
