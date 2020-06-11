using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class DialogVM
    {
        public event Action Ok;
        public DelegateCommand<object> OkCommand { get; }
        public DialogVM()
        {
            OkCommand = new DelegateCommand<object>((obj) => Ok?.Invoke());
        }
    }
}
