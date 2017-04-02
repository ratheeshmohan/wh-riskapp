using System;
using Xunit;
using BetService.Services;
using BetService.DomainObjects;

namespace BetService.Test
{
    public class BetsServiceTest
    {
        [Fact]
        public void BettService_Should_Handle_Invalid_Arguments()
        {
            var service = new BetsService();
            Assert.Throws<ArgumentNullException>(() => service.AddSettledBets(null));
            Assert.Throws<ArgumentNullException>(() => service.AddUnSettledBets(null));
            Assert.Throws<ArgumentNullException>(() => service.RemoveUnSettledBets(null));
        }

        [Fact]
        public void BettService_Should_Add_Settled_Bets_Gracefully()
        {
            var service = new BetsService();
            var bet = new Bet("1","9", "7",32424,324);
            service.AddSettledBets(bet);
            Assert.Equal(service.SettledBets.Length, 1);
            Assert.Equal(service.SettledBets[0], bet);
        }
        
        [Fact]
        public void BettService_Should_Return_Settled_Bets_While_Querying_By_CustomerId()
        {
            var service = new BetsService();
            var bet = new Bet("1","9", "7",32424,324);
            service.AddSettledBets(bet);
            var sbets = service.GetSettledBets("1");
            Assert.Equal(1, sbets.Length);
            Assert.Equal(bet, sbets[0]);
        }   
        
        [Fact]
        public void BettService_Should_Add_UnSettled_Bets_Gracefully()
        {
            var service = new BetsService();
            var bet = new Bet("1","9", "7",32424,324);
            service.AddUnSettledBets(bet);
            Assert.Equal(service.UnSettledBets.Length, 1);
            Assert.Equal(service.UnSettledBets[0], bet);
        }
        
        [Fact]
        public void BettService_Should_Return_UnSettled_Bets_While_Querying_By_CustomerId()
        {
            var service = new BetsService();
            var bet = new Bet("1","9", "7",32424,324);
            service.AddUnSettledBets(bet);
            var ubets = service.GetUnSettledBets("1");
            Assert.Equal(ubets.Length, 1);
            Assert.Equal(bet, ubets[0]);
        }
         
        [Fact]
        public void BettService_Should_Return_Valid_CustomersList()
        {
            var service = new BetsService();
            var bet = new Bet("1","9", "7",32424,324);
            service.AddSettledBets(bet);
            Assert.Equal(service.SettledCustomers.Length, 1);
            Assert.Equal(service.SettledCustomers[0], "1");
        }

        [Fact]
        public void BettService_Should_Return_Valid_CustomerStatistics()
        {
            var service = new BetsService();
            var bet1 = new Bet("1","9", "7",100,50);
            var bet2 = new Bet("1","9", "7",150,0);

            service.AddSettledBets(bet1,bet2);
            Assert.Equal(service.GetCustomerStatistics("1").WonBets, 1U);
            Assert.Equal(service.GetCustomerStatistics("1").LostBets, 1U);
            Assert.Equal(service.GetCustomerStatistics("1").AvgBet, 125U);
            Assert.Equal(service.GetCustomerStatistics("1").CustomerId, "1");
        }
    }
}
