using System.Collections.Generic;

namespace Repository
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<EntryUser> RelatedEntries { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
