using System.Collections.Generic;
using Calendar.WebAPI.Dtos;
using Calendar.WebAPI.Helpers;
using Calendar.WebAPI.Models;

namespace Calendar.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        bool SaveChanges();
        List<AvailabilityDto> GetAllAvailabilitiesByPerson(int personId);
        Availability GetAvailabilityById(int Id);
        Person GetPersonById(int Id);
        List<PersonAvailabilitiesDto> GetAllAvailabilities();
        List<AvailabilityDto> GetInterviewerAvailabilitiesForCandidate(PageParams pageParams, string name);
        void AddAvailabilities(Availability availability);
    }
}