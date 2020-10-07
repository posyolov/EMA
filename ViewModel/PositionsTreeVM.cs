using Model;
using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class PositionsTreeVM : NotifyViewModel
    {
        private ObservableCollection<PositionShortDataVM> positionsTree;
        private int? selectedItemId;
        private PositionInfoVM selectedItemInfo;
        private readonly PositionsProxy positionProxy;

        public event Action<int?> ShowAddEntryByPositionRequest;
        public event Action<int?> ShowAddChildPositionRequest;
        public event Action<int?> ShowEditPositionRequest;
        public event Action<int?> ShowDeletePositionRequest;

        public DelegateCommand<int?> ShowAddEntryByPositionCommand { get; }
        public DelegateCommand<int?> ShowAddChildPositionCommand { get; }
        public DelegateCommand<int?> ShowEditPositionCommand { get; }
        public DelegateCommand<int?> ShowDeletePositionCommand { get; }

        public ObservableCollection<PositionShortDataVM> PositionsTree
        {
            get
            {
                return positionsTree;
            }
            private set
            {
                positionsTree = value;
                NotifyPropertyChanged();
            }
        }

        public int? SelectedItemId
        {
            get => selectedItemId;
            set
            {
                selectedItemId = value;

                ShowAddEntryByPositionCommand.RiseCanExecuteChanged();
                ShowAddChildPositionCommand.RiseCanExecuteChanged();
                ShowEditPositionCommand.RiseCanExecuteChanged();
                ShowDeletePositionCommand.RiseCanExecuteChanged();

                SelectedItemInfo = positionProxy.GetPositionInfo(selectedItemId);
            }
        }

        public PositionInfoVM SelectedItemInfo
        {
            get => selectedItemInfo;
            set
            {
                selectedItemInfo = value;
                NotifyPropertyChanged();
            }
        }

        public PositionsTreeVM(PositionsProxy positionProxy)
        {
            this.positionProxy = positionProxy;

            positionProxy.EntitiesChanged += UpdatePositions;
            UpdatePositions();

            ShowAddEntryByPositionCommand = new DelegateCommand<int?>(
                canExecute: (obj) => SelectedItemId != null,
                execute: (obj) => ShowAddEntryByPositionRequest?.Invoke(selectedItemId));

            ShowAddChildPositionCommand = new DelegateCommand<int?>(
                canExecute: (obj) => SelectedItemId != null,
                execute: (obj) => ShowAddChildPositionRequest?.Invoke(selectedItemId));

            ShowEditPositionCommand = new DelegateCommand<int?>(
                canExecute: (obj) => SelectedItemId != null,
                execute: (obj) => ShowEditPositionRequest?.Invoke(selectedItemId));

            ShowDeletePositionCommand = new DelegateCommand<int?>(
                canExecute: (obj) => SelectedItemId != null,
                execute: (obj) => ShowDeletePositionRequest?.Invoke(selectedItemId));
        }

        private void UpdatePositions()
        {
            PositionsTree = positionProxy.GetPositionsTree();
        }
    }
}
