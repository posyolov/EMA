using Model;
using System;
using System.Windows;
using ViewModel;

namespace EMA
{
    public class PositionCrudWindowsCreator
    {
        private readonly PositionsProxy entityProxy = new PositionsProxy();

        public Window CreateEntitiesListWindow()
        {
            var vm = new EntitiesListEditVM<TEntityVM>(entityProxy);
            vm.AddItemRequest += CreateEntityAddWindow;
            vm.EditItemRequest += CreateEntityEditWindow;
            vm.DeleteItemRequest += CreateEntityDeleteWindow;
            var win = new TEntitiesListV { DataContext = vm };
            return win;
        }

        public Window CreateEntityAddWindow(int entityId)
        {
            var win = new PositionEditWindow();
            win.DataContext = new EntityEditVM<PositionEditVM>(entityProxy.GetPositionEditVM, entityProxy.Add, win.Close);
            return win;
        }

        public Window CreateEntityEditWindow(int entityId)
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntityVM>(entityVM, entityProxy.Update, win.Close);
            return win;
        }

        public Window CreateEntityDeleteWindow(int entityId)
        {
            var win = new EntityDeleteWindow();
            win.DataContext = new EntityDeleteVM<TEntityVM>(entityVM, entityProxy.Delete, win.Close);
            return win;
        }
    }
}
