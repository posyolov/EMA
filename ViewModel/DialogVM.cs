using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public abstract class DialogVM<TEntity>
    {
        protected readonly Func<TEntity, bool> executeDelegate;
        protected readonly Action closeDialogDelegate;
        public DelegateCommand<TEntity> OkCommand { get; }

        protected DialogVM(Func<TEntity, bool> executeDialogDelegate, Action closeDialogDelegate)
        {
            this.executeDelegate = executeDialogDelegate;
            this.closeDialogDelegate = closeDialogDelegate;

            OkCommand = new DelegateCommand<TEntity>((obj) => OnOk());
        }

        protected abstract void OnOk();
    }
}
