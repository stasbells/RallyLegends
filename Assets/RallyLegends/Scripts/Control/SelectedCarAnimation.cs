using UnityEngine;
using RallyLegends.Data;
using UnityEngine.SceneManagement;

namespace RallyLegends.Control
{
    [RequireComponent(typeof(AnimateCarAlongSpline))]
    public class SelectedCarAnimation : MonoBehaviour
    {
        private const float DurationAdjustmentValue = 5f;

        private void Update()
        {
            if (SceneManager.GetActiveScene().buildIndex == Constants.MenuSceneIndex & this.gameObject.activeSelf)
                transform.RotateAround(gameObject.transform.position, Vector3.up, DurationAdjustmentValue * Time.deltaTime);
        }
    }
}