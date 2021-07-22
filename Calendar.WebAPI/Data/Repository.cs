using System.Collections.Generic;
using System.Linq;
using Calendar.WebAPI.Enums;
using Calendar.WebAPI.Dtos;
using Calendar.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Calendar.WebAPI.Helpers;

namespace Calendar.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public void AddAvailabilities(Availability availability)
        {
            if ((int)(availability.End - availability.Start).TotalMinutes > 60)
            {
                var intermediate = availability.Start;
                while(intermediate < availability.End)
                {
                    var newDate = intermediate.AddMinutes(60);
                    Add(new Availability()
                    {
                        PersonId = availability.PersonId,
                        Start = intermediate,
                        End = newDate
                    });
                    intermediate = newDate;
                }
            }
            else
            {
                Add(availability);
            }
        }

        public List<AvailabilityDto> GetAllAvailabilitiesByPerson(int personId)
        {
            List<AvailabilityDto> retorno = new List<AvailabilityDto>();
            IQueryable<Availability> query = _context.Availabilities;
            var availabilities = query.AsNoTracking().OrderBy(a => a.Id).Where(a => a.PersonId == personId).ToList();
            availabilities.ForEach(ava => retorno.Add(new AvailabilityDto()
            {
                Start = ava.Start,
                End = ava.End
            }));
            return retorno;
        }

        public Availability GetAvailabilityById(int Id)
        {
            IQueryable<Availability> query = _context.Availabilities;
            query = query.AsNoTracking().OrderBy(a => a.Id).Where(a => a.Id == Id);
            return query.FirstOrDefault();
        }

        public Person GetPersonById(int Id)
        {
            IQueryable<Person> query = _context.Persons;
            query = query.AsNoTracking().OrderBy(a => a.Id).Where(a => a.Id == Id);
            return query.FirstOrDefault();
        }

        public List<PersonAvailabilitiesDto> GetAllAvailabilities()
        {
            IQueryable<Person> query = _context.Persons;
            var persons = query.AsNoTracking().OrderBy(a => a.Id).ToList();
            List<PersonAvailabilitiesDto> retorno = new List<PersonAvailabilitiesDto>();
            foreach (var person in persons)
            {
                PersonAvailabilitiesDto dto = new PersonAvailabilitiesDto();
                dto.Name = person.Name;
                dto.Role = person.Role == Role.Interviewer ? "Interviewer" : "Candidate";
                dto.AvailabilityDtoList = GetAllAvailabilitiesByPerson(person.Id);
                retorno.Add(dto);
            }
            return retorno;
        }

        public List<AvailabilityDto> GetInterviewerAvailabilitiesForCandidate(PageParams pageParams, string name)
        {
            List<AvailabilityDto> retorno = new List<AvailabilityDto>();
            IQueryable<Person> queryCandidate = _context.Persons;
            var candidate = queryCandidate.AsNoTracking().Where(p => p.Name.ToLower() == name.ToLower() && p.Role == Role.Candidate).FirstOrDefault();
            if (candidate != null)
            {
                IQueryable<Availability> queryAva = _context.Availabilities;
                var availabilitiesCandidate = queryAva.AsNoTracking().Where(a => a.PersonId == candidate.Id).ToList();
                foreach (var ava in availabilitiesCandidate)
                {
                    List<Availability> interviewerAvailabilities = new List<Availability>();
                    if (pageParams.interviewers != null)
                        interviewerAvailabilities = GetSpecifiedInterviewerAvailabilities(pageParams.interviewers, ava.Start, ava.End);
                    else
                        interviewerAvailabilities = GetAllAvailabilities(ava.Start, ava.End);

                    foreach (var av in interviewerAvailabilities)
                    {
                        retorno.Add(new AvailabilityDto()
                        {
                            Start = av.Start,
                            End = av.End
                        });
                    }
                }
            }
            return retorno;
        }

        private List<Availability> GetAllAvailabilities(DateTime start, DateTime end)
        {
            IQueryable<Availability> query = _context.Availabilities;
            return query.AsNoTracking().Where(a => a.Start == start && a.End == a.End).ToList();
        }

        private List<Availability> GetSpecifiedInterviewerAvailabilities(List<string> names, DateTime start, DateTime end)
        {
            List<Availability> retorno = new List<Availability>();
            foreach (var name in names)
            {
                IQueryable<Person> queryInterviewer = _context.Persons;
                var person = queryInterviewer.AsNoTracking().Where(p => p.Name.ToLower() == name.ToLower() && p.Role == Role.Interviewer).FirstOrDefault();
                if (person != null)
                {
                    IQueryable<Availability> queryAva = _context.Availabilities;
                    var interviewerAvailabilities = queryAva.AsNoTracking().Where(a => a.Start == start && a.End == a.End && a.PersonId == person.Id).ToList();
                    retorno.AddRange(interviewerAvailabilities);
                }
            }
            return retorno;
        }

    }
}