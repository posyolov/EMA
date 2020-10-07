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
            var vm = new EntitiesListEditVM<PositionFullData>(entityProxy.GetPositionsList());
            vm.AddItemRequest += () => CreateEntityAddWindow();
            vm.EditItemRequest += (id) => CreateEntityEditWindow(id);
            vm.DeleteItemRequest += (id) => CreateEntityDeleteWindow(id);
            var win = new PositionsListWindow { DataContext = vm };
            return win;
        }

        public Window CreateEntityAddWindow()
        {
            var win = new PositionEditWindow();
            win.DataContext = new EntityEditVM<PositionEdit>(new PositionEdit(), entityProxy.Add, win.Close);
            return win;
        }

        public Window CreateEntityEditWindow(int entityId)
        {
            var win = new PositionEditWindow();
            win.DataContext = new EntityEditVM<PositionEdit>(entityProxy.GetPositionEdit(entityId), entityProxy.Update, win.Close);
            return win;
        }

        public Window CreateEntityDeleteWindow(int entityId)
        {
            var win = new EntityDeleteWindow();
            win.DataContext = new EntityDeleteVM<PositionEdit>(entityProxy.GetPositionEdit(entityId), entityProxy.Delete, win.Close);
            return win;
        }
    }
}
