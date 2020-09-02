using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionTeamApp
{
    class EncryptorTest
    {
        public bool TestEncryption()
        {
            var encryptor = new Encryptor();
            int successfullConversion = 0;
            int failuredConversion = 0;
            for (var i = 0; i < 100; i++)
            {
                var randomText = Guid.NewGuid().ToString();
                var randomCypher = Guid.NewGuid().ToString();
                var encryptedRandomText = encryptor.Encrypt(randomText, randomCypher);
                var decryptedRandomText = encryptor.Decrypt(encryptedRandomText, randomCypher);

                if (decryptedRandomText == randomText)
                {
                    successfullConversion++;
                }
                else
                {
                    failuredConversion++;
                }
            }

            return successfullConversion == 100;
        }
    }
}
