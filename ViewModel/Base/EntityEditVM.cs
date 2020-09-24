using System;

namespace ViewModel
{
    public class EntityEditVM<TEntityVM> : DialogVM
    {
        private readonly Func<TEntityVM, bool> executeDelegate;
        private readonly Action closeDialogDelegate;

        public TEntityVM EntityViewModel { get; set; }

        public EntityEditVM(TEntityVM entityVM, Func<TEntityVM, bool> executeDelegate, Action closeDialogDelegate)
        {
            EntityViewModel = entityVM;

            this.executeDelegate = executeDelegate;
            this.closeDialogDelegate = closeDialogDelegate;
        }

        protected override void OnOk()
        {
            if (executeDelegate(EntityViewModel))
                closeDialogDelegate();
        }
    }
}
