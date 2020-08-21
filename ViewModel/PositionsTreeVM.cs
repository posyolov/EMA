using Repository;
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

        public event Action<Entry> ShowAddEntryByPositionRequest;
        public event Action<Position> ShowAddChildPositionRequest;
        public event Action<Position> ShowEditPositionRequest;
        public event Action<Position> ShowDeletePositionRequest;

        public DelegateCommand<object> ShowAddEntryByPositionCommand { get; }
        public DelegateCommand<object> ShowAddChildPositionCommand { get; }
        public DelegateCommand<object> ShowEditPositionCommand { get; }
        public DelegateCommand<object> ShowDeletePositionCommand { get; }

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
            get => selectedItem;
            set
            {
                selectedItem = value;// getPositionFullDataDelegate?.Invoke(value.Id);
                ShowAddEntryByPositionCommand.RiseCanExecuteChanged();
                ShowAddChildPositionCommand.RiseCanExecuteChanged();
                ShowEditPositionCommand.RiseCanExecuteChanged();
                ShowDeletePositionCommand.RiseCanExecuteChanged();
                NotifyPropertyChanged();
            }
        }

        public PositionsTreeVM(Func<IEnumerable<Position>> getTreeDelegate, Func<int, Position> getPositionFullDataDelegate)
        {
            this.getTreeDelegate = getTreeDelegate;
            this.getPositionFullDataDelegate = getPositionFullDataDelegate;
            Positions = new ObservableCollection<Position>(getTreeDelegate?.Invoke());

            ShowAddEntryByPositionCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowAddEntryByPositionRequest?.Invoke(new Entry { PositionId = selectedItem.Id }));

            ShowAddChildPositionCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowAddChildPositionRequest?.Invoke(new Position() { ParentId = selectedItem.Id, Parent = selectedItem }));

            ShowEditPositionCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowEditPositionRequest?.Invoke(selectedItem));

            ShowDeletePositionCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowDeletePositionRequest?.Invoke(selectedItem));
        }

        public void UpdateTree()
        {
            Positions = new ObservableCollection<Position>(getTreeDelegate?.Invoke());
        }
    }
}
