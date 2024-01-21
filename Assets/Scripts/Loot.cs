using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Objective objective;

    void Start()
    {
        objective = GameObject.FindObjectOfType(typeof(Objective)) as Objective;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objective.pickedupItem();
            gameObject.SetActive(false);
        }
    }
}
