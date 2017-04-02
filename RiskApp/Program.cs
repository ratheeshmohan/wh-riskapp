using System;
using System.Collections.Generic;
using System.IO;
using BetService.DomainObjects;
using BetService.Services;

namespace RiskApp
{
    class Program
    {
        private static BetsService betsService = new BetsService();
        private static RiskService riskService = new RiskService(betsService);

        static void Main(string[] args)
        {
            Console.WriteLine("********Risk App***********");
            Console.WriteLine("Reading settled best....");

            var settledBetLines = File.ReadAllLines("./Settled.csv");
            var sBets = new List<Bet>();
            for (int i = 1; i < settledBetLines.Length; ++i)
            {
                var splits = settledBetLines[i].Split(',');
                sBets.Add(new Bet(splits[0], splits[1], splits[2],
                   uint.Parse(splits[3]), uint.Parse(splits[4])));
            }

            Console.WriteLine("Reading unsettled best....");
            var unSettledBetLines = File.ReadAllLines("./Unsettled.csv");
            var uBets = new List<Bet>();
            for (int i = 1; i < unSettledBetLines.Length; ++i)
            {
                var splits = unSettledBetLines[i].Split(',');
                uBets.Add(new Bet(splits[0], splits[1], splits[2],
                        uint.Parse(splits[3]), uint.Parse(splits[4])));
            }

            betsService.AddSettledBets(sBets.ToArray());
            betsService.AddUnSettledBets(uBets.ToArray());

            Console.WriteLine("Unusual won customers are....");
            foreach (var customer in riskService.GetUnusuallyWonCustomers())
            {
                Console.WriteLine("Customer- {0}", customer);
            }

            Console.WriteLine("Risky unsettled bets are....");
            foreach (var bet in betsService.UnSettledBets)
            {
                var status = riskService.GetUnSettledBetRiskStatus(bet);
                if (status != RiskService.RiskStatus.NoRisk)
                    Console.WriteLine("Bet - {0},{1},{2},{3},{4} ****{5}****",
                    bet.CustomerId, bet.EventId, bet.ParticipantId, bet.Stake, bet.Returns, status);
            }
        }
    }
}
