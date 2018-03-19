//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SharpTestsEx;

//namespace Solicita.TG.Utils.Testes
//{
//    [TestClass]
//    public class CriptografiaTestes
//    {
//        [TestMethod]
//        public void testeDeveCriptografarUmaStringAPartirDeUmaChave()
//        {
//            Criptografia crp = new Criptografia();

//            String textoOriginal = "Mogi das Cruzes";

//            String textoEncriptografado = crp.Encrypt(textoOriginal);

//            textoEncriptografado.Should().Be.EqualTo("kNyBiKZP508z1UZ8CKaqxg==");

//            String textoDescriptografado = crp.Decrypt(textoEncriptografado);

//            textoDescriptografado.Should().Be.EqualTo(textoOriginal);

//            ///yrhgfdgfgfdgh
//        }
//    }
//}
