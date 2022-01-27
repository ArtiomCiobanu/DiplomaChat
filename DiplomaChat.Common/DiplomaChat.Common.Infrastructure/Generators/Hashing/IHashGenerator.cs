namespace DiplomaChat.Common.Infrastructure.Generators.Hashing
{
    public interface IHashGenerator
    {
        public byte[] GenerateHash(string inputText);
        public string GenerateStringHash(string inputText);
    }
}