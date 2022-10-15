using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMovement : MonoBehaviour
{
	public float range = 10.0f;
	NavMeshAgent agent;
	Animator anim;
	bool hasTarget;
	Vector3 targetPoint;
	bool isInCombat = false;

	GameObject target;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
	}
	private void Start()
	{
		//agent.updateRotation = false;
	}
	bool RandomPoint(Vector3 center, float range, out Vector3 result)
	{
		for (int i = 0; i < 30; i++)
		{
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, 1))
			{
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}
	private IEnumerator WaitBeforeMoving()
	{
		anim.SetBool("isWalking", false);
		yield return new WaitForSeconds(2f);
		agent.destination = targetPoint;
		hasTarget = false;
		anim.SetBool("isWalking", true);
		yield return null;
	}
	void Update()
	{
		if (!isInCombat)
		{
			agent.stoppingDistance = 0f;
			if (!agent.pathPending && agent.remainingDistance < 1f)
			{
				Vector3 point;

				if (!hasTarget)
				{
					if (RandomPoint(transform.position, range, out point))
					{
						Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
						targetPoint = point;
						hasTarget = true;
						StartCoroutine(WaitBeforeMoving());
					}
				}


				//if (targetPoint == Vector3.zero)
				//{

				//}
				//if (Vector3.Angle(transform.position, targetPoint) > 5f)
				//{
				//	//Vector3 targetDirection = (targetPoint - transform.position).normalized;
				//	Vector3 newDirection = Vector3.RotateTowards(transform.position, targetPoint, 2f*Time.deltaTime, 0.0f);
				//	Debug.DrawRay(transform.position, newDirection, Color.red);
				//	transform.rotation = Quaternion.LookRotation(newDirection);
				//}
				//else
				//{
				//	agent.destination = targetPoint;
				//	anim.SetBool("isWalking", true);
				//}

			}
			else
			{
				//targetPoint = Vector3.zero;
			}
		}
		else
		{
			agent.destination = target.transform.position;
			
			anim.SetBool("isAttacking", agent.remainingDistance <= agent.stoppingDistance);
		}

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			isInCombat = true;
			target = other.gameObject;
			anim.SetBool("inCombat", isInCombat);
			agent.stoppingDistance = 3f;
		}
	}
}
