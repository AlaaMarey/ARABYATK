using Arabytak.Core.Entities.Identity_Entities;


namespace Arabytak.Core.Repositories.Contract
{
    public  interface ITokenServices
    {
        public string CreateJWTAsync(ApplicationUser AppUser);




    }
}
