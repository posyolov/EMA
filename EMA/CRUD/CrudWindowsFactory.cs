using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EMA
{
    abstract public class CrudWindowsFactory
    {
        public abstract Window GetListWindow();
        public abstract Window GetAddWindow(int entityId);
        public abstract Window GetEditWindow(int entityId);
        public abstract Window GetDeleteWindow(int entityId);
    }
}
