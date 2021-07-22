using System;

namespace Calendar.WebAPI.Models
{
    public class Availability
    {
        public Availability(){}
        public Availability(int id, DateTime start, DateTime end, int personId)
        {
            this.Id = id;
            this.Start = start;
            this.End = end;
            this.PersonId = personId;
        }
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int PersonId { get; set; }
    }
}