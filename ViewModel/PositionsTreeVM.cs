using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class PositionsTreeVM : NotifyViewModel
    {
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
                selectedItem = value;
                ShowAddEntryByPositionCommand.RiseCanExecuteChanged();
                ShowAddChildPositionCommand.RiseCanExecuteChanged();
                ShowEditPositionCommand.RiseCanExecuteChanged();
                ShowDeletePositionCommand.RiseCanExecuteChanged();
                NotifyPropertyChanged();
            }
        }

        public PositionsTreeVM(PositionsManager positionManager)
        {
            Positions = new ObservableCollection<Position>(positionManager.GetPositionsTree());

            positionManager.EntitiesChanged += () => { Positions = new ObservableCollection<Position>(positionManager.GetPositionsTree()); };

            ShowAddEntryByPositionCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowAddEntryByPositionRequest?.Invoke(new Entry { PositionId = selectedItem.Id }));

            ShowAddChildPositionCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowAddChildPositionRequest?.Invoke(new Position() { ParentId = selectedItem.Id, Name = selectedItem.Name}));

            ShowEditPositionCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowEditPositionRequest?.Invoke(selectedItem));

            ShowDeletePositionCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => ShowDeletePositionRequest?.Invoke(selectedItem));
        }
    }
}
