using System;
using System.Collections.Generic;
using RallyLegends.GameLogic;
using RallyLegends.Objects;

namespace RallyLegends.Data.Structs
{
    [Serializable]
    public struct ColorsData
    {
        public ProductData[] Colors;

        public ColorsData(IReadOnlyList<Product> items)
        {
            int index = 0;
            int count = 0;

            for (int i = 0; i < items.Count; i++)
            {
                Container car = items[i].GetComponentInChildren<Container>();

                for (int j = 0; j < car.Items.Count; j++)
                    count++;
            }

            Colors = new ProductData[count];

            for (int i = 0; i < items.Count; i++)
            {
                Container car = items[i].GetComponentInChildren<Container>();

                for (int j = 0; j < car.Items.Count; j++)
                    Colors[index++] = new ProductData(car.Items[j]);
            }
        }
    }
}