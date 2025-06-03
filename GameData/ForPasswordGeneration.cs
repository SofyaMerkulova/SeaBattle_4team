using System.Security.Cryptography;
namespace GameData
{
    /// <summary>  
    /// Для хеширования пароля и его проверки
    /// </summary>
    public static class ForPasswordGeneration
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;  
        private const int Iterations = 100_000;
        /// <summary>  
        /// Создает хеш пароля и возвращает его в нужном формате, содержащий соль+ключ
        /// </summary>
        public static string HashPassword(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);

                var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256).GetBytes(KeySize);
                var hashBytes = new byte[SaltSize + KeySize];

                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(key, 0, hashBytes, SaltSize, KeySize);

                return Convert.ToBase64String(hashBytes);
            }
        }
        /// <summary>  
        /// Проверяет что введенный пароль соответсвует захешированному
        /// </summary>

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword); 
            var salt = new byte[SaltSize];
            var key = new byte[KeySize];

            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            Array.Copy(hashBytes, SaltSize, key, 0, KeySize);

            var keyToCheck = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256).GetBytes(KeySize);

            return CryptographicOperations.FixedTimeEquals(key, keyToCheck);
        }
    }
}
