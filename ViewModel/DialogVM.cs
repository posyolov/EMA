using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class DialogVM
    {
        public object ViewModel { get; set; }

        public event Action Ok;
        public DelegateCommand<object> OkCommand { get; }
        public DialogVM(object viewModel)
        {
            ViewModel = viewModel;

            OkCommand = new DelegateCommand<object>((obj) => Ok?.Invoke());
        }
    }
}
