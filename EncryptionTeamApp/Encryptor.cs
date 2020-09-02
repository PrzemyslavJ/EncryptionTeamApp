using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionTeamApp
{
    class Encryptor
    {
        public byte[] CypherBytes_Filling(string textToEncrypt, byte[] cypherBytes)
        {
            byte[] cypherBytes_Supp = new byte[textToEncrypt.Length];

            if (cypherBytes.Length > textToEncrypt.Length)
            {
                for (int i = 0; i < textToEncrypt.Length; i++)
                {
                    cypherBytes_Supp[i] = cypherBytes[i];
                }
            }
            else
            {
                for (int i = 0; i < cypherBytes.Length; i++)
                {
                    cypherBytes_Supp[i] = cypherBytes[i];
                }
            }

            if (cypherBytes.Length < textToEncrypt.Length)
            {
                for (int i = cypherBytes.Length; i < textToEncrypt.Length; i++)
                {
                    cypherBytes_Supp[i] = cypherBytes_Supp[(i - cypherBytes.Length)];
                }
            }

            return cypherBytes_Supp;

        }
        public string Encrypt(string textToEncrypt, string cypher)
        {
            byte[] textToEncryptBytes = Encoding.ASCII.GetBytes(textToEncrypt);
            byte[] cypherBytes = Encoding.ASCII.GetBytes(cypher);
            byte[] encryptionResultInBytes = new byte[textToEncrypt.Length];
            byte[] cypherBytes_Supp = CypherBytes_Filling(textToEncrypt, cypherBytes);

            int bufor = textToEncryptBytes[0];

            for (int i = 0; i < textToEncrypt.Length; i++)
            {
                bufor = bufor + textToEncryptBytes[i] + i - 127 + cypherBytes_Supp[i];
                while (bufor < 0 || bufor > 127)
                {
                    while (bufor < 0)
                    {
                        bufor += 127;
                    }
                    while (bufor > 127)
                    {
                        bufor -= 127;
                    }
                }

                encryptionResultInBytes[i] = (byte)bufor;
            }


            string encryptionResult = Encoding.ASCII.GetString(encryptionResultInBytes);
            return encryptionResult;
        }

        public string Decrypt(string textToDecrypt, string cypher)
        {
            byte[] textToDecryptBytes = Encoding.ASCII.GetBytes(textToDecrypt);
            byte[] cypherBytes = Encoding.ASCII.GetBytes(cypher);
            byte[] decryptionResultBytes = new byte[textToDecryptBytes.Length];
            byte[] cypherBytes_Supp = CypherBytes_Filling(textToDecrypt, cypherBytes);

            for (int m = -1; m < 2; m++)
            {

                double firstC = (double)(textToDecryptBytes[0] + m * 127 + 127 - cypherBytes_Supp[0]) / 2;
                if ((firstC % 1 == 0) && firstC > 0)
                {
                    decryptionResultBytes[0] = (byte)firstC;
                    break;
                }
            }

            for (int i = 1; i < textToDecrypt.Length; i++)
            {
                for (int m = -5; m < 5; m++)
                {
                    double Enter_check = textToDecryptBytes[i] + m * 127 + -textToDecryptBytes[i - 1] - i + 127 - cypherBytes_Supp[i];
                    if (Enter_check > 0)
                    {
                        decryptionResultBytes[i] = (byte)Enter_check;
                        break;
                    }
                }
            }

            string decryptionResult = Encoding.ASCII.GetString(decryptionResultBytes);
            return decryptionResult;
        }
    }
}
