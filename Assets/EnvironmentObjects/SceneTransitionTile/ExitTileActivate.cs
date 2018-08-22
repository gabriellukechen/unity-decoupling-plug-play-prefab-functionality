﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTileActivate : MonoBehaviour {
    private GameObject loadingScreen;
    public string destinationSceneName;
    private SceneController sceneController;

    void Start()
    {
        loadingScreen = GameObject.Find("Loading Screen");
        GameObject sceneControllerGameObject = GameObject.FindGameObjectWithTag("SceneController");
        sceneController = sceneControllerGameObject.GetComponent<SceneController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col != null)
        {
            if (col.gameObject.tag == "Player")
            {
                sceneController.FadeAndLoadScene(destinationSceneName);
            }
        }        
    }    
}
