using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC4
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    class MainClass
    {
        public static void Main(string[] args)
        {


            //Encrypt("abcd", "test");
            //Encrypt("aaaa", "test");
            // Encrypt("0x61626364", "0x74657374");

            // Decrypt("ÏíDu", "test");
            // Decrypt("ÏîFp", "test");
            Decrypt("0xcfed4475", "0x74657374");


        }
        public static string Decrypt(string cipherText, string key)
        {
            bool isHexa = false;

            //Handling the hexadecimal input case 
            if (cipherText.Substring(0, 2) == "0x")
            {
                cipherText = hexToText(cipherText);
                key = hexToText(key);

                isHexa = true;
            }

            StringBuilder result = new StringBuilder();
            int x, y, j = 0;
            int[] S_Arr = new int[256];

            //filling s array with elements from 0-255
            for (int i = 0; i < 256; i++)
            {
                S_Arr[i] = i;
            }

            //permute by KSA
            for (int i = 0; i < 256; i++)
            {
                //rule: j= j+ s[i]+s[j] %256
                j = (j + S_Arr[i] + key[i % key.Length]) % 256;

                //swap s[i] with s[j]
                x = S_Arr[i];
                S_Arr[i] = S_Arr[j];
                S_Arr[j] = x;
            }

            //permute by PRGA
            //wil use y as a container for i result
            j = 0;
            for (int i = 0; i < cipherText.Length; i++)
            {

                //y= i= (i+1) % 256
                y = (i + 1) % 256;
                j = (j + S_Arr[y]) % 256;

                //swap s[i] with s[j]
                x = S_Arr[y];
                S_Arr[y] = S_Arr[j];
                S_Arr[j] = x;


                //note for me: character is implicitly converrted to int and XORed 
                result.Append((char)(cipherText[i] ^ S_Arr[(S_Arr[y] + S_Arr[j]) % 256]));
            }

            if (isHexa == true)
            {
                string resInHexa = textToHexa(result.ToString());
                Console.WriteLine(resInHexa);
                return resInHexa;

            }

            Console.WriteLine(result.ToString());
            return result.ToString();

        }


        public static string Encrypt(string plainText, string key)
        {
            bool isHexa = false;

            //Handling the hexadecimal input case 
            if (plainText.Substring(0,2)== "0x")
            {
               plainText= hexToText(plainText);
               key = hexToText(key);

               isHexa = true;
            }

            StringBuilder result = new StringBuilder();
            int x, y, j = 0;
            int[] S_Arr = new int[256];

            //filling s array with elements from 0-255
            for (int i = 0; i < 256; i++)
            {
                S_Arr[i] = i;
            }

            //permute by KSA
            for (int i = 0; i < 256; i++)
            {
                //rule: j= j+ s[i]+s[j] %256
                j = (j + S_Arr[i] + key[i % key.Length]) % 256;

                //swap s[i] with s[j]
                x = S_Arr[i];
                S_Arr[i] = S_Arr[j];
                S_Arr[j] = x;
            }

            //permute by PRGA
            //wil use y as a container for i result
            j = 0;
            for (int i = 0; i < plainText.Length; i++)
            {

                //y= i= (i+1) % 256
                y = (i + 1) % 256;
                j = (j + S_Arr[y]) % 256;

                //swap s[i] with s[j]
                x = S_Arr[y];
                S_Arr[y] = S_Arr[j];
                S_Arr[j] = x;


                //note for me: character is implicitly converrted to int and XORed 
                result.Append((char)(plainText[i] ^ S_Arr[(S_Arr[y] + S_Arr[j]) % 256]));
            }
            
            if (isHexa == true)
            {
                string resInHexa= textToHexa(result.ToString());
                Console.WriteLine(resInHexa);
                return resInHexa;

            }
            else
            
            Console.WriteLine(result.ToString());
            return result.ToString();

        }

        public static string hexToText(string hexaString)
        {
           // 2 hexa = 8 bits = 1 letter
         
            string result = "";
            for (int i = 2; i < hexaString.Length; i+=2)
            {
                int decValue = Convert.ToInt32(hexaString.Substring(i, 2),16);
                result += (char)decValue;
            }
            return result;

        }

        public static string textToHexa(string plainText)
        {
            // 2 hexa = 8 bits = 1 letter
            string result = "";
            string concatResult = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                result += Convert.ToString((int)plainText[i], 16);
                
            }

            concatResult = string.Concat("0x", result);
            return concatResult;

        }


    }
}