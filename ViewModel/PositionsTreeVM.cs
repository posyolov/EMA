using Repository.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class PositionsTreeVM : NotifyViewModel
    {
        private readonly Func<IEnumerable<Position>> getTreeDelegate;
        private readonly Func<int, Position> getPositionFullDataDelegate;

        private ObservableCollection<Position> positions;
        private Position selectedItem;

        public ObservableCollection<Position> Positions
        {
            get
            {
                return positions;
            }
            private set
            {
                positions = value;
                NotifyPropertyChanged();
            }
        }

        public Position SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = getPositionFullDataDelegate?.Invoke(value.Id);
                NotifyPropertyChanged();
            }
        }

        public PositionsTreeVM(Func<IEnumerable<Position>> getTreeDelegate, Func<int, Position> getPositionFullDataDelegate)
        {
            this.getTreeDelegate = getTreeDelegate;
            this.getPositionFullDataDelegate = getPositionFullDataDelegate;
            Positions = new ObservableCollection<Position>(getTreeDelegate?.Invoke());
        }

        public void UpdateTree()
        {
            Positions = new ObservableCollection<Position>(getTreeDelegate?.Invoke());
        }
    }
}
