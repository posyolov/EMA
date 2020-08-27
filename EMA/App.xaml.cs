using System.Windows;
using ViewModel;
using Model;
using Repository;

namespace EMA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        VendorsManager vendorsManager;
        CatalogManager catalogManager;
        PositionsManager positionsManager;
        EntryReasonsManager entryReasonsManager;
        EntryContinuationCriteriaManager entryContinuationCriteriaManager;
        EntriesManager entriesManager;

        EntitiyWindowsCreator<Vendor, VendorVM, VendorEditWindow, VendorsWindow> vendorsWindowsCreator;
        EntitiyWindowsCreator<CatalogItem, CatalogItemVM, CatalogItemEditWindow, CatalogWindow> catalogWindowsCreator;
        EntitiyWindowsCreator<Position, PositionVM, PositionEditWindow, PositionsListWindow> positionsWindowsCreator;
        EntitiyWindowsCreator<Entry, EntryVM, EntryEditWindow, EntriesListWindow> entriesWindowsCreator;

        MainVM mainVM;
        MainWindow mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            mainVM = new MainVM();
            mainWindow = new MainWindow();

            ConfigureEntitiesManagers();

            ConfigureEntityWindowsCreators();

            ConfigureMainVM();

            mainWindow.DataContext = mainVM;
            mainWindow.Show();
        }

        private void ConfigureEntitiesManagers()
        {
            vendorsManager = new VendorsManager();
            catalogManager = new CatalogManager(vendorsManager);
            positionsManager = new PositionsManager(catalogManager);
            entryReasonsManager = new EntryReasonsManager();
            entryContinuationCriteriaManager = new EntryContinuationCriteriaManager();
            entriesManager = new EntriesManager(positionsManager, entryReasonsManager, entryContinuationCriteriaManager);
        }

        private void ConfigureEntityWindowsCreators()
        {
            vendorsWindowsCreator = new EntitiyWindowsCreator<Vendor, VendorVM, VendorEditWindow, VendorsWindow>(vendorsManager, mainWindow);
            catalogWindowsCreator = new EntitiyWindowsCreator<CatalogItem, CatalogItemVM, CatalogItemEditWindow, CatalogWindow>(catalogManager, mainWindow);
            positionsWindowsCreator = new EntitiyWindowsCreator<Position, PositionVM, PositionEditWindow, PositionsListWindow>(positionsManager, mainWindow);
            entriesWindowsCreator = new EntitiyWindowsCreator<Entry, EntryVM, EntryEditWindow, EntriesListWindow>(entriesManager, mainWindow);
        }

        private void ConfigureMainVM()
        {
            mainVM.MainMenuVM = new MainMenuVM();
            mainVM.MainMenuVM.ShowVendorsRequest += vendorsWindowsCreator.ShowEntitiesListWindow;
            mainVM.MainMenuVM.ShowCatalogRequest += catalogWindowsCreator.ShowEntitiesListWindow;
            mainVM.MainMenuVM.ShowPositionsRequest += positionsWindowsCreator.ShowEntitiesListWindow;
            mainVM.MainMenuVM.ShowEntriesRequest += entriesWindowsCreator.ShowEntitiesListWindow;

            mainVM.PositionsTreeVM = new PositionsTreeVM(positionsManager);
            mainVM.PositionsTreeVM.ShowAddEntryByPositionRequest += entriesWindowsCreator.ShowEntityAddDialog;
            mainVM.PositionsTreeVM.ShowAddChildPositionRequest += positionsWindowsCreator.ShowEntityAddDialog;
            mainVM.PositionsTreeVM.ShowEditPositionRequest += positionsWindowsCreator.ShowEntityEditDialog;
            mainVM.PositionsTreeVM.ShowDeletePositionRequest += positionsWindowsCreator.ShowEntityDeleteDialog;

            mainVM.EntriesTreeVM = new EntriesTreeVM(entriesManager);
            mainVM.EntriesTreeVM.ShowAddEntryRequest += entriesWindowsCreator.ShowEntityAddDialog;
            mainVM.EntriesTreeVM.ShowAddChildEntryRequest += entriesWindowsCreator.ShowEntityAddDialog;
            mainVM.EntriesTreeVM.ShowEditEntryRequest += entriesWindowsCreator.ShowEntityEditDialog;
            mainVM.EntriesTreeVM.ShowDeleteEntryRequest += entriesWindowsCreator.ShowEntityDeleteDialog;
        }

    }
}
