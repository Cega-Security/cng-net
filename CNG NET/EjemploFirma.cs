using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CNG_NET
{
    class EjemploFirma
    {
        static void Main(string[] args)
        {
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCollection = store.Certificates;
            X509Certificate2Collection certificado = certCollection.Find(X509FindType.FindByIssuerName, "Cega Security", false);

            if (certificado == null)
                throw new ArgumentException("No se encontró el certificado....");
            RSA rsa = certificado[0].GetRSAPrivateKey();

            byte[] firma = rsa.SignData(Encoding.ASCII.GetBytes("Cega Security"), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            Console.WriteLine(Convert.ToBase64String(firma));
            store.Close();

            Console.ReadLine();
        }
    }
}
