using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenShake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration;
        bool waiting;


    // Update is called once per frame
    void Update()
    {
        if(start) {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking() {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0.0f;

        while(elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }

    public void Stop(float duration, float timeS){
        if(waiting){
            return;
        }
        Time.timeScale = timeS;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration){
        if(!gameObject.GetComponent<Pause>().isPaused){
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
        }
    }
}
