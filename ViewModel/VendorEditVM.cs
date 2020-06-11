using Repository.EF;
using System;

namespace ViewModel
{
    public class VendorEditVM : DialogVM
    {
        private readonly Vendor original;
        private readonly Action<Vendor> executeDelegate;

        public string Name { get; set; }

        public VendorEditVM(Vendor vendor, Action<Vendor> executeDelegate, Action closeDialogDelegate)
            : base(closeDialogDelegate)
        {
            original = vendor;
            this.executeDelegate = executeDelegate;

            Name = original.Name;
        }

        protected override void OnOk()
        {
            //add validation

            original.Name = Name;

            executeDelegate(original);

            closeDialogDelegate();
        }

    }
}
