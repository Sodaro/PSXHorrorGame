using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseLight : MonoBehaviour
{
    [SerializeField] float maxDist = 20f;
    [SerializeField] float speed = 5.0f;
    Light _light;
	private void Start()
	{
        _light = GetComponent<Light>();
	}
	void Update()
    {
        _light.range = Mathf.PingPong(Time.time * speed, maxDist);
    }
}
