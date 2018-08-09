﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingKickMovePlayer : MonoBehaviour {
    private Rigidbody2D characterRigidBody;
    private IMovementHandler characterMovementHandler;
    private ICharacterStateManager stateManager;
    private HeavyMeleeAbilityStats heavyMeleeAbilityStats;
    private CharacterState directionState;
    private float currentKickDistance;
    public float kickDistance;
    public float kickSpeed;

    void Awake()
    {
        characterRigidBody = GetComponentInParent<Rigidbody2D>();
    }

    void Start()
    {
        
        heavyMeleeAbilityStats = GetComponent<HeavyMeleeAbilityStats>();
        characterMovementHandler =
            transform.root.GetComponentInChildren(typeof(IMovementHandler)) as IMovementHandler;
        stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        directionState = stateManager.GetExistingCharacterState(ConstantStrings.DIRECTION);
        currentKickDistance = 0;
    }

    public void FlyingKickDistance(float distance)
    {
        while (currentKickDistance < kickDistance)
        {
            characterMovementHandler.AddToXPosition
                (((float[])directionState.GetStateValue())[0] * kickSpeed * Time.deltaTime);
            currentKickDistance += kickSpeed * Time.deltaTime;
        }
        currentKickDistance = 0;
    }
}
