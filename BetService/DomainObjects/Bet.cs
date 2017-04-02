using System;

namespace BetService.DomainObjects
{
    public class Bet
    {
        public Bet(string customerId, string eventId, string participantId,
            uint stake, uint returns)
        {
            CustomerId = customerId;
            EventId = eventId;
            ParticipantId = participantId;
            Stake = stake;
            Returns = returns;
        }

        public String CustomerId { get; }

        public String EventId { get; }

        public String ParticipantId { get; }

        public uint Stake { get; }

        public uint Returns { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var other = (Bet)obj;
            return (this.CustomerId == other.CustomerId && this.EventId == other.EventId &&
            this.ParticipantId == other.ParticipantId && this.Stake == other.Stake &&
            this.Returns == other.Returns);
        }

        public override int GetHashCode()
        {
            var hashCode = 13;
            hashCode = (hashCode * 397) ^ Stake.GetHashCode();
            hashCode = (hashCode * 397) ^ Returns.GetHashCode();
            hashCode = (hashCode * 397) ^ (!string.IsNullOrEmpty(CustomerId) ? ParticipantId.GetHashCode() : 1);
            hashCode = (hashCode * 397) ^ (!string.IsNullOrEmpty(EventId) ? ParticipantId.GetHashCode() : 1);
            hashCode = (hashCode * 397) ^ (!string.IsNullOrEmpty(ParticipantId) ? ParticipantId.GetHashCode() : 1);
            return hashCode;
        }

    }
}
