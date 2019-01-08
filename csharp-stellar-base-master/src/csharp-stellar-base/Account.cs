using static Stellar.Preconditions;

namespace Stellar
{
    public class Account : ITransactionBuilderAccount
    {
        public KeyPair KeyPair
        {
            get;
            private set;
        }

        public long SequenceNumber
        {
            get;
            private set;
        }
      
        public double Balance
        {
            get;
            private set;
        }

        public Account(KeyPair keyPair)
        {
            this.KeyPair = CheckNotNull(keyPair, "keyPair cannot be null.");
        }

        public Account(KeyPair keyPair, long sequenceNumber)
        {
            this.KeyPair = CheckNotNull(keyPair, "keyPair cannot be null.");
            this.SequenceNumber = sequenceNumber;
        }

        public Account(KeyPair keyPair, double balance)
        {
            this.KeyPair = CheckNotNull(keyPair, "keyPair cannot be null.");
            this.Balance = balance;
        }

        public Account(KeyPair keyPair, long sequenceNumber, double balance)
        {
            this.KeyPair = CheckNotNull(keyPair, "keyPair cannot be null.");
            this.SequenceNumber = sequenceNumber;
            this.Balance = balance;
        }

        public void IncrementSequenceNumber()
        {
            SequenceNumber++;
        }
    }
}
