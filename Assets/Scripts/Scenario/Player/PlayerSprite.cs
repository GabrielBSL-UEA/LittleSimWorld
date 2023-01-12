using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario
{
    public class PlayerSprite : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dirtPS;
        private PlayerController Controller;

        private void Awake()
        {
            transform.parent.TryGetComponent(out Controller);
        }

        public void PlayFootEffect()
        {
            Controller.PlayFootSound();
            dirtPS.Play();
        }
    }
}
