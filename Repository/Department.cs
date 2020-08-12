namespace Repository
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
