using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.EF
{
    public class Entry
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public bool IsFinal { get; set; }
        public DateTime OccurDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? PositionId { get; set; }
        public int? ReasonId { get; set; }
        public int? ContinuationCriteriaId { get; set; }
        public ushort? Priority { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public float? EstimatedResources { get; set; }
        public DateTime ChangeDateTime { get; set; }
        public int ChangeUserId { get; set; }

        public virtual Entry Parent { get; set; }
        public virtual ICollection<Entry> Children { get; set; }
        public virtual Position Position { get; set; }
        public virtual EntryReason Reason { get; set; }
        public virtual EntryContinuationCriteria ContinuationCriteria { get; set; }
        public virtual User ChangeUser { get; set; }
    }
}
