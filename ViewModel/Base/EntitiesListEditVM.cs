﻿using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class EntitiesListEditVM<TEntity> : NotifyViewModel where TEntity : class, new()
    {
        public event Action<TEntity> AddItemRequest;
        public event Action<TEntity> EditItemRequest;
        public event Action<TEntity> DeleteItemRequest;

        public DelegateCommand<object> AddItemRequestCommand { get; }
        public DelegateCommand<object> EditItemRequestCommand { get; }
        public DelegateCommand<object> DeleteItemRequestCommand { get; }

        private TEntity selectedItem;
        private ObservableCollection<TEntity> items;

        public ObservableCollection<TEntity> Items
        {
            get
            {
                return items;
            }
            private set
            {
                items = value;
                NotifyPropertyChanged();
            }
        }

        public TEntity SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                EditItemRequestCommand.RiseCanExecuteChanged();
                DeleteItemRequestCommand.RiseCanExecuteChanged();
            }
        }


        public EntitiesListEditVM(IEntityManager<TEntity> entityManager)
        {
            Items = new ObservableCollection<TEntity>(entityManager.Get());

            entityManager.EntitiesChanged += () => { Items = new ObservableCollection<TEntity>(entityManager.Get()); };

            AddItemRequestCommand = new DelegateCommand<object>(
                (obj) => AddItemRequest?.Invoke(new TEntity()));

            EditItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => EditItemRequest?.Invoke(selectedItem));

            DeleteItemRequestCommand = new DelegateCommand<object>(
                canExecute: (obj) => SelectedItem != null,
                execute: (obj) => DeleteItemRequest?.Invoke(selectedItem));
        }
    }
}
