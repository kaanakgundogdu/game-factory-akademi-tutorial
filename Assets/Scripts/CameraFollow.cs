using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float smoothness=0.5f;
	[SerializeField]
	private Vector3 offset;

	private void Start()
	{
		offset = transform.position - target.position;
	}
	//Normal Update kaç fps ise pc
	//Late update frame sonunda çalışıyor.


	private void FixedUpdate() //Fİxed sabit 58 yada 61 kere
	{
		if (target == null)
		{
			return;
		}

		//Lerp boşlukları doldurarak taşıyor. Başlnagıçtan sonuna akdar taşıyor.
		transform.position = Vector3.Lerp(transform.position, target.position + offset,  Time.deltaTime *  smoothness);

	}

}
