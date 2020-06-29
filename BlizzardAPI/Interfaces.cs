namespace SoftRes.BlizzardAPI
{
    public class BlizzardItemAPI : IItemAPIItemId, IItemAPIItemLocale, IItemAPINamespace
    {
        public void ItemId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IItemAPIItemId Locale(string locale)
        {
            throw new System.NotImplementedException();
        }

        public IItemAPIItemLocale Namespace(string ns)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IItemAPINamespace
    {
        IItemAPIItemLocale Namespace(string ns);
    }

    public interface IItemAPIItemId
    {
        void ItemId(int id);
    }

    public interface IItemAPIItemLocale
    {
        IItemAPIItemId Locale(string locale);
    }
}