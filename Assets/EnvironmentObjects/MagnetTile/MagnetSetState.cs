﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSetState : MonoBehaviour {
    private ICharacterStateManager stateManager;
    private CharacterState magnetState;
    private InventoryController inventoryController;

    // Use this for initialization
    void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        stateManager = go.GetComponent<ICharacterStateManager>();
        if (stateManager.ExistsState(ConstantStrings.MAGNET_STATE))
        {
            magnetState = stateManager.GetExistingCharacterState(ConstantStrings.MAGNET_STATE);
        }
        else
        {
            magnetState = new CharacterState(ConstantStrings.MAGNET_STATE, false);
            stateManager.RegisterCharacterState(magnetState.name, magnetState);
        }
        GameObject globalGameObject = GameObject.FindGameObjectWithTag("GlobalGameObject");
        inventoryController = globalGameObject.GetComponentInChildren<InventoryController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //If player does not have an aluminum shield, set to true
            if(inventoryController.FindItemByName("AluminumShield") == -1)
            {
                magnetState.SetState(true);
            }            
        }        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            magnetState.SetState(false);
        }
    }
}