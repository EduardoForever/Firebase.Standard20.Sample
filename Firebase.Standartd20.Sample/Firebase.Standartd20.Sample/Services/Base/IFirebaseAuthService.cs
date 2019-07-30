using Google.Cloud.Firestore.V1;
using System.Threading.Tasks;

namespace Firebase.Standartd20.Sample.Services
{
    public interface IFirebaseAuthService
    {
        FirestoreClient Client { get; }

        Task<bool> LoginAsync(string email, string password);
    }
}