          // Automatically generated by xdrgen 
          // DO NOT EDIT or your changes may be overwritten

          namespace Stellar.Generated
{


// === xdr source ============================================================
//  struct SetOptionsOp
//  {
//      AccountID* inflationDest; // sets the inflation destination
//  
//      uint32* clearFlags; // which flags to clear
//      uint32* setFlags;   // which flags to set
//  
//      // account threshold manipulation
//      uint32* masterWeight; // weight of the master account
//      uint32* lowThreshold;
//      uint32* medThreshold;
//      uint32* highThreshold;
//  
//      string32* homeDomain; // sets the home domain
//  
//      // Add, update or remove a signer for the account
//      // signer is deleted if the weight is 0
//      Signer* signer;
//  };
//  ===========================================================================
public class SetOptionsOp {
  public SetOptionsOp () {}
  public AccountID InflationDest { get; set; }
  public Uint32 ClearFlags { get; set; }
  public Uint32 SetFlags { get; set; }
  public Uint32 MasterWeight { get; set; }
  public Uint32 LowThreshold { get; set; }
  public Uint32 MedThreshold { get; set; }
  public Uint32 HighThreshold { get; set; }
  public String32 HomeDomain { get; set; }
  public Signer Signer { get; set; }

  public static void Encode(IByteWriter stream, SetOptionsOp encodedSetOptionsOp) {
    if (encodedSetOptionsOp.InflationDest != null) {
    XdrEncoding.EncodeInt32(1, stream);
    AccountID.Encode(stream, encodedSetOptionsOp.InflationDest);
    } else {
    XdrEncoding.EncodeInt32(0, stream);
    }
    if (encodedSetOptionsOp.ClearFlags != null) {
    XdrEncoding.EncodeInt32(1, stream);
    Uint32.Encode(stream, encodedSetOptionsOp.ClearFlags);
    } else {
    XdrEncoding.EncodeInt32(0, stream);
    }
    if (encodedSetOptionsOp.SetFlags != null) {
    XdrEncoding.EncodeInt32(1, stream);
    Uint32.Encode(stream, encodedSetOptionsOp.SetFlags);
    } else {
    XdrEncoding.EncodeInt32(0, stream);
    }
    if (encodedSetOptionsOp.MasterWeight != null) {
    XdrEncoding.EncodeInt32(1, stream);
    Uint32.Encode(stream, encodedSetOptionsOp.MasterWeight);
    } else {
    XdrEncoding.EncodeInt32(0, stream);
    }
    if (encodedSetOptionsOp.LowThreshold != null) {
    XdrEncoding.EncodeInt32(1, stream);
    Uint32.Encode(stream, encodedSetOptionsOp.LowThreshold);
    } else {
    XdrEncoding.EncodeInt32(0, stream);
    }
    if (encodedSetOptionsOp.MedThreshold != null) {
    XdrEncoding.EncodeInt32(1, stream);
    Uint32.Encode(stream, encodedSetOptionsOp.MedThreshold);
    } else {
    XdrEncoding.EncodeInt32(0, stream);
    }
    if (encodedSetOptionsOp.HighThreshold != null) {
    XdrEncoding.EncodeInt32(1, stream);
    Uint32.Encode(stream, encodedSetOptionsOp.HighThreshold);
    } else {
    XdrEncoding.EncodeInt32(0, stream);
    }
    if (encodedSetOptionsOp.HomeDomain != null) {
    XdrEncoding.EncodeInt32(1, stream);
    String32.Encode(stream, encodedSetOptionsOp.HomeDomain);
    } else {
    XdrEncoding.EncodeInt32(0, stream);
    }
    if (encodedSetOptionsOp.Signer != null) {
    XdrEncoding.EncodeInt32(1, stream);
    Signer.Encode(stream, encodedSetOptionsOp.Signer);
    } else {
    XdrEncoding.EncodeInt32(0, stream);
    }
  }
  public static SetOptionsOp Decode(IByteReader stream) {
    SetOptionsOp decodedSetOptionsOp = new SetOptionsOp();
    int inflationDestPresent = XdrEncoding.DecodeInt32(stream);
    if (inflationDestPresent != 0) {
    decodedSetOptionsOp.InflationDest = AccountID.Decode(stream);
    }
    int clearFlagsPresent = XdrEncoding.DecodeInt32(stream);
    if (clearFlagsPresent != 0) {
    decodedSetOptionsOp.ClearFlags = Uint32.Decode(stream);
    }
    int setFlagsPresent = XdrEncoding.DecodeInt32(stream);
    if (setFlagsPresent != 0) {
    decodedSetOptionsOp.SetFlags = Uint32.Decode(stream);
    }
    int masterWeightPresent = XdrEncoding.DecodeInt32(stream);
    if (masterWeightPresent != 0) {
    decodedSetOptionsOp.MasterWeight = Uint32.Decode(stream);
    }
    int lowThresholdPresent = XdrEncoding.DecodeInt32(stream);
    if (lowThresholdPresent != 0) {
    decodedSetOptionsOp.LowThreshold = Uint32.Decode(stream);
    }
    int medThresholdPresent = XdrEncoding.DecodeInt32(stream);
    if (medThresholdPresent != 0) {
    decodedSetOptionsOp.MedThreshold = Uint32.Decode(stream);
    }
    int highThresholdPresent = XdrEncoding.DecodeInt32(stream);
    if (highThresholdPresent != 0) {
    decodedSetOptionsOp.HighThreshold = Uint32.Decode(stream);
    }
    int homeDomainPresent = XdrEncoding.DecodeInt32(stream);
    if (homeDomainPresent != 0) {
    decodedSetOptionsOp.HomeDomain = String32.Decode(stream);
    }
    int signerPresent = XdrEncoding.DecodeInt32(stream);
    if (signerPresent != 0) {
    decodedSetOptionsOp.Signer = Signer.Decode(stream);
    }
    return decodedSetOptionsOp;
  }
}
}