using System;
using System.Collections.Generic;
using BetService.DomainObjects;

namespace BetService.Services
{
    public class RiskService
    {
        public enum RiskStatus
        {
            NoRisk,
            Risky,
            Unusual,
            HighlyUnusual
        };

        private IBetsService _betsService;

        public RiskService(IBetsService betsService)
        {
            _betsService = betsService;
        }

        public String[] GetUnusuallyWonCustomers()
        {
            var unusals = new List<string>();
            foreach(var id in _betsService.SettledCustomers)
            {
              if(IsUnusuallyWonCustomer(id))
              {
                unusals.Add(id);
              }
            }
            return unusals.ToArray();
        }

        public bool IsUnusuallyWonCustomer(string customerId)
        {
            var stats = _betsService.GetCustomerStatistics(customerId);
            //Check if customer won more than 60% times
            return (stats!=null) ? ((stats.WonBets*100)/(stats.WonBets+stats.LostBets) > 60) : false;
        }

        public RiskStatus GetUnSettledBetRiskStatus(Bet bet)
        {
            //Note: If the rules are complicated we can model a rule engine.
            var stats = _betsService.GetCustomerStatistics(bet.CustomerId);
            if(stats != null)
            {
                if(bet.Stake > 30* stats.AvgBet)    
                    return RiskStatus.HighlyUnusual;

                if(bet.Stake > 10* stats.AvgBet)    
                    return RiskStatus.Unusual;
            }   
            if(IsUnusuallyWonCustomer(bet.CustomerId))
                return RiskStatus.Risky;
            
            if(bet.Returns >= 1000)
                return RiskStatus.Risky;
            else
                return RiskStatus.NoRisk;
        }
    }
}
        