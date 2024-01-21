using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objective : MonoBehaviour
{
    private int itemCount = 0;
    private int pickedupItems = 0;
    private GameObject[] items;
    public TMP_Text objective;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        items = GameObject.FindGameObjectsWithTag("Loot");
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        foreach (var item in items) 
        {
            itemCount++;
        }
        objective.text = "Loot all items: " + pickedupItems + "/" + itemCount;
    }

    public void pickedupItem()
    {
        pickedupItems++;
        objective.text = "Loot all items: " + pickedupItems + "/" + itemCount;
        if(pickedupItems == itemCount)
        {
            objective.text = "Escape";
            player.Escape();
        }
    }

    
}
