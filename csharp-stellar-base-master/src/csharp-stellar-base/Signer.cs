using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System;
using static Stellar.Preconditions;

namespace Stellar
{    
    public class Signer
    {       
        public static Generated.SignerKey Ed25519PublicKey(KeyPair keyPair)
        {            
            if (keyPair == null)
            {
               throw new ArgumentNullException(nameof(keyPair), "keyPair cannot be null");
            }
            return keyPair.XdrSignerKey;
        }

        public static Generated.SignerKey Sha256Hash(byte[] hash)
        {
            if (hash == null)
            {
                throw new ArgumentNullException(nameof(hash), "hash cannot be null");
            }

            var signerKey = new Generated.SignerKey();
            var value = CreateUint256(hash);

            signerKey.Discriminant = Generated.SignerKeyType.Create(Generated.SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_HASH_X);
            signerKey.HashX = value;

            return signerKey;
        }

        //public static Generated.SignerKey PreAuthTx(Transaction tx)
        //{
        //    return PreAuthTx(tx, Network.Current);
        //}

        //public static Generated.SignerKey PreAuthTx(Transaction tx, Network network)
        //{
        //    if (tx == null)
        //        throw new ArgumentNullException(nameof(tx), "tx cannot be null");

        //    return PreAuthTx(tx.Hash(network));
        //}

        public static Generated.SignerKey PreAuthTx(byte[] hash)
        {
            if (hash == null)
                throw new ArgumentNullException(nameof(hash), "hash cannot be null");

            var signerKey = new Generated.SignerKey();
            var value = CreateUint256(hash);

            signerKey.Discriminant = Generated.SignerKeyType.Create(Generated.SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_PRE_AUTH_TX);
            signerKey.PreAuthTx = value;

            return signerKey;
        }

        private static Generated.Uint256 CreateUint256(byte[] hash)
        {
            if (hash.Length != 32)
                throw new ArgumentException("hash must be 32 bytes long");

            var value = new Generated.Uint256();
            value.InnerValue = hash;
            return value;
        }
    }
}