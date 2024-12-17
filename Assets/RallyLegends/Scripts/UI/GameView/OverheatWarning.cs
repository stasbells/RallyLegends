using UnityEngine;

namespace RallyLegends.UI
{
    public class OverheatWarning : MonoBehaviour
    {
        [SerializeField] private CheckEngineSignal _checkEngine;
        [SerializeField] private OverheatView _overheatView;

        private const float ResetValue = 0f;
        private const float DelayValue = 0.4f;
        private const float TargetAlphaValue = 0.8f;
        private const float OverheatingStartTime = 1.5f;

        private void OnDisable()
        {
            _checkEngine.gameObject.SetActive(false);
            _overheatView.ResetColor();
        }

        public void ActiveBy(float counter)
        {
            _checkEngine.gameObject.SetActive(counter != ResetValue && counter > DelayValue);
            _overheatView.ChangeColorAlphaTo(counter > OverheatingStartTime ? TargetAlphaValue : ResetValue);
        }
    }
}