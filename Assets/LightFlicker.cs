using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light _light;

    const float MAX_ON_TIME = 10f;
    const float MAX_OFF_TIME = 1f;

    float targetTime;
    float timer;

	private void Awake()
	{
        _light = GetComponent<Light>();
	}


    void Start()
    {
        if (_light == null)
        {
            _light = GetComponent<Light>();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= targetTime)
        {
            _light.enabled = !_light.enabled;
            bool isOn = _light.enabled;
            if (isOn)
                targetTime = Random.Range(3f, MAX_ON_TIME);
            else
                targetTime = Random.Range(0.05f, MAX_OFF_TIME);
            timer = 0;
        }
    }
}
