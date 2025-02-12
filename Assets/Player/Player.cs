using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField]
    private AttackRadius AttackRadius;
    
    [SerializeField]
    private int Health = 300;
    // Start is called before the first frame update
    private void Awake()
    {
        AttackRadius.OnAttack += OnAttack;
    }

    private void OnAttack(IDamageable Target)
    {
        Debug.Log("Attacking");
    }
    
    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
