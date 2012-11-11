using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageDealt = 5f;
    public float DealDamage()
    {
        Object.Destroy(gameObject);
        return damageDealt;
    }
}
