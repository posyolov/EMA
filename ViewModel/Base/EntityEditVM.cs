using System;

namespace ViewModel
{
    public class EntityEditVM<TEntity, TEntityVM> : DialogVM where TEntityVM : IEntityVM<TEntity>, new()
    {
        private readonly Func<TEntity, bool> executeDelegate;
        private readonly Action closeDialogDelegate;

        private readonly TEntity entityModel;

        public IEntityVM<TEntity> EntityViewModel { get; set; }
        public object RelationData { get; set; }

        public EntityEditVM(TEntity entity, Func<TEntity, bool> executeDelegate, Action closeDialogDelegate, object relationData = null)
        {
            entityModel = entity;
            EntityViewModel = new TEntityVM();
            EntityViewModel.ToViewModel(entityModel);

            this.executeDelegate = executeDelegate;
            this.closeDialogDelegate = closeDialogDelegate;

            RelationData = relationData;
        }

        protected override void OnOk()
        {
            if (EntityViewModel.IsValid)
            {
                EntityViewModel.ToModel(entityModel);
                if (executeDelegate(entityModel))
                    closeDialogDelegate();
            }
        }
    }
}
