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
        private Dictionary<string, CustomerStatistics> _CustomerStatistics = new Dictionary<string, CustomerStatistics>();

        public event EventHandler<Bet[]> SettledBetsAdded;
        public event EventHandler<Bet[]> UnSettledBetsAdded;
        public event EventHandler<Bet[]> UnSettledBetsRemoved;

        public Bet[] SettledBets
        {
            get
            {
                return _settledBets.ToArray();
            }
        }

        public Bet[] UnSettledBets
        {
            get
            {
                return _unSettledBets.ToArray();
            }
        }

        public void AddSettledBets(params Bet[] bets)
        {
            if (bets == null)
                throw new ArgumentNullException(nameof(bets));

            _settledBets.AddRange(bets);
            UpdateStatistics(bets);

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

        public Bet[] GetSettledBets(string customerId)
        {
            return _settledBets.FindAll(bet => bet.CustomerId == customerId).ToArray();
        }

        public Bet[] GetUnSettledBets(string customerId)
        {
            return _unSettledBets.FindAll(bet => bet.CustomerId == customerId).ToArray();
        }

        public string[] SettledCustomers => _CustomerStatistics.Keys.ToArray();
 
        public CustomerStatistics GetCustomerStatistics(string customerId)
        {
            CustomerStatistics stats;
            if (!_CustomerStatistics.TryGetValue(customerId, out stats))
            {
                return null;
            }
            return new CustomerStatistics
            {
                CustomerId = stats.CustomerId,
                WonBets = stats.WonBets,
                LostBets = stats.LostBets,
                AvgBet = stats.AvgBet
            };
        }

        private void UpdateStatistics(Bet[] bets)
        {
            foreach (var bet in bets)
            {
                CustomerStatistics stats;
                if (!_CustomerStatistics.TryGetValue(bet.CustomerId, out stats))
                {
                    stats = new CustomerStatistics();
                    _CustomerStatistics[bet.CustomerId] = stats;

                    stats.CustomerId = bet.CustomerId;
                }
                var betCount = stats.WonBets + stats.LostBets;
                if (bet.Returns > 0)
                {
                    stats.WonBets++;
                }
                else
                {
                    stats.LostBets++;
                }
                stats.AvgBet = (stats.AvgBet * betCount + bet.Stake) / (betCount + 1);
            }
        }
    }
}
