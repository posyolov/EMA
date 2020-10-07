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

        private PositionCrudWindowsCreator<VendorVM, VendorEditWindow, VendorsWindow> vendorsWindowsCreator;
        private PositionCrudWindowsCreator<CatalogItemVM, CatalogItemEditWindow, CatalogWindow> catalogWindowsCreator;
        private PositionCrudWindowsCreator positionsWindowsCreator = new PositionCrudWindowsCreator();
        private PositionCrudWindowsCreator<EntryVM, EntryEditWindow, EntriesListWindow> entriesWindowsCreator;

        private MainVM mainVM;
        private MainWindow mainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            mainVM = new MainVM();
            mainWindow = new MainWindow();

            ConfigureMainVM();

            mainWindow.DataContext = mainVM;
            mainWindow.Show();
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
