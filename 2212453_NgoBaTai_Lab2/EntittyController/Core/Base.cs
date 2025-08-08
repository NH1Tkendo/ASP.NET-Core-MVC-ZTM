using EntityModel;


namespace EntityController.Core
{
    public abstract class Base
    {
        public EF db;
        public Base()
        {
            db = new EF();
        }
    }
}
