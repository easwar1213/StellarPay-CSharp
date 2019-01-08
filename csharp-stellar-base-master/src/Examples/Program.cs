using Stellar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static Stellar.Preconditions;

namespace Examples
{
    using UrlContent = KeyValuePair<string, string>;
    class Program
    {
        static string horizon_url = "https://horizon-testnet.stellar.org/";
        static string friendbotUrl = "https://friendbot.stellar.org/";
        static string network_passphrase = "Test SDF Network ; September 2015";

        static void Main(string[] args)
        {
            Stellar.Network.CurrentNetwork = network_passphrase;
            string signUp,seedKey,desSeedKey;
            long transAmt;
            int accountOperation;
            //Account myAccount;
            Console.WriteLine("Welcome to Setellar Network");
            Console.WriteLine("----------------------------");
            //var myKeyPair = KeyPair.FromSeed("SBLSPXGEEDCVXLBSNGLD7JLTWDFKWKZBQCRUWENY6YFXZAKFFB27CEH4");
            do
            {
                Console.Write("Do you have Stellar Account(Y/N)?:");
                signUp = Console.ReadLine();                
                if (signUp.ToUpper() == "Y")
                {
                    Console.WriteLine("Please enter your SeedKey:");
                    seedKey = Console.ReadLine();
                    do
                    {
                        //Console.WriteLine("\n 1.PublicKey/Address\n 2.SequenceNum \n 3.Balance");
                        Console.WriteLine("\n 1.Load Balance \n 2.Check Balance \n 3.Account Details \n 4.Transaction \n 5.Merge Account \n 6.Transaction with MultiSign \n 7.Exit \n");
                        Console.Write("\n Please enter your choice of Account operations:");
                        accountOperation = Convert.ToInt16(Console.ReadLine());
                        var myKeyPair = KeyPair.FromSeed(seedKey);                        
                        switch (accountOperation)
                        {
                            case 1:
                                LoadBalance(myKeyPair.Address);
                                break;
                            case 2:
                                CheckBalance(myKeyPair.Address);
                                break;
                            case 3:                                
                                AccountDetails(myKeyPair.Address);
                                break;
                            case 4:
                                Console.WriteLine("\n Enter your Destination Address:");
                                desSeedKey = Console.ReadLine();
                                var desKeyPaire = KeyPair.FromSeed(desSeedKey);
                                Console.WriteLine("\n Enter your Transfer Amount:");
                                transAmt = Convert.ToInt64(Console.ReadLine());
                                SimplePayment(myKeyPair, desKeyPaire, transAmt * Stellar.One.Value);
                                break;
                            case 5:
                                Console.WriteLine("\n Enter the Account to Merge Address:");
                                desSeedKey = Console.ReadLine();
                                var desSignKey = Signer.Ed25519PublicKey(KeyPair.FromSeed(desSeedKey));
                                Console.WriteLine("\n Enter Weight:");
                                uint signWgt = Convert.ToUInt32(Console.ReadLine());
                                Console.WriteLine("\n Enter Master Weight:");
                                uint masWgt = Convert.ToUInt32(Console.ReadLine());
                                Console.WriteLine("\n Enter Low Threshold:");
                                uint lowThreshold = Convert.ToUInt32(Console.ReadLine());
                                Console.WriteLine("\n Enter Medium Threshold:");
                                uint medThreshold = Convert.ToUInt32(Console.ReadLine());
                                Console.WriteLine("\n Enter High Threshold:");
                                uint highThreshold = Convert.ToUInt32(Console.ReadLine());
                                //SetOptionsOperation(KeyPair inflationDestination, uint clearFlags, uint setFlags,
                                //uint masterKeyWeight, uint lowThreshold, uint mediumThreshold, uint highThreshold, 
                                //string homeDomain, Generated.SignerKey signer, uint signerWeight)
                                //MergeAccount(myKeyPair, 0, 0, 1, 0, 2, 2, "aa", desKeyPairef, 1);
                                MergeAccount(myKeyPair, masWgt, lowThreshold, medThreshold, highThreshold, desSignKey, signWgt);
                                break;
                            case 6:
                                Console.WriteLine("\n Enter your Destination Address:");
                                desSeedKey = Console.ReadLine();
                                var desKeyPairr = KeyPair.FromSeed(desSeedKey);
                                Console.WriteLine("\n Enter your Transfer Amount:");
                                transAmt = Convert.ToInt64(Console.ReadLine());
                                Console.WriteLine("\n Enter your Partner Sign/Secret Key:");
                                var srcSeedKey = Console.ReadLine();
                                var srcKeyPairr = KeyPair.FromSeed(srcSeedKey);
                                MultiSignPayment(myKeyPair, desKeyPairr,srcKeyPairr, transAmt * Stellar.One.Value);
                                break;
                            case 7:
                                break;
                            default:
                                Console.WriteLine("\n Invalid Choice!!!");
                                break;
                        }
                    } while (accountOperation < 7);
                }
                else if (signUp.ToUpper() == "N")
                {
                    //Console.WriteLine("Create New Account");
                    //var myKeyPair = KeyPair.FromSeed("SBLSPXGEEDCVXLBSNGLD7JLTWDFKWKZBQCRUWENY6YFXZAKFFB27CEH4");
                    //myAccount = new Account(myKeyPair);
                    //var randomAccountKeyPair = CreateRandomAccount(myAccount, 1000 * Stellar.One.Value);
                    CreateAccount();
                }
                else
                {
                    Console.WriteLine("Invalid Answer!!!");
                }
            } while (signUp.ToUpper() != "Y" && signUp.ToUpper()!="N");            

            //Payment(myKeyPair, randomAccountKeyPair, 10 * Stellar.One.Value);

            // Wait for input to prevent the cmd window from closing
            Console.Read();
        }

