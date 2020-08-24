using System.Windows;
using ViewModel;
using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace EMA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        VendorsManager vendorsManager;
        CatalogManager catalogManager;
        PositionsManager positionManager;
        EntryReasonsManager entryReasonsManager;
        EntryContinuationCriteriaManager entryContinuationCriteriaManager;
        EntriesManager entriesManager;

        MainVM mainVM;
        MainWindow mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            vendorsManager = new VendorsManager();
            catalogManager = new CatalogManager(vendorsManager);
            positionManager = new PositionsManager(catalogManager);
            entryReasonsManager = new EntryReasonsManager();
            entryContinuationCriteriaManager = new EntryContinuationCriteriaManager();
            entriesManager = new EntriesManager(positionManager, entryReasonsManager, entryContinuationCriteriaManager);

            mainVM = new MainVM();

            mainVM.MainMenuVM = new MainMenuVM();
            mainVM.MainMenuVM.ShowVendorsRequest += () => ShowEntitiesListWindow<Vendor, VendorVM, VendorEditWindow, VendorsWindow>(vendorsManager);
            mainVM.MainMenuVM.ShowCatalogRequest += () => ShowEntitiesListWindow<CatalogItem, CatalogItemVM, CatalogItemEditWindow, CatalogWindow>(catalogManager);
            mainVM.MainMenuVM.ShowPositionsRequest += () => ShowEntitiesListWindow<Position, PositionVM, PositionEditWindow, PositionsListWindow>(positionManager);
            mainVM.MainMenuVM.ShowEntriesRequest += () => ShowEntitiesListWindow<Entry, EntryVM, EntryEditWindow, EntriesListWindow>(entriesManager);

            mainVM.PositionsTreeVM = new PositionsTreeVM(positionManager);
            mainVM.PositionsTreeVM.ShowAddEntryByPositionRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.Add, entriesManager.RelationEntities);
            mainVM.PositionsTreeVM.ShowAddChildPositionRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(obj, positionManager.Add, positionManager.RelationEntities);
            mainVM.PositionsTreeVM.ShowEditPositionRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(obj, positionManager.Update, positionManager.RelationEntities);
            mainVM.PositionsTreeVM.ShowDeletePositionRequest += (obj) => ShowEntityDeleteDialog<Position>(obj, positionManager.Delete);

            mainVM.EntriesTreeVM = new EntriesTreeVM(entriesManager);
            mainVM.EntriesTreeVM.ShowAddEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.Add, entriesManager.RelationEntities);
            mainVM.EntriesTreeVM.ShowAddChildEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.Add, entriesManager.RelationEntities);
            mainVM.EntriesTreeVM.ShowEditEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(obj, entriesManager.Update, entriesManager.RelationEntities);
            mainVM.EntriesTreeVM.ShowDeleteEntryRequest += (obj) => ShowEntityDeleteDialog<Entry>(obj, entriesManager.Delete);

            mainWindow = new MainWindow { DataContext = mainVM };
            mainWindow.Show();
        }


        private void ShowEntitiesListWindow<TEntity, TEntityVM, TEntityV, TEntitiesListV>(IEntityManager<TEntity> entityManager) where TEntity : class, new() where TEntityV : Window, new() where TEntityVM : IEntityVM<TEntity>, new() where TEntitiesListV : Window, new()
        {
            var vm = new EntitiesListEditVM<TEntity>(entityManager);
            vm.AddItemRequest += (obj) => ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(obj, entityManager.Add, entityManager.RelationEntities);
            vm.EditItemRequest += (obj) => ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(obj, entityManager.Update, entityManager.RelationEntities);
            vm.DeleteItemRequest += (obj) => ShowEntityDeleteDialog<TEntity>(obj, entityManager.Delete);
            var win = new TEntitiesListV();
            win.DataContext = vm;
            win.Owner = mainWindow;
            win.Show();
        }

        private void ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(TEntity entity, Func<TEntity, bool> executeDelegate, object relationEntities = null) where TEntityV : Window, new() where TEntityVM : IEntityVM<TEntity>, new()
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntity, TEntityVM>(entity, executeDelegate, win.Close, relationEntities);
            win.Owner = mainWindow;
            win.ShowDialog();
        }

        private void ShowEntityDeleteDialog<TEntity>(TEntity entity, Func<TEntity, bool> executeDelegate)
        {
            var win = new EntityDeleteWindow();
            win.DataContext = new EntityDeleteVM<TEntity>(entity, executeDelegate, win.Close);
            win.Owner = mainWindow;
            win.ShowDialog();
        }

    }
}
