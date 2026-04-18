using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private float _openingTime = 3f;
    [SerializeField] private Vector3 _openPosition = new Vector3(0f, -90f, 0f);
    [SerializeField] private Transform _gateTransform;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = _gateTransform.localEulerAngles;
    }

    public void StartOpening() { StartCoroutine(Open()); }

    private IEnumerator Open()
    {
        float timer = 0f;

        while (timer < _openingTime)
        {
            _gateTransform.localEulerAngles = Vector3.Lerp(_startPosition, _openPosition, timer / _openingTime);

            yield return null;

            timer += Time.deltaTime;
        }
    }
}
