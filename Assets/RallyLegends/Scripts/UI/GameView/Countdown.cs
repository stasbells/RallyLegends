using UnityEngine;

namespace RallyLegends.UI
{
    public class Countdown : MonoBehaviour
    {
        public bool IsDone { get; private set; } = false;

        public void SetCountdown() => IsDone = true;

        public void ResetCountdown() => IsDone = false;
    }
}