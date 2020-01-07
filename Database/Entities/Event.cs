using System;

namespace Database.Entities
{
    public class Event : IUniquelyIdentifiable, IDbTableAssociate
    {
        public int IdEvent { get; set; }

        public string Name { get; set; }

        public string Organizers { get; set; }

        public string Description { get; set; }

        public int IdCity { get; set; }

        public int IdAddress { get; set; }

        public int IdCategory { get; set; }

        public DateTime ScheduledOn { get; set; }

        public int Duration { get; set; }

        public string GetAssociatedDbTableName()
        {
            return "Event";
        }

        public string[] GetPrimaryKeyPropertyNames()
        {
            return new string[] { "IdEvent" };
        }
    }
}
