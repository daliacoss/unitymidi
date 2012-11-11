using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ScrollBehaviour : MonoBehaviour
{
    public Vector3 scrollDirection = -Vector3.right;
    public float speed = 3f;

    void Update()
    {
        rigidbody.MovePosition(rigidbody.position + scrollDirection * speed * Time.deltaTime);
    }
}
