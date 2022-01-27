using System;
using System.Security.Cryptography;
using System.Text;
using DiplomaChat.Common.Infrastructure.Configuration;

namespace DiplomaChat.Common.Infrastructure.Generators.Hashing
{
    public class SaltedHashGenerator : IHashGenerator
    {
        private readonly string _salt;
        private readonly HashAlgorithm _hashAlgorithm;

        public SaltedHashGenerator(HashConfiguration hashConfiguration) : this(hashConfiguration, MD5.Create())
        {
        }

        public SaltedHashGenerator(HashConfiguration hashConfiguration, HashAlgorithm hashAlgorithm)
        {
            _salt = hashConfiguration.Salt;
            _hashAlgorithm = hashAlgorithm;
        }

        public byte[] GenerateHash(string inputText)
        {
            var bytes = Encoding.UTF8.GetBytes(inputText + _salt);

            var hash = _hashAlgorithm.ComputeHash(bytes);

            return hash;
        }

        public string GenerateStringHash(string inputText)
        {
            var result = Convert.ToBase64String(GenerateHash(inputText));
            return result;
        }
    }
}