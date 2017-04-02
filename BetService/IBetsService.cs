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

    }
}


