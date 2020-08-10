using System.Windows;
using ViewModel;
using Model;
using Repository;
using System;

namespace EMA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        CatalogManager catalogManager;
        PositionsManager positionManager;
        EntriesManager entriesManager;

        MainVM mainVM;
        MainWindow mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            catalogManager = new CatalogManager();
            positionManager = new PositionsManager();
            entriesManager = new EntriesManager();

            mainVM = new MainVM();

            mainVM.MainMenuVM = new MainMenuVM();
            mainVM.MainMenuVM.CatalogRequest += ShowCatalogWindow;
            mainVM.MainMenuVM.VendorsRequest += ShowVendorsWindow;
            mainVM.MainMenuVM.PositionsRequest += ShowPositionsWindow;
            mainVM.MainMenuVM.EntriesRequest += ShowEntriesWindow;

            mainVM.PositionsTreeVM = new PositionsTreeVM(positionManager.GetPositionsTree, positionManager.GetPositionFullData);
            mainVM.PositionsTreeVM.AddEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(entriesManager.AddEntry, new Entry { PositionId = obj }, new object[] { entriesManager.GetEntries(), positionManager.GetPositions(), entriesManager.GetReasons(), entriesManager.GetContinuationCriterias() });
            mainVM.PositionsTreeVM.AddPositionRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(positionManager.AddPosition, obj, new object[] { positionManager.GetPositions(), catalogManager.GetCatalog() });
            mainVM.PositionsTreeVM.EditPositionRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(positionManager.UpdatePosition, obj, new object[] { positionManager.GetPositions(), catalogManager.GetCatalog() });
            mainVM.PositionsTreeVM.DeletePositionRequest += (obj) => ShowEntityEditDialog<Position, PositionVM, PositionDeleteWindow>(positionManager.DeletePosition, obj, null);
            positionManager.PositionsChanged += mainVM.PositionsTreeVM.UpdateTree;

            mainVM.EntriesTreeVM = new EntriesTreeVM(entriesManager.GetEntries, entriesManager.GetEntriesTree);
            mainVM.EntriesTreeVM.AddEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(entriesManager.AddEntry, obj, new object[] { entriesManager.GetEntries(), positionManager.GetPositions(), entriesManager.GetReasons(), entriesManager.GetContinuationCriterias() });
            mainVM.EntriesTreeVM.AddChildEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(entriesManager.AddEntry, obj, new object[] { entriesManager.GetEntries(), positionManager.GetPositions(), entriesManager.GetReasons(), entriesManager.GetContinuationCriterias() });
            mainVM.EntriesTreeVM.EditEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(entriesManager.UpdateEntry, obj, new object[] { entriesManager.GetEntries(), positionManager.GetPositions(), entriesManager.GetReasons(), entriesManager.GetContinuationCriterias() });
            mainVM.EntriesTreeVM.DeleteEntryRequest += (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryDeleteWindow>(entriesManager.DeleteEntry, obj, null);
            entriesManager.EntriesChanged += mainVM.EntriesTreeVM.UpdateList;

            mainWindow = new MainWindow { DataContext = mainVM };
            mainWindow.Show();
        }

        private void ShowEntriesWindow()
        {
            var vm = new EntitiesListEditVM<Entry>(
                entriesManager.GetEntries,
                (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(entriesManager.AddEntry, obj, new object[] { entriesManager.GetEntries(), positionManager.GetPositions(), entriesManager.GetReasons(), entriesManager.GetContinuationCriterias() }),
                (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryEditWindow>(entriesManager.UpdateEntry, obj, new object[] { entriesManager.GetEntries(), positionManager.GetPositions(), entriesManager.GetReasons(), entriesManager.GetContinuationCriterias() }),
                (obj) => ShowEntityEditDialog<Entry, EntryVM, EntryDeleteWindow>(entriesManager.DeleteEntry, obj, null));
            entriesManager.EntriesChanged += vm.UpdateList;
            var win = new EntriesListWindow { DataContext = vm, Owner = mainWindow };
            win.Show();
        }

        private void ShowPositionsWindow()
        {
            var vm = new EntitiesListEditVM<Position>(
                positionManager.GetPositions,
                (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(positionManager.AddPosition, obj, new object[] { positionManager.GetPositions(), catalogManager.GetCatalog() }),
                (obj) => ShowEntityEditDialog<Position, PositionVM, PositionEditWindow>(positionManager.UpdatePosition, obj, new object[] { positionManager.GetPositions(), catalogManager.GetCatalog() }),
                (obj) => ShowEntityEditDialog<Position, PositionVM, PositionDeleteWindow>(positionManager.DeletePosition, obj, null));
            positionManager.PositionsChanged += vm.UpdateList;
            var win = new PositionsListWindow { DataContext = vm, Owner = mainWindow };
            win.Show();
        }

        private void ShowCatalogWindow()
        {
            var vm = new EntitiesListEditVM<CatalogItem>(
                catalogManager.GetCatalog,
                (obj) => ShowCatalogItemEditDialog(catalogManager.AddCatalogItem, obj, catalogManager.GetVendors()),
                (obj) => ShowCatalogItemEditDialog(catalogManager.UpdateCatalogItem, obj, catalogManager.GetVendors()),
                //(obj) => ShowEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemEditWindow>(catalogManager.AddCatalogItem, obj, catalogManager.GetVendors()),
                //(obj) => ShowEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemEditWindow>(catalogManager.UpdateCatalogItem, obj, catalogManager.GetVendors()),
                (obj) => ShowEntityEditDialog<CatalogItem, CatalogItemVM, CatalogItemDeleteWindow>(catalogManager.DeleteCatalogItem, obj, null));
            catalogManager.CatalogChanged += vm.UpdateList;
            var win = new CatalogWindow { DataContext = vm, Owner = mainWindow };
            win.Show();
        }

        private void ShowVendorsWindow()
        {
            var vm = new EntitiesListEditVM<Vendor>(
                catalogManager.GetVendors,
                (obj) => ShowVendorEditDialog(catalogManager.AddVendor, obj, null),
                (obj) => ShowVendorEditDialog(catalogManager.UpdateVendor, obj, null),
                //(obj) => ShowEntityEditDialog<Vendor, VendorVM, VendorEditWindow>(catalogManager.AddVendor, obj, null),
                //(obj) => ShowEntityEditDialog<Vendor, VendorVM, VendorEditWindow>(catalogManager.UpdateVendor, obj, null),
                (obj) => ShowEntityEditDialog<Vendor, VendorVM, VendorDeleteWindow>(catalogManager.DeleteVendor, obj, null));
            catalogManager.VendorsChanged += vm.UpdateList;
            var win = new VendorsWindow { DataContext = vm, Owner = mainWindow };
            win.Show();
        }

        private void ShowCatalogItemEditDialog(Func<CatalogItem, bool> executeDelegate, CatalogItem entity, object relationData)
        {
            var win = new CatalogItemEditWindow();
            win.DataContext = new EntityEditVM<CatalogItem, CatalogItemVM>(executeDelegate, win.Close, entity, relationData);
            win.Owner = mainWindow;
            win.ShowDialog();
        }

        private void ShowVendorEditDialog(Func<Vendor, bool> executeDelegate, Vendor entity, object relationData)
        {
            var win = new VendorEditWindow();
            win.DataContext = new EntityEditVM<Vendor, VendorVM>(executeDelegate, win.Close, entity, relationData);
            win.Owner = mainWindow;
            win.ShowDialog();
        }

        private void ShowEntityEditDialog<TEntity, TEntityVM, TEntityV>(Func<TEntity, bool> executeDelegate, TEntity entity, object relationData) where TEntityV : Window, new() where TEntityVM : IEntityVM<TEntity>, new()
        {
            var win = new TEntityV();
            win.DataContext = new EntityEditVM<TEntity, TEntityVM>(executeDelegate, win.Close, entity, relationData);
            win.Owner = mainWindow;
            win.ShowDialog();
        }
    }
}
