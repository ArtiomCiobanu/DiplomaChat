namespace DiplomaChat.Common.Authorization.Readers
{
    public interface ITokenReader
    {
        public string GetAccountId(string token);
        string GetClaim(string token, string claimType);
    }
}
