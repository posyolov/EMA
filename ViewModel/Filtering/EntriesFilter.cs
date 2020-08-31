using Repository;
using System;

namespace ViewModel
{
    public class EntriesFilter
    {
        public event Action CriteriasChanged;

        public FilterCriteria<string> PositionName { get; }
        public FilterCriteria<DateTime> OccurDateTime { get; }
        public FilterCriteria<int> Reason { get; }
        public FilterCriteria<string> Title { get; }

        public EntriesFilter()
        {
            PositionName = new FilterCriteria<string>();
            PositionName.Changed += CriteriasChangeInvoke;

            OccurDateTime = new FilterCriteria<DateTime>();
            OccurDateTime.Changed += CriteriasChangeInvoke;

            Reason = new FilterCriteria<int>();
            Reason.Changed += CriteriasChangeInvoke;

            Title = new FilterCriteria<string>();
            Title.Changed += CriteriasChangeInvoke;

        }

        public bool Filter(Entry entry)
        {
            return PositionName.ContainsIn(entry.Position.Name)
                && OccurDateTime.Include(entry.OccurDateTime)
                && Title.ContainsIn(entry.Title);
        }

        protected void CriteriasChangeInvoke()
        {
            CriteriasChanged?.Invoke();
        }

    }
}
