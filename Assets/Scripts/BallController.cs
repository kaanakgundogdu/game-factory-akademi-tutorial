using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class BallController : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed=1f;
	[SerializeField]
	private float _jumpForce = 5f;

	private bool isGround;

	private Rigidbody _rigidBody;

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}


	void Update()
	{
		CheckInput();

	}


	private void CheckInput()
	{
		if(Input.GetKey(KeyCode.D))
		{
			Move(Vector3.right);
		}

		else if (Input.GetKey(KeyCode.A))
		{
			Move(Vector3.left);
		}
		if (Input.GetKeyDown(KeyCode.Space) && isGround)
		{
			Jump();
		}
	}

	private void Jump()
	{
			/* if(!isgorund) Bu da kullanılabilir eğer && isgorund koymasaydın if de
			 * {
			 *		return;
			 * }*/
		_rigidBody.AddForce(Vector3.up*_jumpForce , ForceMode.Impulse );
	}


	private void OnCollisionEnter(Collision collision)
	{
		isGround = true;

		CollisionCheckEnemy(collision);
	}

	private void CollisionCheckEnemy(Collision collision)
	{
		bool hasCollidedWithEnemy = collision.collider.GetComponent<Enemy>();

		if (!hasCollidedWithEnemy)
		{
			return;
		}
		if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
		{
			Enemy enemy = hit.collider.GetComponent<Enemy>();
			bool isOnTopOfEnemy = enemy != null;
			if (isOnTopOfEnemy)
			{
				enemy.Die();
				return;
			}
		}
			Die();
	}

	private void OnCollisionExit(Collision collision)
	{
		isGround = false;
	}


	private void Move( Vector3 direction)
	{
		//transform.position += direction * Time.deltaTime * moveSpeed;
		_rigidBody.AddForce(direction * moveSpeed, ForceMode.Acceleration);
	}


	private void OnTriggerEnter(Collider other)
	{
		Collectible collectible = other.GetComponent<Collectible>();
		bool isCollectible = collectible != null;

		if(isCollectible)
		{
			collectible.Collect();
		}
	}

	//coroutine ler unity nin belirtilen şartı belirleyerek belirli sonuçlara ulaşmamızı sağlayan metodlar
	private void Die()
	{

		FindObjectOfType<LevelManager>().RestartScene();
		GetComponent<MeshRenderer>().enabled = false;

		/*Coroutine yerine bu da yazılabilir.
		Invoke(nameof(ChangeScene), 1f);*/

	}

}
