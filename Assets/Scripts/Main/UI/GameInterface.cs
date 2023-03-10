using System.Collections;
using UnityEngine;
using Enums;
using TMPro;
using System;
using Custom;
using Audio;

namespace Main.UI
{
    /*
     *  Main class to interact with the UI elements. Translate the UIManager class commands to the UI
     * 
     *  Other classes can inherit from this class depending the scene context
     */
    public class GameInterface : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] private Canvas messageCanvas;
        [SerializeField] private CanvasGroup transitionCanvas;

        //Text speed animation
        [Header("Animations")]
        [SerializeField] private float messageFillSpeed = .075f;
        [SerializeField] private float sceneTransitionDuration = .8f;

        [Header("MessageBox")]
        [SerializeField] private UIObjectEffector MessageBox;
        [SerializeField] private TextMeshProUGUI MessageText;

        [Header("Default")]
        [SerializeField] private CustomEvent defaultEventAfterConversationEnd;

        private Coroutine _textCoroutine;
        private CustomEvent _currentEvent;

        public bool InMessageAnimation { get; private set; }

        private void Awake()
        {
            transitionCanvas.alpha = 1;
            transitionCanvas.LeanAlpha(0, sceneTransitionDuration);
        }


        //Run the text animation or imediatly stops in case to be called while the animation is running
        public bool ShowMessageOnScreen(string message, Characters character, Mood charMood, CustomEvent eventToTrigger)
        {
            if(_textCoroutine != null)
            {
                StopCoroutine(_textCoroutine);
                _textCoroutine = null;

                MessageText.text = message;
                InMessageAnimation = false;

                return false;
            }

            //Keep track of the coroutine in case to stop it early
            _textCoroutine = StartCoroutine(TextAnimation(message, eventToTrigger));
            return true;
        }

        //Deactivate the message panel, finishing the conversation. Also sends a callback after the deactivation animation ends
        public void CloseConversation()
        {
            if(_currentEvent == null)
            {
                MessageBox.Deactivate(defaultEventAfterConversationEnd);
                return;
            }

            MessageBox.Deactivate(_currentEvent);
            _currentEvent = null;
        }

        //IEnumerator that animates the message box
        private IEnumerator TextAnimation(string text, CustomEvent newEvent)
        {
            _currentEvent?.TriggerEvent();

            InMessageAnimation = true;
            _currentEvent = newEvent;

            MessageText.text = "";

            //Wait for the activation animation of the message box
            if (!MessageBox.gameObject.activeSelf)
            {
                MessageBox.gameObject.SetActive(true);
                yield return new WaitForSeconds(MessageBox.ActivationTime());
            }

            var textArray = text.ToCharArray();

            //Set the message text, char by char
            for (int i = 0; i < textArray.Length; i++)
            {
                MessageText.text += textArray[i];

                AudioManager.Instance.PlaySFX("s_MessageChar");
                yield return new WaitForSeconds(messageFillSpeed);
            }

            InMessageAnimation = false;
            _textCoroutine = null;
        }

        //Functions for the special panel of the inherit classes
        public virtual void OpenSpecialPanel()
        {

        }

        public virtual void CloseSpecialPanel()
        {

        }

        public void PlayUIClick()
        {
            AudioManager.Instance.PlaySFX("s_UIClick");
        }

        public void SceneTransitionAnimation(Action callback)
        {
            LeanTween.cancel(gameObject);

            transitionCanvas.LeanAlpha(1, sceneTransitionDuration)
                .setOnComplete(() =>
                {
                    callback?.Invoke();
                });
        }
    }
}