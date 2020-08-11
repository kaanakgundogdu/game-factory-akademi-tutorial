using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float _patrolRange;
	[SerializeField]
	private float _moveSpeed;

	[SerializeField]
	private GameObject _goldWhenEnemyDie;

	private Vector3 _initialPosition;

	private Vector3 _minPatrolPos;
	private Vector3 _maxPatrolPos;
	private Vector3 _destinationPoint;
	private void Awake()
	{
		_initialPosition = transform.position;
		_minPatrolPos = _initialPosition + (Vector3.left * _patrolRange);
		_maxPatrolPos = _initialPosition + (Vector3.right * _patrolRange);

		SetDestination(_maxPatrolPos);
		LoadGoldFromResources();

	}

	private void SetDestination(Vector3 destination)
	{
		_destinationPoint = destination;
	}

	private void Update()
	{
		if(Mathf.Abs( Vector3.Distance(transform.position, _maxPatrolPos)) <  0.1f )
		{
			SetDestination(_minPatrolPos);
		}

		else if (Mathf.Abs(Vector3.Distance(transform.position, _minPatrolPos)) < 0.1f)
		{
			SetDestination(_maxPatrolPos);
		}

		transform.position = Vector3.MoveTowards(transform.position, _destinationPoint, Time.deltaTime * _moveSpeed);
	}

	private void LoadGoldFromResources()
	{
		_goldWhenEnemyDie = Resources.Load<GameObject>("Coin");
	}


	public void Die()
	{
		Instantiate(_goldWhenEnemyDie, transform.position, transform.rotation);
		Destroy(gameObject);
		
	}

}
