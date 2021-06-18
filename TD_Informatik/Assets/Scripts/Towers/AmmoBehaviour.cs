using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehaviour : MonoBehaviour
{

    [SerializeField] private float damage;
    public GameObject Bloodeffect;

    private Transform target;
    public float speed;

    public void ConfirmEnemy (Transform _target)
    {
        target = _target;
    }
    
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - gameObject.transform.position;
        float currentdistancemoved = speed * Time.deltaTime;
        
        if (direction.magnitude <= currentdistancemoved)
        {
            GameObject Blood = (GameObject)Instantiate(Bloodeffect, gameObject.transform.position, transform.rotation);
            Destroy(Blood, 1.5f);
            
            EnemyBehavior2 enemyScript = target.GetComponent<EnemyBehavior2>();
            enemyScript.takeDamage(damage);

            Destroy(gameObject);
        }
        transform.Rotate(direction);

        transform.Translate(direction.normalized * currentdistancemoved, Space.World);

    }
}
