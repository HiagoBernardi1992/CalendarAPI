using System.Collections.Generic;

namespace Calendar.WebAPI.Dtos
{
    public class PersonAvailabilitiesDto
    {
        public PersonAvailabilitiesDto()
        {
            this.AvailabilityDtoList = new List<AvailabilityDto>();
        }
        public PersonAvailabilitiesDto(string name, string role)
        {
            this.Name = name;
            this.Role = role;
            this.AvailabilityDtoList = new List<AvailabilityDto>();
        }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<AvailabilityDto> AvailabilityDtoList { get; set; }
    }
}