using System;

namespace ViewModel
{
    public class EntityDeleteVM<TEntityVM> : DialogVM
    {
        private readonly Func<TEntityVM, bool> executeDelegate;
        private readonly Action closeDialogDelegate;

        public TEntityVM EntityViewModel { get; }

        public EntityDeleteVM(TEntityVM entityVM, Func<TEntityVM, bool> executeDelegate, Action closeDialogDelegate)
        {
            EntityViewModel = entityVM;
            this.executeDelegate = executeDelegate;
            this.closeDialogDelegate = closeDialogDelegate;
        }

        protected override void OnOk()
        {
            //if (executeDelegate(EntityViewModel.ToModel()))
            //    closeDialogDelegate();
        }
    }
}
