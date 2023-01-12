using Audio;
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

        [Header("Deactivation")]
        [SerializeField] private LeanTweenType deactivationEase;
        [SerializeField] private float deactivationTime;

        [Header("Background")]
        [SerializeField] private CanvasGroup background;

        private bool _deactivating;
        private float _initialBackgroundAlpha;

        private void Awake()
        {
            if(background != null)
            {
                _initialBackgroundAlpha = background.alpha;
            }
        }

        private void OnEnable()
        {
            SetBackground(0, 0);

            _deactivating = false;
            transform.localScale = Vector2.zero;

            transform.LeanScale(Vector2.one, activationTime)
                .setEase(activationEase);

            SetBackground(_initialBackgroundAlpha, activationTime);
            AudioManager.Instance.PlaySFX("s_Panel");
        }

        //Deactivate the panel and sends a callback
        public void Deactivate(CustomEvent callback = null, Action action = null)
        {
            if (_deactivating)
            {
                return;
            }
            _deactivating = true;

            SetBackground(0, deactivationTime);
            AudioManager.Instance.PlaySFX("s_Panel");

            transform.LeanScale(Vector2.zero, deactivationTime).
                setEase(deactivationEase)
                .setOnComplete(() =>
                {
                    action?.Invoke();
                    callback?.TriggerEvent();
                    gameObject.SetActive(false);
                });
        }

        public void SetBackground(float value, float time)
        {
            if (background == null)
            {
                return;
            }

            background.LeanAlpha(value, time);
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