using System.Collections.ObjectModel;

namespace ViewModel
{
    public class PositionShortDataVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public ObservableCollection<PositionShortDataVM> Children { get; set; }
    }
}
