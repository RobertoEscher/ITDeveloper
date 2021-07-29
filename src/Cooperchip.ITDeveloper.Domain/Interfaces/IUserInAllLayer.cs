using System.Collections.Generic;
using System.Security.Claims;

namespace Cooperchip.ITDeveloper.Domain.Interfaces
{
    public interface IUserInAllLayer
    {
        //IDictionary<string, string> DictionaryOfClaims();
        public IDictionary<string, string> DictionayOfClaims();
        //IEnumerable<Claim> ListOfClaims();
        public IEnumerable<Claim> ListOfClaims();

    }
}
