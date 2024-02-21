namespace Application.Interfaces.Services
{
    /// <summary>
    /// Interface for hashing service
    /// </summary>
    public interface IHashService
    {
        /// <summary>
        /// Hashes the input text
        /// </summary>
        /// <param name="text">The text to hash</param>
        /// <returns>The hashed text</returns>
        public string Hash(string text);

        /// <summary>
        /// Verifies if the input text matches the given hash
        /// </summary>
        /// <param name="text">The text to verify</param>
        /// <param name="hash">The hash to compare with</param>
        /// <returns>True if the text matches the hash, otherwise false</returns>
        public bool Verify(string text, string hash);
    }
}