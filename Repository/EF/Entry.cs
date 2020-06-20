using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.EF
{
    public class Entry
    {
        public int Id { get; set; }
        [ForeignKey("Prev")]
        public int? PrevId { get; set; }
        [ForeignKey("Next")]
        public int? NextId { get; set; }
        public DateTime OccurDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? PositionId { get; set; }
        public int? ReasonId { get; set; }
        public int? ContinuationCriteriaId { get; set; }
        public ushort? Priority { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public float? EstimatedResources { get; set; }

        public virtual Entry Prev { get; set; }
        public virtual Entry Next { get; set; }
        public virtual Position Position { get; set; }
        public virtual EntryReason Reason { get; set; }
        public virtual EntryContinuationCriteria ContinuationCriteria { get; set; }
        //public virtual ICollection<Department> AttachedDepartments { get; set; }

    }
}
