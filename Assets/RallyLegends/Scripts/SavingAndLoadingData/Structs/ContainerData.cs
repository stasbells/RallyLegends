using System;
using System.Collections.Generic;
using RallyLegends.Objects;

namespace RallyLegends.Data.Structs
{
    [Serializable]
    public struct ContainerData
    {
        public ProductData[] Items;

        public ContainerData(IReadOnlyList<Product> items)
        {
            Items = new ProductData[items.Count];

            for (int i = 0; i < items.Count; i++)
            {
                Items[i] = new ProductData(items[i]);
            }
        }
    }
}