using System.Collections;

namespace DomainModel
{
    public class User : IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CompareTo(object? obj)
        {
            User user = obj as User;
            return Id.CompareTo(user.Id);
        }
    }
}