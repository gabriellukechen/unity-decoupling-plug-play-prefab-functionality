﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerVelocityState : AbstractCharacterVelocityState {

	public float jumpMaxTime = 1;
	public float currentJumpTime;

	private void Awake()
	{
		currentJumpTime = jumpMaxTime;
		base.Awake();
		CharacterState.CharacterStateSubscription groundedSubscription = statesManager.GetCharacterStateSubscription(ConstantStrings.GROUNDED);
		//groundedSubscription.OnStateChanged += AllowJumpOnGrounded;
		groundedSubscription.OnStateChanged += StopJumpOnHeadHit;
	}

	// Update is called once per frame
	void Update () {
		float horizontalAxisValue = CrossPlatformInputManager.GetAxisRaw(
			ConstantStrings.UI.Input.INPUT_HORIZONTAL);
        
		float verticalValues = 0;
		bool isGrounded = ((bool[])statesManager.GetCharacterStateValue(ConstantStrings.GROUNDED))[1];

		if (isGrounded){
			if (!CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_JUMP)){
				currentJumpTime = 0;
			}
			verticalValues = 0;
		} 
		if (CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_JUMP) && currentJumpTime < jumpMaxTime)
        {
            verticalValues = CrossPlatformInputManager.GetAxisRaw(ConstantStrings.UI.Input.INPUT_JUMP);
            currentJumpTime += Time.deltaTime;
        }

		if (!CrossPlatformInputManager.GetButton(ConstantStrings.UI.Input.INPUT_JUMP)){
			if (!isGrounded)
            {
				currentJumpTime = jumpMaxTime;
                verticalValues = -1;
            }
		}      

		if(currentJumpTime >= jumpMaxTime && !isGrounded){
            verticalValues = -1;
        }  


		directionState.SetState(new float[] { horizontalAxisValue, verticalValues });

	}
    
    

	private void StopJumpOnHeadHit(object groundedState){
		bool isHitGroundOnHead = ((bool[])groundedState)[0];
		if (isHitGroundOnHead)
        {
			currentJumpTime = jumpMaxTime;
        }
	}
    
}
