using System;
using BetService.DomainObjects;

namespace BetService
{
    public interface IBetsService
    {
        void AddSettledBets(params Bet[] bets);

        void AddUnSettledBets(params Bet[] bets);

        void RemoveUnSettledBets(params Bet[] bets);

        event EventHandler<Bet[]> SettledBetsAdded;

        event EventHandler<Bet[]> UnSettledBetsAdded;

        event EventHandler<Bet[]> UnSettledBetsRemoved;

        Bet[] SettledBets{get;}

        Bet[] UnSettledBets{get;}

        Bet[] GetSettledBets(string customerId);
        
        Bet[] GetUnSettledBets(string customerId);

        string[] SettledCustomers{get;}
        
        CustomerStatistics GetCustomerStatistics(string customerId);
    }
}


