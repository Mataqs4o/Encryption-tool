using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a passphrase for encryption:");
        string passphrase = Console.ReadLine();

        var encryptionService = new EncryptionService(passphrase);

        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Encrypt text");
            Console.WriteLine("2. Decrypt text");
            Console.WriteLine("3. Exit");

            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Enter text to encrypt:");
                string plainText = Console.ReadLine();
                string encryptedText = encryptionService.Encrypt(plainText);
                Console.WriteLine($"Encrypted text: {encryptedText}");
            }
            else if (choice == "2")
            {
                Console.WriteLine("Enter text to decrypt:");
                string cipherText = Console.ReadLine();
                try
                {
                    string decryptedText = encryptionService.Decrypt(cipherText);
                    Console.WriteLine($"Decrypted text: {decryptedText}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Decryption failed: {ex.Message}");
                }
            }
            else if (choice == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}