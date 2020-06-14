using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public interface IEntityVM<Entity>
    {
        public bool IsValid { get; }
        public void ToViewModel(Entity model);
        public void ToModel(Entity model);
    }
}
