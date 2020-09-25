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
        private readonly EntitiesMapper entitiesMapper = new EntitiesMapper();

        private VendorsProxy vendorsProxy;
        private CatalogProxy catalogProxy;
        private PositionsProxy positionsProxy;
        private EntriesProxy entriesProxy;

        private EntitiyWindowsCreator<VendorVM, VendorEditWindow, VendorsWindow> vendorsWindowsCreator;
        private EntitiyWindowsCreator<CatalogItemVM, CatalogItemEditWindow, CatalogWindow> catalogWindowsCreator;
        private EntitiyWindowsCreator<PositionVM, PositionEditWindow, PositionsListWindow> positionsWindowsCreator;
        private EntitiyWindowsCreator<EntryVM, EntryEditWindow, EntriesListWindow> entriesWindowsCreator;

        private MainVM mainVM;
        private MainWindow mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            vendorsProxy = new VendorsProxy(entitiesMapper);
            catalogProxy = new CatalogProxy(entitiesMapper);
            positionsProxy = new PositionsProxy(entitiesMapper);
            entriesProxy = new EntriesProxy(entitiesMapper);

            mainVM = new MainVM();
            mainWindow = new MainWindow();

            ConfigureEntityWindowsCreators();

            ConfigureMainVM();

            mainWindow.DataContext = mainVM;
            mainWindow.Show();
        }

        private void ConfigureEntityWindowsCreators()
        {
            vendorsWindowsCreator = new EntitiyWindowsCreator<VendorVM, VendorEditWindow, VendorsWindow>(vendorsProxy, mainWindow);
            catalogWindowsCreator = new EntitiyWindowsCreator<CatalogItemVM, CatalogItemEditWindow, CatalogWindow>(catalogProxy, mainWindow);
            positionsWindowsCreator = new EntitiyWindowsCreator<PositionVM, PositionEditWindow, PositionsListWindow>(positionsProxy, mainWindow);
            entriesWindowsCreator = new EntitiyWindowsCreator<EntryVM, EntryEditWindow, EntriesListWindow>(entriesProxy, mainWindow);
        }

        private void ConfigureMainVM()
        {
            mainVM.MainMenuVM = new MainMenuVM();
            mainVM.MainMenuVM.ShowVendorsRequest += vendorsWindowsCreator.ShowEntitiesListWindow;
            mainVM.MainMenuVM.ShowCatalogRequest += catalogWindowsCreator.ShowEntitiesListWindow;
            mainVM.MainMenuVM.ShowPositionsRequest += positionsWindowsCreator.ShowEntitiesListWindow;
            mainVM.MainMenuVM.ShowEntriesRequest += entriesWindowsCreator.ShowEntitiesListWindow;

            mainVM.PositionsTreeVM = new PositionsTreeVM(positionsProxy);
            mainVM.PositionsTreeVM.ShowAddEntryByPositionRequest += entriesWindowsCreator.ShowEntityAddDialog;
            mainVM.PositionsTreeVM.ShowAddChildPositionRequest += positionsWindowsCreator.ShowEntityAddDialog;
            mainVM.PositionsTreeVM.ShowEditPositionRequest += positionsWindowsCreator.ShowEntityEditDialog;
            mainVM.PositionsTreeVM.ShowDeletePositionRequest += positionsWindowsCreator.ShowEntityDeleteDialog;

            mainVM.EntriesTreeVM = new EntriesTreeVM(entriesProxy);
            mainVM.EntriesTreeVM.ShowAddEntryRequest += entriesWindowsCreator.ShowEntityAddDialog;
            mainVM.EntriesTreeVM.ShowAddChildEntryRequest += entriesWindowsCreator.ShowEntityAddDialog;
            mainVM.EntriesTreeVM.ShowEditEntryRequest += entriesWindowsCreator.ShowEntityEditDialog;
            mainVM.EntriesTreeVM.ShowDeleteEntryRequest += entriesWindowsCreator.ShowEntityDeleteDialog;
        }

    }
}
