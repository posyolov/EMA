using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class EntityDeleteVM<TEntity> : DialogVM
    {
        private readonly Func<TEntity, bool> executeDelegate;
        private readonly Action closeDialogDelegate;

        public TEntity Entity { get; }

        public EntityDeleteVM(TEntity entity, Func<TEntity, bool> executeDelegate, Action closeDialogDelegate)
        {
            Entity = entity;
            this.executeDelegate = executeDelegate;
            this.closeDialogDelegate = closeDialogDelegate;
        }

        protected override void OnOk()
        {
            if (executeDelegate(Entity))
                closeDialogDelegate();
        }
    }
}
