using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;
using System.Text;

namespace ChromeTools
{
    internal class ChromeCookieCrypto
    {
        internal static string Decrypt(byte[] message, byte[] key, int nonSecretPayloadLength = 3)
        {
            const int keyBitSize = 256;
            if (key == null || key.Length != keyBitSize / 8)
                throw new ArgumentException($"Key needs to be {keyBitSize} bit!", nameof(key));
            if (message == null || message.Length == 0)
                throw new ArgumentException("Message required!", nameof(message));

            using var cipherStream = new MemoryStream(message);
            using var cipherReader = new BinaryReader(cipherStream);

            cipherReader.ReadBytes(nonSecretPayloadLength);
            const int nonceBitSize = 96;
            var nonce = cipherReader.ReadBytes(nonceBitSize / 8);
            var cipher = new GcmBlockCipher(new AesEngine());
            const int macBitSize = 128;
            var parameters = new AeadParameters(new KeyParameter(key), macBitSize, nonce);
            cipher.Init(false, parameters);
            var cipherText = cipherReader.ReadBytes(message.Length);
            var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];

            var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
            cipher.DoFinal(plainText, len);

            return Encoding.Default.GetString(plainText);
        }
    }
}