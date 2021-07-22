using Calendar.WebAPI.Enums;

namespace Calendar.WebAPI.Models
{
    public class Person
    {
        public Person() { }
        public Person(int id, string name, Role role)
        {
            this.Id = id;
            this.Name = name;
            this.Role = role;

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}