using UnityEngine;

public class ScrollBehaviour : MonoBehaviour
{
    public Vector3 scrollDirection;
    public float speed;

    void Update()
    {
        transform.position += scrollDirection * speed * Time.deltaTime;
    }
}
