using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class EntityEditVM<TEntity, TEntityVM> : DialogVM<TEntity> where TEntityVM : IEntityVM<TEntity>, new()
    {
        private readonly TEntity entityModel;

        public IEntityVM<TEntity> EntityViewModel { get; set; }
        public object RelationData { get; set; }

        public EntityEditVM(Func<TEntity, bool> executeDelegate, Action closeDialogDelegate, TEntity entity, object relationData = null)
            : base(executeDelegate, closeDialogDelegate)
        {
            entityModel = entity;
            EntityViewModel = new TEntityVM();
            EntityViewModel.ToViewModel(entityModel);

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
