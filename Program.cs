// See https://aka.ms/new-console-template for more information

using Encryptor;

var text = "Halo, dunia!";
var password = "P@ssw0rd";

var cipherText1 = StringEncryptor.Encrypt(text, password);
var cipherText2 = StringEncryptor.Encrypt(text, password);

var decipherText1 = StringEncryptor.Decrypt(cipherText1, password);
var decipherText2 = StringEncryptor.Decrypt(cipherText2, password);

Console.WriteLine($"Text: {text}");
Console.WriteLine($"Password: {password}");
Console.WriteLine($"Chipper Text #1: {cipherText1}");
Console.WriteLine($"Chipper Text #2: {cipherText2}");
Console.WriteLine($"Decipher Text #1: {decipherText1}");
Console.WriteLine($"Decipher Text #2: {decipherText2}");

Console.ReadKey();