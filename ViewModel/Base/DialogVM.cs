using System;

namespace ViewModel
{
    public abstract class DialogVM
    {
        public DelegateCommand<object> OkCommand { get; }

        protected DialogVM()
        {
            OkCommand = new DelegateCommand<object>((obj) => OnOk());
        }

        protected abstract void OnOk();
    }
}
