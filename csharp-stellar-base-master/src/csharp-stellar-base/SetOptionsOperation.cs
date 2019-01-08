using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stellar.Preconditions;

namespace Stellar
{   
    public class SetOptionsOperation : Operation
    {
        //private SetOptionsOperation(KeyPair inflationDestination, uint clearFlags, uint setFlags,
        //    uint masterKeyWeight, uint lowThreshold, uint mediumThreshold,
        //    uint highThreshold, string homeDomain, Generated.SignerKey signer, uint signerWeight)
        private SetOptionsOperation(uint masterKeyWeight, uint lowThreshold, uint mediumThreshold,
            uint highThreshold, Generated.SignerKey signer, uint signerWeight)
        {            
            MasterKeyWeight = masterKeyWeight;
            LowThreshold = lowThreshold;
            MediumThreshold = mediumThreshold;
            HighThreshold = highThreshold;
            //InflationDestination = inflationDestination;
            //ClearFlags = clearFlags;
            //SetFlags = setFlags;
            //HomeDomain = homeDomain;
            Signer = signer;
            SignerWeight = signerWeight;
        }

        //public KeyPair InflationDestination { get; private set; }

        //public uint ClearFlags { get; }

        //public uint SetFlags { get; }

        public uint MasterKeyWeight { get; }

        public uint LowThreshold { get; }

        public uint MediumThreshold { get; }

        public uint HighThreshold { get; }

        //public string HomeDomain { get; }

        public Generated.SignerKey Signer { get; }

        public uint SignerWeight { get; }

        //public override OperationThreshold Threshold
        //{
        //    get => OperationThreshold.High;
        //}

        public override Generated.Operation.OperationBody ToOperationBody()
        {
            var op = new Generated.SetOptionsOp();
            //if (InflationDestination != null)
            //{
            //    var inflationDestination = new Generated.AccountID();
            //    //inflationDestination.InnerValue = InflationDestination.XdrPublicKey;
            //    op.InflationDest = inflationDestination;
            //}

            //if (ClearFlags != 0)
            //{
            //    var clearFlags = new Generated.Uint32();
            //    clearFlags.InnerValue = ClearFlags;
            //    op.ClearFlags = clearFlags;
            //}

            //if (SetFlags != 0)
            //{
            //    var setFlags = new Generated.Uint32();
            //    setFlags.InnerValue = SetFlags;
            //    op.SetFlags = setFlags;
            //}

            if (MasterKeyWeight != 0)
            {
                var uint32 = new Generated.Uint32();
                uint32.InnerValue = MasterKeyWeight;
                op.MasterWeight = uint32;
            }

            if (LowThreshold != 0)
            {
                var uint32 = new Generated.Uint32();
                uint32.InnerValue = LowThreshold;
                op.LowThreshold = uint32;
            }

            if (MediumThreshold != 0)
            {
                var uint32 = new Generated.Uint32();
                uint32.InnerValue = MediumThreshold;
                op.MedThreshold = uint32;
            }

            if (HighThreshold != 0)
            {
                var uint32 = new Generated.Uint32();
                uint32.InnerValue = HighThreshold;
                op.HighThreshold = uint32;
            }

            //if (HomeDomain != null)
            //{
            //    var homeDomain = new Generated.String32();
            //    homeDomain.InnerValue = HomeDomain;
            //    op.HomeDomain = homeDomain;
            //}

            if (Signer != null)
            {
                var signer = new Generated.Signer();
                var weight = new Generated.Uint32();
                weight.InnerValue = SignerWeight & 0xFF;
                signer.Key = Signer;
                signer.Weight = weight;
                op.Signer = signer;
            }

            var body = new Generated.Operation.OperationBody();
            body.Discriminant = Generated.OperationType.Create(Generated.OperationType.OperationTypeEnum.SET_OPTIONS);
            body.SetOptionsOp = op;
            return body;
        }
      
        public class Builder
        {            
            //private KeyPair inflationDestination;            
            private uint masterKeyWeight;
            private uint lowThreshold;
            private uint mediumThreshold;
            private uint highThreshold;
            //private uint clearFlags;
            //private string homeDomain;
            //private uint setFlags;
            private Generated.SignerKey signer;
            private uint signerWeight;
            private KeyPair sourceAccount;

            public Builder(Generated.SetOptionsOp op)
            {
                //if (op.InflationDest != null)
                //    inflationDestination = KeyPair.FromXdrPublicKey(
                //        op.InflationDest.InnerValue);
                //if (op.ClearFlags != null)
                //    clearFlags = op.ClearFlags.InnerValue;
                //if (op.SetFlags != null)
                //    setFlags = op.SetFlags.InnerValue;
                if (op.MasterWeight != null)
                    masterKeyWeight = op.MasterWeight.InnerValue;
                if (op.LowThreshold != null)
                    lowThreshold = op.LowThreshold.InnerValue;
                if (op.MedThreshold != null)
                    mediumThreshold = op.MedThreshold.InnerValue;
                if (op.HighThreshold != null)
                    highThreshold = op.HighThreshold.InnerValue;
                //if (op.HomeDomain != null)
                //    homeDomain = op.HomeDomain.InnerValue;
                if (op.Signer != null)
                {
                    signer = op.Signer.Key;
                    signerWeight = op.Signer.Weight.InnerValue & 0xFF;
                }
            }
           
            public Builder()
            {

            }
           
            //public Builder SetInflationDestination(KeyPair inflationDestination)
            //{
            //    this.inflationDestination = inflationDestination;
            //    return this;
            //}
            
            //public Builder SetClearFlags(uint clearFlags)
            //{
            //    this.clearFlags = clearFlags;
            //    return this;
            //}
          
            //public Builder SetSetFlags(uint setFlags)
            //{
            //    this.setFlags = setFlags;
            //    return this;
            //}
           
            public Builder SetMasterKeyWeight(uint masterKeyWeight)
            {
                this.masterKeyWeight = masterKeyWeight;
                return this;
            }
         
            public Builder SetLowThreshold(uint lowThreshold)
            {
                this.lowThreshold = lowThreshold;
                return this;
            }
          
            public Builder SetMediumThreshold(uint mediumThreshold)
            {
                this.mediumThreshold = mediumThreshold;
                return this;
            }
           
            public Builder SetHighThreshold(uint highThreshold)
            {
                this.highThreshold = highThreshold;
                return this;
            }
           
            //public Builder SetHomeDomain(string homeDomain)
            //{
            //    if (homeDomain.Length > 32)
            //        throw new ArgumentException("Home domain must be <= 32 characters");
            //    this.homeDomain = homeDomain;
            //    return this;
            //}
           
            public Builder SetSigner(Generated.SignerKey signer, uint weight)
            {
                this.signer = CheckNotNull(signer, "signer cannot be null");
                if (weight == 0)
                {
                    throw new ArgumentNullException(nameof(weight), "weight cannot be null");
                }

                signerWeight = weight & 0xFF;
                return this;
            }
           
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                this.sourceAccount = sourceAccount;
                return this;
            }
           
            public SetOptionsOperation Build()
            {
                //var operation = new SetOptionsOperation(inflationDestination, clearFlags,
                //    setFlags, masterKeyWeight, lowThreshold, mediumThreshold, highThreshold,
                //    homeDomain, signer, signerWeight);
                var operation = new SetOptionsOperation(masterKeyWeight, lowThreshold, mediumThreshold, highThreshold,
                    signer, signerWeight);
                if (sourceAccount != null)
                    operation.SourceAccount = sourceAccount;
                return operation;
            }
        }
    }
}