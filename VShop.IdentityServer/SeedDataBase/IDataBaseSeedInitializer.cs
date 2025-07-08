namespace VShop.IdentityServer.SeedDataBase
{
    public interface IDataBaseSeedInitializer
    {
        void InitializeSeedRoles();
        void InitializeSeedUsers();
    }
}
