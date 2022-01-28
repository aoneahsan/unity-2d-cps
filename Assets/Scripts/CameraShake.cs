using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float shakeDuration = .5f;

    [SerializeField]
    float shakeMagnitude = .25f;

    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void PlayShakeAnimation()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float delta = 0;
        while (delta < shakeDuration)
        {
            transform.position =
                initialPosition +
                (Vector3) Random.insideUnitCircle * shakeMagnitude;
            delta += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
