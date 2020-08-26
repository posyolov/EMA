using Model;
using System;
using System.Windows;
using ViewModel;

namespace EMA
{
    public class EntitiyWindowsCreator<TEntity, TEntityVM, TEntityV, TEntitiesListV> where TEntity : class, new() where TEntityVM : IEntityVM<TEntity>, new() where TEntityV : Window, new() where TEntitiesListV : Window, new()
    {
        private readonly IEntityManager<TEntity> entityManager;
        private readonly Window ownerWindow;

        public EntitiyWindowsCreator(IEntityManager<TEntity> entityManager, Window ownerWindow)
        {
            this.entityManager = entityManager;
            this.ownerWindow = ownerWindow;
        }

        public void ShowEntitiesListWindow()
        {
            var vm = new EntitiesListEditVM<TEntity>(entityManager);
            vm.AddItemRequest += ShowEntityAddDialog;
            vm.EditItemRequest += ShowEntityEditDialog;
            vm.DeleteItemRequest += ShowEntityDeleteDialog;
            var win = new TEntitiesListV();
            win.DataContext = vm;
            win.Owner = ownerWindow;
            win.Show();
        }

        public void ShowEntityAddDialog(TEntity entity)
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntity, TEntityVM>(entity, entityManager.Add, win.Close, entityManager.RelationEntities);
            win.Owner = ownerWindow;
            win.ShowDialog();
        }

        public void ShowEntityEditDialog(TEntity entity)
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntity, TEntityVM>(entity, entityManager.Update, win.Close, entityManager.RelationEntities);
            win.Owner = ownerWindow;
            win.ShowDialog();
        }

        public void ShowEntityDeleteDialog(TEntity entity)
        {
            var win = new EntityDeleteWindow();
            win.DataContext = new EntityDeleteVM<TEntity>(entity, entityManager.Delete, win.Close);
            win.Owner = ownerWindow;
            win.ShowDialog();
        }
    }
}
