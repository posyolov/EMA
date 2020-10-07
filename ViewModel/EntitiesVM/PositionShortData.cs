using System.Collections.ObjectModel;

namespace ViewModel
{
    public class PositionShortData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public ObservableCollection<PositionShortData> Children { get; set; }
    }
}
