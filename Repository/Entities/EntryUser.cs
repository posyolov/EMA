namespace Repository
{
    public class EntryUser
    {
        public int EntryId { get; set; }
        public int UserId { get; set; }
        
        public virtual Entry Entry { get; set; }
        public virtual User User { get; set; }
    }
}
