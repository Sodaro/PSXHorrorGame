using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxe : Entity
{
    float MaxAngleDeflection = 15.0f;
    float SpeedOfPendulum = 1.0f;

    float timeActive = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
		{
            timeActive += Time.deltaTime;
            float angle = MaxAngleDeflection * Mathf.Sin(timeActive * SpeedOfPendulum);
            transform.parent.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
