using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Scenario
{
    public class LightOscilator : MonoBehaviour
    {
        [SerializeField] private float oscilationDuration;
        [SerializeField] private Vector2 IntensityRange;
        [SerializeField] private Vector2 outerRadiusRange;
        [SerializeField] private LeanTweenType oscilationType;

        Light2D _light2D;

        private void Awake()
        {
            TryGetComponent(out _light2D);

            LeanTween.value(0, 1, oscilationDuration / 2)
                .setEase(oscilationType)
                .setLoopPingPong()
                .setOnUpdate((float value) =>
                {
                    _light2D.intensity = Mathf.Lerp(IntensityRange.x, IntensityRange.y, value);
                    _light2D.pointLightOuterRadius = Mathf.Lerp(outerRadiusRange.x, outerRadiusRange.y, value);
                });
        }

        private void OnDestroy()
        {
            LeanTween.cancel(gameObject);
        }
    }
}