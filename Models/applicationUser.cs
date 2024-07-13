using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace mongomvc.Models
{
    [CollectionName("users")]
    public class applicationUser :MongoIdentityUser <Guid>
    {
    }
}
