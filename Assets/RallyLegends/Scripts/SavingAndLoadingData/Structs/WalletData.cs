using System;
using RallyLegends.Objects;

namespace RallyLegends.Data.Structs
{
    [Serializable]
    public struct WalletData
    {
        public int Money;

        public WalletData(Wallet wallet)
        {
            Money = wallet.Money;
        }
    }
}