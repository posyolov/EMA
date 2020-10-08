using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewModel
{
    public class EntryVM
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public bool IsComplete { get; set; }
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

        public EntryVM Parent { get; set; }
        public virtual ObservableCollection<EntryVM> Children { get; set; }
        public virtual PositionShortData Position { get; set; }
        //public virtual EntryReason Reason { get; set; }
        //public virtual EntryContinuationCriteria ContinuationCriteria { get; set; }
        //public virtual User ChangeUser { get; set; }
        //public virtual ICollection<EntryUser> AssignedUsers { get; set; }

        public Entry FillModel(Entry model)
        {
            model.Id = Id;
            model.ParentId = ParentId;
            model.IsComplete = IsComplete;
            model.OccurDateTime = OccurDateTime;
            model.Title = Title;
            model.Description = Description;
            model.PositionId = PositionId;
            model.ReasonId = ReasonId;
            model.ContinuationCriteriaId = ContinuationCriteriaId;
            model.Priority = Priority;
            model.PlannedStartDate = PlannedStartDate;
            model.EstimatedResources = EstimatedResources;

            model.ChangeDateTime = DateTime.Now;
            model.ChangeUserId = 2; //GetCurrentUser;

            return model;
        }
    }
}
