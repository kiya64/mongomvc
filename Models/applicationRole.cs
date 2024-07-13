using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace mongomvc.Models
{
    [CollectionName("Role")]
    public class applicationRole :MongoIdentityRole<Guid>
    {
    }
}
