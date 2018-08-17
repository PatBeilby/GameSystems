using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float hitDelay = 1f;
    public Color hitColor = Color.red;

    public Transform target;
    public NavMeshAgent agent;
    public Renderer rend;

    private Color originalColor;

    void Start()
    {
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    IEnumerator Hit()
    {
        rend.material.color = hitColor;
        yield return new WaitForSeconds(hitDelay);
        rend.material.color = originalColor;
    }

    public void DealDamage(int damageDealt)
    {
        StartCoroutine(Hit());

        health -= damageDealt;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
