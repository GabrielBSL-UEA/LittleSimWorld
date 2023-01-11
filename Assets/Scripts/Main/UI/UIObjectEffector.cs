using Custom;
using System;
using UnityEngine;

namespace Main.UI
{
    /*
     *  Animator class of a UI Panel
     * 
     *  Set animations from activation and deactivation
     *  
     *  This class uses LeanTween for its tween animations
     */
    public class UIObjectEffector : MonoBehaviour
    {
        [Header("Activation")]
        [SerializeField] private LeanTweenType activationEase;
        [SerializeField] private float activationTime;

        [Header("Activation")]
        [SerializeField] private LeanTweenType deactivationEase;
        [SerializeField] private float deactivationTime;

        private bool _deactivating;

        private void OnEnable()
        {
            _deactivating = false;
            transform.localScale = Vector2.zero;

            transform.LeanScale(Vector2.one, activationTime)
                .setEase(activationEase);
        }

        //Deactivate the panel and sends a callback
        public void Deactivate(CustomEvent callback = null)
        {
            if (_deactivating)
            {
                return;
            }
            _deactivating = true;

            transform.LeanScale(Vector2.zero, deactivationTime).
                setEase(deactivationEase)
                .setOnComplete(() =>
                {
                    callback?.TriggerEvent();
                    gameObject.SetActive(false);
                });
        }

        //------------------
        // GET FUNCTIONS
        //------------------

        public float ActivationTime()
        {
            return activationTime;
        }

        public float DeactivationTime()
        {
            return deactivationTime;
        }
    }
}