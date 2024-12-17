using UnityEngine;
using UnityEngine.UI;
using RallyLegends.Objects;

namespace RallyLegends.GameLogic
{
    public abstract class Selector : MonoBehaviour
    {
        [SerializeField] protected Button NextButton;
        [SerializeField] protected Button PrevButton;
        [SerializeField] protected Image LockImage;

        protected int CurrentProductIndex;

        public abstract void ChangeProduct(int changer);

        public abstract Product GetCurrentProduct();

        public abstract void PayProduct();

        protected abstract void SelectProduct(int index);

        protected abstract void ShowInfo();
    }
}