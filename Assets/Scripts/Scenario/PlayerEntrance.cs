using Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario
{
    public class PlayerEntrance : MonoBehaviour
    {
        [SerializeField] private Transform[] entrances;
        [SerializeField] private Transform camera;
        [SerializeField] private bool moveCamera;

        public void TransportPlayer(int positionIndex)
        {
            if(positionIndex >= entrances.Length)
            {
                return;
            }

            GameManager.Instance.Player().transform.position = entrances[positionIndex].position;

            if (!moveCamera)
            {
                return;
            }

            camera.position = entrances[positionIndex].position + new Vector3(0, 0, camera.position.z);
        }
    }
}
