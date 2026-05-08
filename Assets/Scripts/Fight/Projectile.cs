using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

   // [SerializeField] private GameObject hitEffect;

    private Fighter target;

    private int damage;

    public void Init(Fighter targetFighter, int dmg)
    {
        target = targetFighter;

        damage = dmg;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);

            return;
        }

        // moure
        transform.position =
            Vector3.MoveTowards(transform.position,target.transform.position, speed * Time.deltaTime);

        // mirar target
        transform.LookAt(target.transform);

        // impacte
        float dist =Vector3.Distance(transform.position, target.transform.position);

        if (dist < 0.2f)
        {
            Hit();
        }
    }

    void Hit()
    {
        // efecte impacte
       /* if (hitEffect != null)
        {
            Instantiate(hitEffect,transform.position,Quaternion.identity);
        }*/

        // aplicar dmg
        target.TakeDamage(damage);

        Destroy(gameObject);
    }

    internal void Init(Transform transform)
    {
        throw new NotImplementedException();
    }
}
