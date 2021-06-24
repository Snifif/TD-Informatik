using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehaviour : MonoBehaviour
{

    [SerializeField] private float damage;
    public GameObject Bloodeffect;
    public GameObject GutsEffect;
    private Transform target;
    public float speed;
    private float damageMultiplier;

    public void ConfirmEnemy (Transform _target, float dmgMultiplier)
    {
        target = _target;
        damageMultiplier = dmgMultiplier;
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
            //if (damage >= 20 && target != null)
            //{
            //    GameObject Guts = (GameObject)Instantiate(GutsEffect, gameObject.transform.position, transform.rotation);
            //    Destroy(Guts, 2f);
            //}
            
            GameObject Blood = (GameObject)Instantiate(Bloodeffect, gameObject.transform.position, transform.rotation);
            Destroy(Blood, 1.5f);
                   
            EnemyBehavior2 enemyScript = target.GetComponent<EnemyBehavior2>();
            enemyScript.takeDamage(damage*damageMultiplier);

            Destroy(gameObject);
        }
        transform.Rotate(direction);

        transform.Translate(direction.normalized * currentdistancemoved, Space.World);

    }
}
