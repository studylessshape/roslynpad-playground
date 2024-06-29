#r "nuget: System.ComponentModel, 4.3.0"

using System.Security.Cryptography;

const string KEY = "2add3d1b-43d6-402b-8948-b279abe1d7d2";
var plaintext = "byd123456";
var salt = Guid.NewGuid().ToString();
salt.Dump("Salt");

using var crypto = new SHA1CryptoServiceProvider();
var msg = Encoding.UTF8.GetBytes($"{plaintext}/{KEY}/{salt}");
var encrypted = crypto.ComputeHash(msg);

var pass = Convert.ToBase64String(encrypted);
pass.Dump("Password");