using System;

namespace ViewModel
{
    public class MainVM
    {
        public MainMenuVM MainMenuVM { get; }
        public PositionsTreeVM PositionsTreeVM { get; set; }
                
        public MainVM()
        {
            MainMenuVM = new MainMenuVM();

        }

    }
}
