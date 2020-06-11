using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public abstract class DialogVM
    {
        protected readonly Action closeDialogDelegate;
        public DelegateCommand<object> OkCommand { get; }

        public DialogVM(Action closeDialogDelegate)
        {
            this.closeDialogDelegate = closeDialogDelegate;
            OkCommand = new DelegateCommand<object>((obj) => OnOk());
        }

        protected abstract void OnOk();
    }
}
