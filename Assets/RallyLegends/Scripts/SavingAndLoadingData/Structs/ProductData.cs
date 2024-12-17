using System;
using RallyLegends.Objects;

namespace RallyLegends.Data.Structs
{
    [Serializable]
    public struct ProductData
    {
        public bool IsBuyed;

        public ProductData(Product product)
        {
            IsBuyed = product.IsBuyed;
        }
    }
}