        static string GetResult(string msg)
        {
            using (var client = new HttpClient())
            {
                string response = client.GetStringAsync(horizon_url + WebUtility.UrlEncode(msg)).Result;
                return response;
            }
        }

        static HttpResponseMessage PostResult(string tx)
        {
            using (var client = new HttpClient())
            {
                var body = new List<UrlContent>();
                body.Add(new UrlContent("tx", tx));
                var formUrlEncodedContent = new FormUrlEncodedContent(body);
                return client.PostAsync(horizon_url + "transactions", formUrlEncodedContent).Result;
            }
        }       

        private static long GetSequenceNum(string address)
        {
            using (var client = new HttpClient())
            {
                string response = client.GetStringAsync(horizon_url + "accounts/" + address).Result;
                var json = JObject.Parse(response);
                return (long)json["sequence"];
            }
        }

        private static double GetBalance(string address)
        {
            using (var client = new HttpClient())
            {
                string response = client.GetStringAsync(horizon_url + "accounts/" + address).Result;
                var json = JObject.Parse(response);                
                var balance = json["balances"][0]["balance"];                
                return (double)balance;
            }
        }

        private static void CheckBalance(string address)
        {
            using (var client = new HttpClient())
            {
                string response = client.GetStringAsync(horizon_url + "accounts/" + address).Result;
                var json = JObject.Parse(response);
                var balance = json["balances"][0]["balance"];
                Console.WriteLine("\n Balances for account: " + json["account_id"]);
                Console.Write(" Type:" + json["balances"][0]["asset_type"] + ", Balance: " + json["balances"][0]["balance"] + "\n");
            }
        }

        private static void AccountDetails(string address)
        {
            using (var client = new HttpClient())
            {
                string response = client.GetStringAsync(horizon_url + "accounts/" + address).Result;
                var json = JObject.Parse(response);
                Console.WriteLine(json);
            }
        }

        private static void CreateAccount()
        {
            var pair = KeyPair.Random();
            Console.WriteLine("Your has been created successfully");
            Console.WriteLine("PublicKey/Address/AccountID:\n"+pair.Address);
            Console.WriteLine("SeedKey/SecrectKey:\n" + pair.Seed);            
        }

        private static void LoadBalance(string address)
        {            
            using (var client = new HttpClient())
            {
                string response = client.GetStringAsync(friendbotUrl + "?addr=" + address).Result;
                var json = JObject.Parse(response);
                Console.WriteLine(json);
            }
        }

