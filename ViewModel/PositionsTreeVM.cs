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

        public event Action<Position> AddPositionRequest;
        public event Action<Position> EditPositionRequest;
        public event Action<Position> DeletePositionRequest;

        public DelegateCommand<object> AddPositionRequestCommand { get; }
        public DelegateCommand<object> EditPositionRequestCommand { get; }
        public DelegateCommand<object> DeletePositionRequestCommand { get; }

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

            AddPositionRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => AddPositionRequest?.Invoke(new Position() { ParentId = selectedItem.Id } ));

            EditPositionRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => EditPositionRequest?.Invoke(selectedItem));

            DeletePositionRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => DeletePositionRequest?.Invoke(selectedItem));
        }

        public void UpdateTree()
        {
            Positions = new ObservableCollection<Position>(getTreeDelegate?.Invoke());
        }
    }
}
