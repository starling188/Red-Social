
namespace Aplication.ValidationAcount
{
    public class TokenService
    {
        public string GeneradorToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