        static KeyPair CreateRandomAccount(Account source, long nativeAmount)
        {
            var dest = KeyPair.Random();

            var operation =
                new CreateAccountOperation.Builder(dest, nativeAmount)
                .SetSourceAccount(source.KeyPair)
                .Build();

            source.IncrementSequenceNumber();

            Stellar.Transaction transaction =
                new Stellar.Transaction.Builder(source)
                .AddOperation(operation)
                .Build();

            transaction.Sign(source.KeyPair);

            var tx = transaction.ToEnvelopeXdrBase64();

            var response = PostResult(tx);

            Console.WriteLine("response:" + response.ReasonPhrase);
            Console.WriteLine(dest.Address);
            Console.WriteLine(dest.Seed);

            return dest;
        }        

        private static void DecodeTransactionResult(string result)
        {
            var bytes = Convert.FromBase64String(result);
            var reader = new Stellar.Generated.ByteReader(bytes);
            var txResult = Stellar.Generated.TransactionResult.Decode(reader);
        }

        private static void DecodeTxFee(string result)
        {
            var bytes = Convert.FromBase64String(result);
            var reader = new Stellar.Generated.ByteReader(bytes);
            var txResult = Stellar.Generated.LedgerEntryChanges.Decode(reader);
        }

        static void SimplePayment(KeyPair from, KeyPair to, long amount)
        {
            Account source = new Account(from, GetSequenceNum(from.Address));

            // load asset
            Asset asset = new Asset();

            var operation =
                new PaymentOperation.Builder(to, asset, amount)
                .SetSourceAccount(from)
                .Build();

            source.IncrementSequenceNumber();

            Stellar.Transaction transaction =
                new Stellar.Transaction.Builder(source)
                .AddOperation(operation)
                .Build();

            transaction.Sign(source.KeyPair);

            var tx = transaction.ToEnvelopeXdrBase64();

            var response = PostResult(tx);

            Console.WriteLine(response.ReasonPhrase);
        }

        //static void MergeAccount(KeyPair from,uint clearflag,uint setflag,uint masterwgt,uint lowthreshold,uint mediumthreshold,uint highthreshold,string domain,Stellar.Generated.SignerKey to,uint wgt)
        static void MergeAccount(KeyPair from,uint masterwgt, uint lowthreshold, uint mediumthreshold, uint highthreshold, Stellar.Generated.SignerKey to, uint wgt)
        {
            Account source = new Account(from, GetSequenceNum(from.Address));

            //SetOptionsOperation(KeyPair inflationDestination, uint clearFlags, uint setFlags,
            //                    uint masterKeyWeight, uint lowThreshold, uint mediumThreshold, uint highThreshold,
            //                    string homeDomain, Generated.SignerKey signer, uint signerWeight)
            //                    MergeAccount(myKeyPair, 0, 0, 1, 0, 2, 2, null, desKeyPair, 1);

            // load asset
            Asset asset = new Asset();
             
            var operation =
               new SetOptionsOperation.Builder()
               .SetSourceAccount(from)
               .SetMasterKeyWeight(masterwgt)
               .SetLowThreshold(lowthreshold)
               .SetMediumThreshold(mediumthreshold)
               .SetHighThreshold(highthreshold)
               .SetSigner(to,wgt)
               //.SetInflationDestination(from)
               //.SetClearFlags(clearflag)
               //.SetSetFlags(setflag)               
               //.SetHomeDomain(domain)
               .Build();

            source.IncrementSequenceNumber();

            Stellar.Transaction transaction =
                new Stellar.Transaction.Builder(source)
                .AddOperation(operation)
                .Build();

            transaction.Sign(source.KeyPair);

            var tx = transaction.ToEnvelopeXdrBase64();

            var response = PostResult(tx);

            Console.WriteLine(response.ReasonPhrase);
        }

        static void MultiSignPayment(KeyPair from, KeyPair to, KeyPair fromm, long amount)
        {
            Account source = new Account(from, GetSequenceNum(from.Address));
            Account sourcee = new Account(fromm);

            // load asset
            Asset asset = new Asset();

            var operation =
                new PaymentOperation.Builder(to, asset, amount)
                .SetSourceAccount(from)
                .Build();

            source.IncrementSequenceNumber();

            Stellar.Transaction transaction =
                new Stellar.Transaction.Builder(source)
                .AddOperation(operation)
                .Build();

            transaction.Sign(source.KeyPair);
            transaction.Sign(sourcee.KeyPair);

            var tx = transaction.ToEnvelopeXdrBase64();

            var response = PostResult(tx);

            Console.WriteLine(response.ReasonPhrase);
        }
    }
}