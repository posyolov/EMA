using System;

namespace ViewModel
{
    public class FilterCriteria<T> where T : IComparable
    {
        private bool filterEnable;
        private T filterValue;
        private T filterValueTo;

        public event Action Changed;

        public bool FilterEnable
        {
            get => filterEnable;
            set
            {
                filterEnable = value;
                Changed?.Invoke();
            }
        }
        public T FilterValue
        {
            get => filterValue;
            set
            {
                filterValue = value;
                Changed?.Invoke();
            }
        }
        public T FilterValueTo
        {
            get => filterValueTo;
            set
            {
                filterValueTo = value;
                Changed?.Invoke();
            }
        }


        public bool EqualTo(T obj)
        {
            if (!filterEnable || filterValue == null)
                return true;
            if (obj == null)
                return false;

            return obj.Equals(filterValue);
        }

        public bool ContainsIn(string obj)
        {
            string str = filterValue as string;

            if (!filterEnable || str == null)
                return true;
            if (obj == null)
                return false;

            return obj.Contains(str, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool Include(T obj)
        {
            if (!filterEnable || filterValue == null && filterValueTo == null)
                return true;
            if (obj == null)
                return false;

            bool result = true;

            if (filterValue != null && obj.CompareTo(filterValue) < 0)
                result = false;
            if (filterValueTo != null && obj.CompareTo(filterValueTo) > 0)
                result = false;

            return result;
        }
    }
}
