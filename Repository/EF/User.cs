using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.EF
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
