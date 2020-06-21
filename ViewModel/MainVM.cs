namespace ViewModel
{
    public class MainVM
    {
        public MainMenuVM MainMenuVM { get; }
        public PositionsTreeVM PositionsTreeVM { get; set; }
        public EntriesListVM EntriesListVM { get; set; }

        public MainVM()
        {
            MainMenuVM = new MainMenuVM();
        }

    }
}
