using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ScrollBehaviour : MonoBehaviour
{
    public Vector3 scrollDirection = -Vector3.right;
    public float speed = 3f;
	
	public float oscillateRadius = 0f;
	public float oscSpeed = 5f;
	private Vector3 oscDirection;
	private Vector3 initPos;
	
	//determines initial osc direction
	private float vcenter = 0;
	
	void Start(){
		initPos = rigidbody.position;
		
		//if near top, oscillate downwards; vice versa
		oscDirection = (rigidbody.position.y > vcenter) ? Vector3.down : Vector3.up;
	}
	
    void Update()
    {
        rigidbody.MovePosition(rigidbody.position + scrollDirection * speed * Time.deltaTime);
		
		if (oscillateRadius > 0){
			rigidbody.MovePosition(rigidbody.position + oscDirection * oscSpeed * Time.deltaTime);
			if (rigidbody.position.y - initPos.y >= oscillateRadius) oscDirection = Vector3.down;
			else if (rigidbody.position.y - initPos.y <= -oscillateRadius) oscDirection = Vector3.up;
		}
    }
}
