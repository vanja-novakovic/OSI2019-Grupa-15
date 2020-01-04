using System;

namespace Eve.IndexControls
{
    public abstract class IndexControlElement
    {

        public abstract void Create(double height = 0.0, double width = 0.0);
        public abstract void Delete(object selectedItem, double height = 0.0, double width = 0.0);
        public abstract void Edit(object selectedItem, double height = 0.0, double width = 0.0);
        public abstract void Details(object selectedItem, double height = 0.0, double width = 0.0);
    }
}
