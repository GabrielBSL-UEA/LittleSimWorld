using Main;
using UnityEngine;

namespace Scenario
{
    public class SceneTransitionTrigger : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
            {
                return;
            }

            GameManager.Instance.StartSceneTransition(sceneName);
        }
    }
}
