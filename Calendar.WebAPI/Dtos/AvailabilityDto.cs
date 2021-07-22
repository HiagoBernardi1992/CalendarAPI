using System;

namespace Calendar.WebAPI.Dtos
{
    public class AvailabilityDto
    {
        public AvailabilityDto()
        {
        }
        public AvailabilityDto(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;

        }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}