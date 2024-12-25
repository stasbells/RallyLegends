using UnityEngine;
using RallyLegends.Data.Structs;

namespace RallyLegends.Objects
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private int _money;

        public int Money => _money;

        public void Pay(int price) => _money -= price;

        public void LoadData(WalletData data) => _money = data.Money;
    }
}