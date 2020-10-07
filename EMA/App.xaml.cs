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

        private PositionCrudWindowsCreator<VendorVM, VendorEditWindow, VendorsWindow> vendorsWindowsCreator;
        private PositionCrudWindowsCreator<CatalogItemVM, CatalogItemEditWindow, CatalogWindow> catalogWindowsCreator;
        private PositionCrudWindowsCreator<PositionEditVM, PositionEditWindow, PositionsListWindow> positionsWindowsCreator;
        private PositionCrudWindowsCreator<EntryVM, EntryEditWindow, EntriesListWindow> entriesWindowsCreator;

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

            ConfigureCrudWindowsCreators();

            ConfigureMainVM();

            mainWindow.DataContext = mainVM;
            mainWindow.Show();
        }

        private void ConfigureCrudWindowsCreators()
        {
            vendorsWindowsCreator = new PositionCrudWindowsCreator<VendorVM, VendorEditWindow, VendorsWindow>(vendorsProxy);
            catalogWindowsCreator = new PositionCrudWindowsCreator<CatalogItemVM, CatalogItemEditWindow, CatalogWindow>(catalogProxy);
            positionsWindowsCreator = new PositionCrudWindowsCreator<PositionEditVM, PositionEditWindow, PositionsListWindow>(positionsProxy);
            entriesWindowsCreator = new PositionCrudWindowsCreator<EntryVM, EntryEditWindow, EntriesListWindow>(entriesProxy);
        }

        private void ConfigureMainVM()
        {
            mainVM.MainMenuVM = new MainMenuVM();
            mainVM.MainMenuVM.ShowVendorsRequest += () => vendorsWindowsCreator.CreateEntitiesListWindow().Show();
            mainVM.MainMenuVM.ShowCatalogRequest += () => catalogWindowsCreator.CreateEntitiesListWindow().Show();
            mainVM.MainMenuVM.ShowPositionsRequest += () => positionsWindowsCreator.CreateEntitiesListWindow().Show();
            mainVM.MainMenuVM.ShowEntriesRequest += () => entriesWindowsCreator.CreateEntitiesListWindow().Show();

            mainVM.PositionsTreeVM = new PositionsTreeVM(positionsProxy);
            mainVM.PositionsTreeVM.ShowAddEntryByPositionRequest += entriesWindowsCreator.ShowEntityAddDialog;
            mainVM.PositionsTreeVM.ShowAddChildPositionRequest += positionsWindowsCreator.ShowEntityAddDialog;
            mainVM.PositionsTreeVM.ShowEditPositionRequest += positionsWindowsCreator.ShowEntityEditDialog;
            mainVM.PositionsTreeVM.ShowDeletePositionRequest += positionsWindowsCreator.ShowEntityDeleteDialog;

            mainVM.EntriesTreeVM = new EntriesTreeVM(entriesProxy);
            mainVM.EntriesTreeVM.ShowAddEntryRequest += entriesWindowsCreator.ShowEntityAddDialog;
            mainVM.EntriesTreeVM.ShowAddChildEntryRequest += entriesWindowsCreator.ShowEntityAddDialog;
            mainVM.EntriesTreeVM.ShowEditEntryRequest += entriesWindowsCreator.CreateEntityEditWindow;
            mainVM.EntriesTreeVM.ShowDeleteEntryRequest += entriesWindowsCreator.CreateEntityDeleteWindow;
        }

    }
}
