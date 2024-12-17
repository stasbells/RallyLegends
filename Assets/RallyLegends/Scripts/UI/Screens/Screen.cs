using UnityEngine;

namespace RallyLegends.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Screen : MonoBehaviour
    {
        protected CanvasGroup CanvasGroup;

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        public abstract void Open();

        public abstract void Close();

        protected abstract void SetInteractable(bool value);
    }
}