using System;
using System.Collections.Generic;
using System.Linq;
using BetService.DomainObjects;

namespace BetService.Services
{
    public class BetsService :IBetsService
    {
        //Can be backed by a data store
        private List<Bet> _settledBets = new List<Bet>();
        private List<Bet> _unSettledBets = new List<Bet>();

        public event EventHandler<Bet[]> SettledBetsAdded;
        public event EventHandler<Bet[]> UnSettledBetsAdded;
        public event EventHandler<Bet[]> UnSettledBetsRemoved;


        public void AddSettledBets(params Bet[] bets)
        {
            if (bets == null)
                throw new ArgumentNullException(nameof(bets));

            _settledBets.AddRange(bets);

            if (SettledBetsAdded != null)
            {
                SettledBetsAdded(this, bets);
            }
        }

        public void AddUnSettledBets(params Bet[] bets)
        {
            if (bets == null)
                throw new ArgumentNullException(nameof(bets));

            _unSettledBets.AddRange(bets);
            if (UnSettledBetsAdded != null)
            {
                UnSettledBetsAdded(this, bets);
            }
        }

        public void RemoveUnSettledBets(params Bet[] bets)
        {
            if (bets == null)
                throw new ArgumentNullException(nameof(bets));

            foreach (var bet in bets)
            {
                _unSettledBets.Remove(bet);
            }
            if (UnSettledBetsRemoved != null)
            {
                UnSettledBetsRemoved(this, bets);
            }
        }
    }
}
