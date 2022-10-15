using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Utility
{
	public static void SwapObjectPositions(GameObject lhs, GameObject rhs)
	{
		Vector3 tempPos = lhs.transform.position;
		lhs.transform.position = rhs.transform.position;
		rhs.transform.position = tempPos;
	}

    public static void SwapPosition(this GameObject lhs, GameObject target)
    {
        Vector3 tempPos = lhs.transform.position;
        lhs.transform.position = target.transform.position;
        target.transform.position = tempPos;
    }

    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
