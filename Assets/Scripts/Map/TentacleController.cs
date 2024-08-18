using System;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    public StatusController stc;
    public float tentacleDamage = 100f;

    private void Start()
    {
        stc = FindObjectOfType<StatusController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stc.playerHp -= tentacleDamage;
        }
    }

    public void TenTacleMove()
    {
        Vector3 speed = Vector3.zero;
        Vector3 tentacleMove = new Vector3(transform.localPosition.x, transform.localPosition.y, -70f);
        transform.localPosition = tentacleMove;
    }
}
