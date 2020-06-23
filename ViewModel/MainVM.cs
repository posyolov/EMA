namespace ViewModel
{
    public class MainVM
    {
        public MainMenuVM MainMenuVM { get; }
        public PositionsTreeVM PositionsTreeVM { get; set; }
        public EntriesTreeVM EntriesTreeVM { get; set; }

        public MainVM()
        {
            MainMenuVM = new MainMenuVM();
        }

    }
}
