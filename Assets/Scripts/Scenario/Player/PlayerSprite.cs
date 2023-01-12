using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario
{
    public class PlayerSprite : MonoBehaviour
    {
        private PlayerController Controller;

        private void Awake()
        {
            transform.parent.TryGetComponent(out Controller);
        }

        public void PlayFootSound()
        {
            Controller.PlayFootSound();
        }
    }
}
