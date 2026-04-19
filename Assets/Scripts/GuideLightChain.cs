using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLightChain : MonoBehaviour
{
    [SerializeField] private List<GuideLight> _guideLights;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _animationLeght = 3f;
    [SerializeField] private int _animationDetail = 4;
    [Space]
    [SerializeField] private Color _guideStateColor = Color.beige;
    [SerializeField] private float _guideStateStrength = 0.3f;
    [SerializeField] private Color _poweredStateColor = Color.limeGreen;
    [SerializeField] private float _poweredStateStrength = 1f;


    private ChainStateType _chainState = ChainStateType.Off;
    private Coroutine _waveCoroutine;

    private enum ChainStateType
    {
        Off,
        Guide,
        Powered
    }

    void OnEnable()
    {
        for (int i = 0; i < _guideLights.Count; i++)
            _guideLights[i].Setup(Color.black, 0f, 0f);
    }

    void Start()
    {
        GameManager.Instance.OnActivateGuideCables += ActivateGuide;
    }

    public void ActivateGuide()
    {
        if (_chainState != ChainStateType.Off) return;

        _chainState = ChainStateType.Guide;
        SetGuideLights();

        _waveCoroutine = StartCoroutine(WaveAnimation(-1));
    }

    public void ActivateFullPower()
    {
        if (_chainState == ChainStateType.Powered) return;

        _chainState = ChainStateType.Powered;
        SetGuideLights();

        if (_waveCoroutine != null)
        {
            StopCoroutine(_waveCoroutine);
            _waveCoroutine = null;
        }

        _waveCoroutine = StartCoroutine(WaveAnimation(1));
    }

    private void SetGuideLights()
    {
        for (int i = 0; i < _guideLights.Count; i++)
        {
            if (_chainState == ChainStateType.Guide)
            {
                _guideLights[i].Setup(_guideStateColor, _guideStateStrength, (i % _animationDetail) / _animationLeght);
            }
            else
            {
                _guideLights[i].Setup(_poweredStateColor, _poweredStateStrength, (i % _animationDetail) / _animationLeght);
            }
        }
    }

    private IEnumerator WaveAnimation(int direction)
    {
        while (true)
        {
            for (int i = 0; i < _guideLights.Count; i++)
            {
                _guideLights[i].UpdateLight(_animationCurve.Evaluate(_guideLights[i].Timer / _animationLeght));

                if (direction == 1)
                {
                    _guideLights[i].Timer += Time.deltaTime;

                    if (_guideLights[i].Timer > _animationLeght) _guideLights[i].Timer -= _animationLeght;
                }
                else
                {
                    _guideLights[i].Timer -= Time.deltaTime;

                    if (_guideLights[i].Timer < 0f) _guideLights[i].Timer += _animationLeght;
                }
            }

            yield return null;
        }
    }
}
