Risk APP
--------

Solution structure:-
> BetService(library)
> BetService.Test (test xunit library)
> RiskAPP (console application)

BetService is a pure service library which allows client to add settled and unsettled bets. RiskService allow client to get risk of an unsettled bet.

How to Run the app
-------------------

1. Run console app:
cd RiskApp
dotnet run

2. Run Unit test
cd BetService.Test
dotnet test