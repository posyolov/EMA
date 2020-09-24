using Model;
using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class PositionsTreeVM : NotifyViewModel
    {
        private ObservableCollection<PositionVM> positions;
        private PositionVM selectedItem;
        private readonly PositionsProxy positionProxy;

        public event Action<EntryVM> ShowAddEntryByPositionRequest;
        public event Action<PositionVM> ShowAddChildPositionRequest;
        public event Action<PositionVM> ShowEditPositionRequest;
        public event Action<PositionVM> ShowDeletePositionRequest;

        public DelegateCommand<EntryVM> ShowAddEntryByPositionCommand { get; }
        public DelegateCommand<PositionVM> ShowAddChildPositionCommand { get; }
        public DelegateCommand<PositionVM> ShowEditPositionCommand { get; }
        public DelegateCommand<PositionVM> ShowDeletePositionCommand { get; }

        public ObservableCollection<PositionVM> Positions
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

        public PositionVM SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                ShowAddEntryByPositionCommand.RiseCanExecuteChanged();
                ShowAddChildPositionCommand.RiseCanExecuteChanged();
                ShowEditPositionCommand.RiseCanExecuteChanged();
                ShowDeletePositionCommand.RiseCanExecuteChanged();
                NotifyPropertyChanged();
            }
        }

        public PositionsTreeVM(PositionsProxy positionProxy)
        {
            this.positionProxy = positionProxy;

            positionProxy.EntitiesChanged += UpdatePositions;
            UpdatePositions();

            ShowAddEntryByPositionCommand = new DelegateCommand<EntryVM>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowAddEntryByPositionRequest?.Invoke(new EntryVM() { PositionId = selectedItem.Id }));

            ShowAddChildPositionCommand = new DelegateCommand<PositionVM>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowAddChildPositionRequest?.Invoke(new PositionVM() { ParentId = selectedItem.Id, Name = selectedItem.Name}));

            ShowEditPositionCommand = new DelegateCommand<PositionVM>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowEditPositionRequest?.Invoke(selectedItem));

            ShowDeletePositionCommand = new DelegateCommand<PositionVM>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowDeletePositionRequest?.Invoke(selectedItem));
        }

        private void UpdatePositions()
        {
            Positions = positionProxy.Get();
        }
    }
}
