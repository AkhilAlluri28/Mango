namespace Mango.Web.Services.Interfaces
{
    public interface ITokenProvider
    {
        /// <summary>
        /// Gets the JWT Token from cookies.
        /// </summary>
        /// <returns></returns>
        public string? GetToken();

        /// <summary>
        /// Sets the JWT Token in cookies.
        /// </summary>
        /// <param name="token">JWT Token</param>
        public void SetToken(string token);

        /// <summary>
        /// Clears the JWT Token from cookies.
        /// </summary>
        public void ClearToken();
    }
}
