using Repository;
using System;

namespace ViewModel
{
    public class EntriesFilter
    {
        public event Action CriteriasChanged;

        public FilterCriteria<DateTime> OccurDateTime { get; }
        public FilterCriteria<string> PositionName { get; }
        public FilterCriteria<int?> ReasonId { get; }
        public FilterCriteria<string> Title { get; }
        public FilterCriteria<bool> IsCopmlete { get; }

        public EntriesFilter()
        {
            OccurDateTime = new FilterCriteria<DateTime>();
            OccurDateTime.Changed += () => CriteriasChanged?.Invoke();

            PositionName = new FilterCriteria<string>();
            PositionName.Changed += () => CriteriasChanged?.Invoke();

            ReasonId = new FilterCriteria<int?>();
            ReasonId.Changed += () => CriteriasChanged?.Invoke();

            Title = new FilterCriteria<string>();
            Title.Changed += () => CriteriasChanged?.Invoke();

            IsCopmlete = new FilterCriteria<bool>();
            IsCopmlete.Changed += () => CriteriasChanged?.Invoke();
        }

        public bool Filter(Entry entry)
        {
            return OccurDateTime.Include(entry.OccurDateTime)
                && PositionName.ContainsIn(entry.Position.Name)
                && ReasonId.EqualTo(entry.ReasonId)
                && Title.ContainsIn(entry.Title)
                && IsCopmlete.EqualTo(entry.IsComplete);
        }
    }
}
