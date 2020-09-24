using Model;
using System;
using System.Windows;
using ViewModel;

namespace EMA
{
    public class EntitiyWindowsCreator<TEntityVM, TEntityV, TEntitiesListV> where TEntityVM : class, new() where TEntityV : Window, new() where TEntitiesListV : Window, new()
    {
        private readonly IEntityProxy<TEntityVM> entityProxy;
        private readonly Window ownerWindow;

        public EntitiyWindowsCreator(IEntityProxy<TEntityVM> entityProxy, Window ownerWindow)
        {
            this.entityProxy = entityProxy;
            this.ownerWindow = ownerWindow;
        }

        public void ShowEntitiesListWindow()
        {
            var vm = new EntitiesListEditVM<TEntityVM>(entityProxy);
            vm.AddItemRequest += ShowEntityAddDialog;
            vm.EditItemRequest += ShowEntityEditDialog;
            vm.DeleteItemRequest += ShowEntityDeleteDialog;
            var win = new TEntitiesListV();
            win.DataContext = vm;
            win.Owner = ownerWindow;
            win.Show();
        }

        public void ShowEntityAddDialog(TEntityVM entityVM)
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntityVM>(entityVM, entityProxy.Add, win.Close);
            win.Owner = ownerWindow;
            win.ShowDialog();
        }

        public void ShowEntityEditDialog(TEntityVM entityVM)
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntityVM>(entityVM, entityProxy.Update, win.Close);
            win.Owner = ownerWindow;
            win.ShowDialog();
        }

        public void ShowEntityDeleteDialog(TEntityVM entityVM)
        {
            var win = new EntityDeleteWindow();
            win.DataContext = new EntityDeleteVM<TEntityVM>(entityVM, entityProxy.Delete, win.Close);
            win.Owner = ownerWindow;
            win.ShowDialog();
        }
    }
}
