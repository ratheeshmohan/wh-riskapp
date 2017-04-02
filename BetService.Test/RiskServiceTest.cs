using System;
using Xunit;
using BetService.Services;
using BetService.DomainObjects;

namespace BetService.Test
{
    public class RiskServiceTest
    {
        RiskService _riskService;
        BetsService _betService;

        public RiskServiceTest()
        {
            _betService = new BetsService();
            _riskService = new RiskService(_betService);
        }

        [Fact]
        public void RiskService_Should_Return_UnusalWon_Customers()
        {
            _betService.AddSettledBets(
                new Bet("1","9", "7",1000,500),
                new Bet("1","5", "7",500,300),
                new Bet("2","9", "7",32424,0),
                new Bet("1","7", "7",500,0)
            );        

            var uwCustomes = _riskService.GetUnusuallyWonCustomers();
            Assert.Equal(1,uwCustomes.Length);
            Assert.Equal("1",uwCustomes[0]);
        }
        
        [Fact]
        public void RiskService_Should_Return_UnusuallyWonCustomer()
        {
            _betService.AddSettledBets(
                new Bet("1","9", "7",1000,500),
                new Bet("1","5", "7",500,300),
                new Bet("2","9", "7",32424,1000),
                new Bet("1","7", "7",500,0),
                new Bet("3","9", "7",32424,0)
            );      

            Assert.True(_riskService.IsUnusuallyWonCustomer("1"));
            Assert.True(_riskService.IsUnusuallyWonCustomer("2"));
            Assert.False(_riskService.IsUnusuallyWonCustomer("3"));
        }

        [Fact]
        public void RiskService_Should_Return_UnSettledRiskStatus_Rule1()
        {
            _betService.AddSettledBets(
                new Bet("1","9", "7",1000,500),
                new Bet("1","5", "7",500,300),
                new Bet("2","9", "7",32424,1000),
                new Bet("1","7", "7",500,0),
                new Bet("3","9", "7",32424,0)
            );      
            var uBet = new Bet("1","9", "7",1000,500);
            _betService.AddUnSettledBets(uBet);

            Assert.Equal(_riskService.GetUnSettledBetRiskStatus(uBet),RiskService.RiskStatus.Risky);
         }
        

        [Fact]
        public void RiskService_Should_Return_UnSettledRiskStatus_Rule2()
        {
            _betService.AddSettledBets(
                new Bet("1","9", "7",100,50),
                new Bet("1","5", "7",100,20),
                new Bet("3","9", "7",32424,0)
            );
            var uBet = new Bet("1","9", "7",1001,2000);
            _betService.AddUnSettledBets(uBet);

            Assert.Equal(_riskService.GetUnSettledBetRiskStatus(uBet),RiskService.RiskStatus.Unusual);
        }

        [Fact]
        public void RiskService_Should_Return_UnSettledRiskStatus_Rule3()
        {
            _betService.AddSettledBets(
                new Bet("1","9", "7",100,50),
                new Bet("1","5", "7",100,20),
                new Bet("3","9", "7",32424,0)
            );
            var uBet = new Bet("1","9", "7",3001,2000);
            _betService.AddUnSettledBets(uBet);

            Assert.Equal(_riskService.GetUnSettledBetRiskStatus(uBet),RiskService.RiskStatus.HighlyUnusual);
        }

        [Fact]
        public void RiskService_Should_Return_UnSettledRiskStatus_Rule4()
        {
            var uBet = new Bet("1","9", "7",1000,2000);
            _betService.AddUnSettledBets(uBet);

            Assert.Equal(_riskService.GetUnSettledBetRiskStatus(uBet),RiskService.RiskStatus.Risky);
         }
        
    }
}