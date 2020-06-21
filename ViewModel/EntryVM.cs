using Repository.EF;
using System;

namespace ViewModel
{
    public class EntryVM : IEntityVM<Entry>
    {
        public bool IsValid
        {
            get
            {
                return true;
            }
        }

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

        public void ToViewModel(Entry model)
        {
            ParentId = model.ParentId;
            IsFinal = model.IsFinal;
            OccurDateTime = model.OccurDateTime != default ? model.OccurDateTime : DateTime.Now;
            Title = model.Title;
            Description = model.Description;
            PositionId = model.PositionId;
            ReasonId = model.ReasonId;
            ContinuationCriteriaId = model.ContinuationCriteriaId;
            Priority = model.Priority;
            PlannedStartDate = model.PlannedStartDate;
            EstimatedResources = model.EstimatedResources;
        }

        public void ToModel(Entry model)
        {
            model.ParentId = ParentId;
            model.IsFinal = IsFinal;
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
        }
    }
}
