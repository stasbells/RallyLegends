using UnityEngine;

namespace RallyLegends.Objects
{
    public class Map : Product
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;

        public int Id => _id;
        public Sprite Sprite => _sprite;
    }
}