using UnityEngine;
using UnityEngine.Splines;

namespace RallyLegends.Control
{
    public class DriftSplineData : MonoBehaviour
    {
        public SplineData<float> Drift = new();
        public SplineContainer SplineContainer;

        public SplineContainer Container
        {
            get
            {
                if (SplineContainer == null)
                    SplineContainer = GetComponent<SplineContainer>();
                return SplineContainer;
            }
            set => SplineContainer = value;
        }
    }
}