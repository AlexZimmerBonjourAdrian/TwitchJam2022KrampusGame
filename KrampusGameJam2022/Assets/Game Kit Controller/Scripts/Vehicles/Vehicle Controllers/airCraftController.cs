﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class airCraftController : vehicleController
{
	[Header ("Custom Settings")]
	[Space]

	public otherVehicleParts vehicleParts;
	public airCraftSettings settings;
	public airCraftNonRealisticSettings nonRealisticSettings;
	public airCraftRealisticSettings realisticSettings;
	public float accelerationSpeed;
	public bool realisticMovements;


	public float maxEnginePower = 40;
	// The maximum output of the engine.
	public float liftAmount = 0.002f;
	// The amount of lift generated by the aeroplane moving forwards.
	public float stopLiftAtSpeed = 300;
	// The speed at which lift is no longer applied.
	public float pitchForce = 0.75f;
	// The strength of effect for pitch input.
	public float yawForce = 0.75f;
	// The strength of effect for yaw input.
	public float rollForce = 0.75f;
	// The strength of effect for roll input.
	public float bankedTurnForce = 0.5f;
	// The amount of turn from doing a banked turn.
	public float aeroDynamicForce = 0.02f;
	// How much aerodynamics affect the speed of the aeroplane.
	public float bankedTurnPitch = 0.5f;
	// How much the aeroplane automatically pitches when in a banked turn.
	public float bankedRollForce = 0.1f;
	// How much the aeroplane tries to level when not rolling.
	public float bankedPitchForce = 0.1f;
	// How much the aeroplane tries to level when not pitching.
	public float brakesForce = 20;
	// How much the air brakes effect the drag.
	public float throttleChangeSpeed = 0.3f;
	// The speed with which the throttle changes.
	public float dragIncreaseFactor = 0.001f;
	// how much drag should increase with speed.
	public float boostMultiplier;
	public float boostAccelerationSpeed;
	public float currentBoost;

	[Range (0f, 1f)] public float engineMasterVolume = 0.5f;
	[Range (0f, 1f)] public float windMasterVolume = 0.5f;

	[Space]
	[Header ("Debug")]
	[Space]

	public bool landing;
	public bool takeOff;
	public bool enginesOn;
	public bool canLand;

	[Space]
	[Header ("Components")]
	[Space]

	public shipInterfaceInfo interfaceInfo;

	List<exhaustsParticlesInfo> exhaustParticles = new List<exhaustsParticlesInfo> ();
	List<ParticleSystem> boostingParticles = new List<ParticleSystem> ();

	Vector3 airCraftVelocity;
	Vector2 rightJoystickAngle;
	Vector2 leftJoystickAngle;
	int collisionForceLimit = 5;
	int i;

	float currentVelocity;
	float rollInput;
	float pitchInput;
	float yawInput;

	float rollDirection;
	float originalDrag;
	float originalAngularDrag;

	RaycastHit hit;

	float Throttle;
	float ForwardSpeed;
	float EnginePower;
	float RollAngle;
	float PitchAngle;
	float ThrottleInput;
	float m_AeroFactor;
	float m_BankedTurnAmount;


	float m_EngineMinThrottlePitch = 0.4f;
	float m_EngineMaxThrottlePitch = 2;
	float m_EngineFwdSpeedMultiplier = 0.002f;
	float m_WindBasePitch = 0.2f;
	float m_WindSpeedPitchFactor = 0.004f;
	float m_WindMaxSpeedVolume = 100;

	public override void Start ()
	{
		base.Start ();

		//get the boost particles inside the vehicle
		Component[] components = vehicleParts.exhausts.GetComponentsInChildren (typeof(ParticleSystem));
		foreach (ParticleSystem child in components) {
			exhaustsParticlesInfo newExhaust = new exhaustsParticlesInfo ();
			newExhaust.particles = child;

			var exhaustParticlesMain = newExhaust.particles.main;
			newExhaust.startSpeed = exhaustParticlesMain.startSpeedMultiplier;

			exhaustParticlesMain.startSpeed = 0;

			newExhaust.particles.gameObject.SetActive (false);
			exhaustParticles.Add (newExhaust);
		}

		setAudioState (vehicleParts.engineAudio, 5, 0, vehicleParts.engineClip, true, false, false);
		setAudioState (vehicleParts.windAudio, 5, 0, vehicleParts.windClip, true, false, false);
		setAudioState (vehicleParts.engineStartAudio, 5, 0.7f, vehicleParts.engineStartClip, false, false, false);

		playAnimation (true, vehicleParts.wings, settings.wingsAnimation);

		for (int i = 0; i < vehicleParts.trails.Count; i++) {
			if (vehicleParts.trails [i] != null) {
				vehicleParts.trails [i].time = 0;
			}
		}

		if (realisticMovements) {
			mainRigidbody.drag = realisticSettings.initialDrag;
			mainRigidbody.angularDrag = realisticSettings.initialAngularDrag;
		} else {
			mainRigidbody.drag = nonRealisticSettings.initialDrag;
			mainRigidbody.angularDrag = nonRealisticSettings.initialAngularDrag;
		}

		originalDrag = mainRigidbody.drag;
		originalAngularDrag = mainRigidbody.angularDrag;

		if (settings.useLandMark) {
			if (vehicleParts.landMark != null) {
				vehicleParts.landMark.SetActive (false);
			}
		}
	}

	public override void vehicleUpdate ()
	{
		//if the player is driving this vehicle, then
		if (driving) {
			if (isTurnedOn) {
				//get the current values from the input manager, keyboard and touch controls
				axisValues = mainInputActionManager.getPlayerMovementAxis ();
				horizontalAxis = axisValues.x;
				verticalAxis = axisValues.y;

				moving = enginesOn && currentSpeed > 0;
			}

			if (mainVehicleCameraController.currentState.useCameraSteer && horizontalAxis == 0 && verticalAxis == 0) {
				localLook = transform.InverseTransformDirection (mainVehicleCameraController.getLookDirection ());

				if (localLook.z < 0f) {
					localLook.x = Mathf.Sign (localLook.x);
				}

				steering = localLook.x;
				steering = Mathf.Clamp (steering, -1f, 1f);

				horizontalAxis = steering;

				localLook = settings.vehicleCamera.transform.InverseTransformDirection (mainVehicleCameraController.currentState.pivotTransform.forward);

				if (localLook.z < 0f) {
					localLook.y = Mathf.Sign (localLook.y);
				}

				steering = localLook.y;
				steering = Mathf.Clamp (steering, -1f, 1f);

				steering = -steering;

				verticalAxis = steering;
			}

			//if the boost input is enabled, check if there is energy enough to use it
			if (usingBoost) {
				//if there is enough energy, enable the boost
				if (mainVehicleHUDManager.useBoost (moving)) {
					boostInput = vehicleControllerSettings.maxBoostMultiplier;

					usingBoosting ();
				} 
				//else, disable the boost
				else {
					usingBoost = false;
					//if the vehicle is not using the gravity control system, disable the camera move away action
					if (!mainVehicleGravityControl.isGravityPowerActive ()) {
						mainVehicleCameraController.usingBoost (false, vehicleControllerSettings.boostCameraShakeStateName, 
							vehicleControllerSettings.useBoostCameraShake, vehicleControllerSettings.moveCameraAwayOnBoost);
					}

					usingBoosting ();

					boostInput = 1;
				}
			}
				
			//set the current speed in the HUD of the vehicle
			mainVehicleHUDManager.getSpeed (currentSpeed, vehicleControllerSettings.maxForwardSpeed);

			//if the vehicle has fuel, allow to move it
			if (moving) {
				if (!mainVehicleHUDManager.useFuel ()) {
					if (isTurnedOn) {
						turnOnOrOff (false, isTurnedOn);
					}
				}
			}

			rightJoystickAngle.x += horizontalAxis * settings.joysticksSensitivity;

			if (Mathf.Abs (horizontalAxis) <= 0.1f) {
				rightJoystickAngle.x = Mathf.Lerp (rightJoystickAngle.x, 0, Time.deltaTime * settings.joysticksSensitivity);
			}

			rightJoystickAngle.y += verticalAxis * settings.joysticksSensitivity;

			if (Mathf.Abs (verticalAxis) <= 0.1f) {
				rightJoystickAngle.y = Mathf.Lerp (rightJoystickAngle.y, 0, Time.deltaTime * settings.joysticksSensitivity);
			}

			rightJoystickAngle.y = Mathf.Clamp (rightJoystickAngle.y, -settings.joystickClamps.x, settings.joystickClamps.y);
			rightJoystickAngle.x = Mathf.Clamp (rightJoystickAngle.x, -settings.joystickClamps.x, settings.joystickClamps.y);
			vehicleParts.rightJoystick.transform.localRotation = Quaternion.Euler (rightJoystickAngle.y, 0, -rightJoystickAngle.x);

			leftJoystickAngle.x -= rollInput * settings.joysticksSensitivity;

			if (Mathf.Abs (rollInput) <= 0.1f) {
				leftJoystickAngle.x = Mathf.Lerp (leftJoystickAngle.x, 0, Time.deltaTime * settings.joysticksSensitivity);
			}

			if (usingBoost) {
				leftJoystickAngle.y += 0.5f * settings.joysticksSensitivity;
			} else if (braking) {
				leftJoystickAngle.y -= 0.5f * settings.joysticksSensitivity;
			} else {
				leftJoystickAngle.y = Mathf.Lerp (leftJoystickAngle.y, 0, Time.deltaTime * settings.joysticksSensitivity);
			}

			leftJoystickAngle.y = Mathf.Clamp (leftJoystickAngle.y, -settings.joystickClamps.x, settings.joystickClamps.y);
			leftJoystickAngle.x = Mathf.Clamp (leftJoystickAngle.x, -settings.joystickClamps.x, settings.joystickClamps.y);
			vehicleParts.leftJoystick.transform.localRotation = Quaternion.Euler (leftJoystickAngle.y, 0, -leftJoystickAngle.x);

			if (enginesOn) {
				// Find what proportion of the engine's power is being used.
				float enginePowerProportion = Mathf.InverseLerp (0, maxEnginePower, EnginePower);

				// Set the engine's pitch to be proportional to the engine's current power.
				vehicleParts.engineAudio.pitch = Mathf.Lerp (m_EngineMinThrottlePitch, m_EngineMaxThrottlePitch, enginePowerProportion);

				// Increase the engine's pitch by an amount proportional to the aeroplane's forward speed.
				// (this makes the pitch increase when going into a dive!)
				vehicleParts.engineAudio.pitch += ForwardSpeed * m_EngineFwdSpeedMultiplier;

				// Set the engine's volume to be proportional to the engine's current power.
				vehicleParts.engineAudio.volume = Mathf.InverseLerp (0, maxEnginePower * engineMasterVolume, EnginePower);

				// Set the wind's pitch and volume to be proportional to the aeroplane's forward speed.
				vehicleParts.windAudio.pitch = m_WindBasePitch + currentSpeed * m_WindSpeedPitchFactor;
				vehicleParts.windAudio.volume = Mathf.InverseLerp (0, m_WindMaxSpeedVolume, currentSpeed) * windMasterVolume;
			} else {
				vehicleParts.engineAudio.pitch = Mathf.Lerp (vehicleParts.engineAudio.pitch, 0, Time.deltaTime * 2);
				vehicleParts.windAudio.pitch = Mathf.Lerp (vehicleParts.windAudio.pitch, 0, Time.deltaTime * 2);
			}
		}
	}

	void FixedUpdate ()
	{
		currentSpeed = mainRigidbody.velocity.magnitude;

		if (driving) {
			if (Physics.Raycast (vehicleParts.landMarkRaycasPosition.position, -transform.up, out hit, settings.distanceToLand, settings.layer)) {
				canLand = true;

				if (settings.useLandMark) {
					if (!vehicleParts.landMark.activeSelf) {
						vehicleParts.landMark.SetActive (true);
					}

					vehicleParts.landMark.transform.position = hit.point + hit.normal * 0.2f;
					Vector3 myForward = Vector3.Cross (vehicleParts.landMark.transform.right, hit.normal);
					Quaternion dstRot = Quaternion.LookRotation (myForward, hit.normal);

					vehicleParts.landMark.transform.rotation = dstRot;
					vehicleParts.landMark.transform.GetChild (0).Rotate (0, 100 * Time.deltaTime, 0);
				}
			} else {
				canLand = false;

				if (settings.useLandMark) {
					if (vehicleParts.landMark.activeSelf) {
						vehicleParts.landMark.SetActive (false);
					}
				}
			}

			if (interfaceInfo != null) {
				interfaceInfo.shipCanLand (canLand);
				interfaceInfo.shipEnginesState (enginesOn);
			}

			if (enginesOn) {
				yawInput = horizontalAxis;
				pitchInput = verticalAxis;

				if (realisticMovements) {
					if (settings.canRoll) {
						rollInput = Mathf.Lerp (rollInput, rollDirection, Time.deltaTime * rollForce);
					}

					if (vehicleControllerSettings.canUseBoost) {
						if (usingBoost) {
							currentBoost = Mathf.Lerp (currentBoost, boostMultiplier, boostAccelerationSpeed * Time.deltaTime);
						} else {
							currentBoost = Mathf.Lerp (currentBoost, 0, boostAccelerationSpeed * Time.deltaTime);
						}
					}

					ThrottleInput = braking ? -1 : 1;

					// Calculate the flat forward direction (with no y component).
					Vector3 flatForward = transform.forward;
					flatForward.y = 0;

					// If the flat forward vector is non-zero (which would only happen if the plane was pointing exactly straight upwards)
					if (flatForward.sqrMagnitude > 0) {
						flatForward.Normalize ();

						// calculate current pitch angle
						Vector3 localFlatForward = transform.InverseTransformDirection (flatForward);
						PitchAngle = Mathf.Atan2 (localFlatForward.y, localFlatForward.z);

						// calculate current roll angle
						Vector3 flatRight = Vector3.Cross (Vector3.up, flatForward);
						Vector3 localFlatRight = transform.InverseTransformDirection (flatRight);
						RollAngle = Mathf.Atan2 (localFlatRight.y, localFlatRight.x);
					}

					m_BankedTurnAmount = Mathf.Sin (RollAngle);

					if (rollInput == 0f) {
						rollInput = -RollAngle * bankedRollForce;
					}

					if (pitchInput == 0f) {
						pitchInput = -PitchAngle * bankedPitchForce;
						pitchInput -= Mathf.Abs (m_BankedTurnAmount * m_BankedTurnAmount * bankedTurnPitch);
					}

					// Forward speed is the speed in the planes's forward direction (not the same as its velocity, eg if falling in a stall)
					Vector3 localVelocity = transform.InverseTransformDirection (mainRigidbody.velocity);
					ForwardSpeed = Mathf.Max (0, localVelocity.z);

					// Adjust throttle based on throttle input (or immobilized state)
					Throttle = Mathf.Clamp01 (Throttle + ThrottleInput * Time.deltaTime * throttleChangeSpeed);

					// current engine power is just:
					EnginePower = Throttle * maxEnginePower + currentBoost;

					// increase the drag based on speed, since a constant drag doesn't seem "Real" (tm) enough
					float extraDrag = mainRigidbody.velocity.magnitude * dragIncreaseFactor;

					// Air brakes work by directly modifying drag. This part is actually pretty realistic!
					mainRigidbody.drag = (braking ? (originalDrag + extraDrag) * brakesForce : originalDrag + extraDrag);

					// Forward speed affects angular drag - at high forward speed, it's much harder for the plane to spin
					mainRigidbody.angularDrag = originalAngularDrag + ForwardSpeed / (mainRigidbody.mass / 10);

					// "Aerodynamic" calculations. This is a very simple approximation of the effect that a plane
					// will naturally try to align itself in the direction that it's facing when moving at speed.
					// Without this, the plane would behave a bit like the asteroids spaceship!
					if (mainRigidbody.velocity.magnitude > 0) {
						// compare the direction we're pointing with the direction we're moving:
						m_AeroFactor = Vector3.Dot (transform.forward, mainRigidbody.velocity.normalized);

						// multipled by itself results in a desirable rolloff curve of the effect
						m_AeroFactor *= m_AeroFactor;

						// Finally we calculate a new velocity by bending the current velocity direction towards
						// the the direction the plane is facing, by an amount based on this aeroFactor
						Vector3 newVelocity = Vector3.Lerp (mainRigidbody.velocity, transform.forward * ForwardSpeed, m_AeroFactor * ForwardSpeed * aeroDynamicForce * Time.deltaTime);
						mainRigidbody.velocity = newVelocity;

						// also rotate the plane towards the direction of movement - this should be a very small effect, but means the plane ends up
						// pointing downwards in a stall
						mainRigidbody.rotation = Quaternion.Slerp (mainRigidbody.rotation, Quaternion.LookRotation (mainRigidbody.velocity, transform.up), aeroDynamicForce * Time.deltaTime);
					}

					// Now calculate forces acting on the aeroplane:
					// we accumulate forces into this variable:
					Vector3 forces = Vector3.zero;

					// Add the engine power in the forward direction
					forces += EnginePower * transform.forward;

					// The direction that the lift force is applied is at right angles to the plane's velocity (usually, this is 'up'!)
					Vector3 liftDirection = Vector3.Cross (mainRigidbody.velocity, transform.right).normalized;
					// The amount of lift drops off as the plane increases speed - in reality this occurs as the pilot retracts the flaps
					// shortly after takeoff, giving the plane less drag, but less lift. Because we don't simulate flaps, this is
					// a simple way of doing it automatically:

					float zeroLiftFactor = Mathf.InverseLerp (stopLiftAtSpeed, 0, ForwardSpeed);

					// Calculate and add the lift power
					float liftPower = ForwardSpeed * ForwardSpeed * liftAmount * zeroLiftFactor * m_AeroFactor;
					forces += liftPower * liftDirection;

					// Apply the calculated forces to the the Rigidbody
					mainRigidbody.AddForce (forces * mainRigidbody.mass);

					// We accumulate torque forces into this variable:
					Vector3 torque = Vector3.zero;

					// Add torque for the pitch based on the pitch input.
					torque += pitchInput * pitchForce * transform.right;

					// Add torque for the yaw based on the yaw input.
					torque += yawInput * yawForce * transform.up;

					// Add torque for the roll based on the roll input.
					torque += rollInput * rollForce * transform.forward;

					// Add torque for banked turning.
					torque += m_BankedTurnAmount * bankedTurnForce * transform.up;

					// The total torque is multiplied by the forward speed, so the controls have more effect at high speed,
					// and little effect at low speed, or when not moving in the direction of the nose of the plane
					mainRigidbody.AddTorque (torque * (ForwardSpeed * m_AeroFactor * mainRigidbody.mass));
				} else {
					if (settings.canRoll) {
						rollInput = Mathf.Lerp (rollInput, rollDirection, Time.deltaTime * nonRealisticSettings.rollForce);
					}

					currentVelocity = mainRigidbody.velocity.magnitude;

					if (vehicleControllerSettings.canUseBoost) {
						if (usingBoost) {
							currentVelocity = Mathf.Lerp (currentVelocity, nonRealisticSettings.maxBoostSpeed, nonRealisticSettings.boostAccelerationSpeed * Time.deltaTime);
						} else if (braking) {
							currentVelocity = Mathf.Lerp (currentVelocity, nonRealisticSettings.brakeSpeed, nonRealisticSettings.boostAccelerationSpeed * Time.deltaTime);
						} else {
							currentVelocity = Mathf.Lerp (currentVelocity, nonRealisticSettings.maxForwardSpeed, nonRealisticSettings.boostAccelerationSpeed * Time.deltaTime);
						}
					}

					Vector3 currentTorque = new Vector3 (pitchInput * nonRealisticSettings.pitchForce, yawInput * nonRealisticSettings.yawForce, rollInput * nonRealisticSettings.rollForce);

					currentTorque *= Time.deltaTime * mainRigidbody.mass;

					mainRigidbody.AddRelativeTorque (currentTorque * mainRigidbody.mass);

					airCraftVelocity = Vector3.MoveTowards (airCraftVelocity, transform.forward * currentVelocity, Time.deltaTime * accelerationSpeed);

					mainRigidbody.velocity = airCraftVelocity;

					EnginePower = Mathf.Lerp (EnginePower, mainRigidbody.velocity.magnitude / 10, Time.deltaTime * 5);
				}
			} 
		}

		if (mainRigidbody.velocity.magnitude != 0) {
			for (int i = 0; i < exhaustParticles.Count; i++) {
				float paritclesSpeed = 0;

				if (usingBoost) {
					paritclesSpeed = (currentSpeed / nonRealisticSettings.maxForwardSpeed) * exhaustParticles [i].startSpeed + (boostInput / 2);
					paritclesSpeed = Mathf.Clamp (paritclesSpeed, 0, exhaustParticles [i].startSpeed + (boostInput / 2));
				} else {
					paritclesSpeed = (currentSpeed / nonRealisticSettings.maxForwardSpeed) * exhaustParticles [i].startSpeed;
					paritclesSpeed = Mathf.Clamp (paritclesSpeed, 0, exhaustParticles [i].startSpeed);
				}
				exhaustParticles [i].currentSpeed = Mathf.Lerp (exhaustParticles [i].currentSpeed, paritclesSpeed, Time.deltaTime * 5);

				var exhaustParticlesMain = exhaustParticles [i].particles.main;

				exhaustParticlesMain.startSpeed = exhaustParticles [i].currentSpeed;
			}
		}
	}

	public void resetInputValues ()
	{
		pitchInput = 0;
		yawInput = 0;
		rollInput = 0;
		airCraftVelocity = Vector3.zero;
		currentVelocity = 0;
	}

	public IEnumerator enableOrDisableTrailsState (bool state)
	{
		float targetValue = 0;

		if (state) {
			targetValue = settings.trailsTime;
		} 

		for (float t = 0; t < 1;) {
			t += Time.deltaTime;

			for (int i = 0; i < vehicleParts.trails.Count; i++) {
				if (vehicleParts.trails [i] != null) {
					vehicleParts.trails [i].time = Mathf.Lerp (vehicleParts.trails [i].time, targetValue, t);
				}
			}

			yield return null;
		}
	}

	void landingOrTakeOffAirCraft ()
	{
		if (!enginesOn) {
			if (mainVehicleGravityControl.OnGround) {
				
				mainVehicleGravityControl.pauseDownForce (true);

				takeOff = true;
				landing = false;

				setTurnOnStateSound ();

				StartCoroutine (placeOnGroundOrOnAir (true));
			} else {
				mainVehicleGravityControl.pauseDownForce (true);

				if (!takeOff) {
					playAnimation (true, vehicleParts.landingGear, settings.landingGearAnimation);
					playAnimation (false, vehicleParts.wings, settings.wingsAnimation);
				}

				for (int i = 0; i < exhaustParticles.Count; i++) {
					if (exhaustParticles [i] != null) {
						if (!exhaustParticles [i].particles.isPlaying) {
							exhaustParticles [i].particles.gameObject.SetActive (true);
							exhaustParticles [i].particles.Play ();

							var particlesMain = exhaustParticles [i].particles.main;
							particlesMain.loop = true;
						}
					}
				}

				enginesOn = true;

				if (realisticMovements) {
					mainVehicleGravityControl.pauseDownForce (false);
				}

				StartCoroutine (enableOrDisableTrailsState (true));
			}
		} else {
			resetInputValues ();

			if (canLand) {
				takeOff = false;
				landing = true;

				StartCoroutine (stopShip ());

				setTurnOffStateSound (isTurnedOn);

				StartCoroutine (placeOnGroundOrOnAir (false));
			} else {
				enginesOn = false;

				StartCoroutine (enableOrDisableTrailsState (false));
			}
		}
	}

	IEnumerator placeOnGroundOrOnAir (bool state)
	{
		if (state) {
			mainRigidbody.AddForce (transform.up * (settings.extraTakeOffForce * mainRigidbody.mass), ForceMode.Impulse);

			playAnimation (true, vehicleParts.landingGear, settings.landingGearAnimation);
			playAnimation (false, vehicleParts.wings, settings.wingsAnimation);

			yield return new WaitForSeconds (settings.waitTimeToTakeOff);

			for (int i = 0; i < exhaustParticles.Count; i++) {
				if (!exhaustParticles [i].particles.isPlaying) {
					exhaustParticles [i].particles.gameObject.SetActive (true);
					exhaustParticles [i].particles.Play ();

					var particlesMain = exhaustParticles [i].particles.main;
					particlesMain.loop = true;
				}
			}

			enginesOn = true;

			if (realisticMovements) {
				mainVehicleGravityControl.pauseDownForce (false);
			}

			StartCoroutine (enableOrDisableTrailsState (true));
		} else {
			if (canLand) {
				for (int i = 0; i < exhaustParticles.Count; i++) {
					var particlesMain = exhaustParticles [i].particles.main;
					particlesMain.loop = false;
				}

				enginesOn = false;

				playAnimation (false, vehicleParts.landingGear, settings.landingGearAnimation);

				playAnimation (true, vehicleParts.wings, settings.wingsAnimation);

				if (settings.rotateToSurfaceInLand) {
					mainVehicleGravityControl.rotateVehicleToLandSurface (hit.normal);
				}

				StartCoroutine (enableOrDisableTrailsState (false));
			}
		}

		yield return null;
	}

	IEnumerator stopShip ()
	{
		while (mainRigidbody.velocity.magnitude > 0.1f) {
			mainRigidbody.velocity = Vector3.Lerp (mainRigidbody.velocity, Vector3.zero, Time.deltaTime * 2);
		}

		yield return null;
	}

	public void playAnimation (bool forward, GameObject obj, string name)
	{
		if (settings.useAnimations) {
			Animation currentAnimation = obj.GetComponent<Animation> ();

			if (forward) {
				currentAnimation [name].speed = 1; 
				currentAnimation.Play (name);
			} else {
				currentAnimation [name].speed = -1; 
				currentAnimation [name].time = currentAnimation [name].length;
				currentAnimation.Play (name);
			}
		}
	}

	//the player is getting on or off from the vehicle, so
	public override void changeVehicleState ()
	{
		base.changeVehicleState ();

		if (interfaceInfo != null) {
			interfaceInfo.enableOrDisableInterface (driving);
		}
	}

	public void setTurnOnStateSound ()
	{
		setAudioState (vehicleParts.engineAudio, 5, 0, vehicleParts.engineClip, true, true, false);
		setAudioState (vehicleParts.windAudio, 5, 0, vehicleParts.windClip, true, true, false);
		setAudioState (vehicleParts.engineStartAudio, 5, 0.7f, vehicleParts.engineStartClip, false, true, false);
	}

	public void setTurnOffStateSound (bool previouslyTurnedOn)
	{
		if (previouslyTurnedOn) {
			setAudioState (vehicleParts.engineAudio, 5, 0, vehicleParts.engineClip, false, false, true);
			setAudioState (vehicleParts.windAudio, 5, 0, vehicleParts.windClip, false, false, true);
			setAudioState (vehicleParts.engineAudio, 5, 1, vehicleParts.engineEndClip, false, true, false);
		}
	}

	public override void setTurnOffState (bool previouslyTurnedOn)
	{
		base.setTurnOffState (previouslyTurnedOn);

		setTurnOffStateSound (previouslyTurnedOn);

		resetInputValues ();

		if (enginesOn) {
			for (int i = 0; i < exhaustParticles.Count; i++) {
				var particlesMain = exhaustParticles [i].particles.main;
				particlesMain.loop = false;
			}

			enginesOn = false;

			playAnimation (false, vehicleParts.landingGear, settings.landingGearAnimation);

			playAnimation (true, vehicleParts.wings, settings.wingsAnimation);

			mainVehicleGravityControl.pauseDownForce (false);
		}

		StartCoroutine (enableOrDisableTrailsState (false));
	}

	public override void setEngineOnOrOffState ()
	{
		base.setEngineOnOrOffState ();


	}

	public override void turnOnOrOff (bool state, bool previouslyTurnedOn)
	{
		isTurnedOn = state;

		if (isTurnedOn) {
			
		} else {
			setTurnOffState (previouslyTurnedOn);

			takeOff = false;
		}
	}

	//the vehicle has been destroyed, so disabled every component in it
	public override void disableVehicle ()
	{
		//stop the audio sources
		setAudioState (vehicleParts.engineAudio, 5, 0, vehicleParts.engineClip, false, false, false);
		setAudioState (vehicleParts.windAudio, 5, 0, vehicleParts.windClip, false, false, false);
		setAudioState (vehicleParts.engineStartAudio, 5, 0.7f, vehicleParts.engineStartClip, false, false, false);

		if (interfaceInfo != null) {
			interfaceInfo.enableOrDisableInterface (false);
		}

		//stop the boost
		if (usingBoost) {
			usingBoost = false;

			mainVehicleCameraController.usingBoost (false, vehicleControllerSettings.boostCameraShakeStateName, 
				vehicleControllerSettings.useBoostCameraShake, vehicleControllerSettings.moveCameraAwayOnBoost);

			usingBoosting ();

			resetInputValues ();
		}

		//disable the exhausts particles
		for (i = 0; i < exhaustParticles.Count; i++) {
			exhaustParticles [i].particles.Stop ();
		}

		StartCoroutine (enableOrDisableTrailsState (false));

		//disable the controller
		this.enabled = false;
	}

	//if any collider in the vehicle collides, then
	public override void setCollisionDetected (Collision currentCollision)
	{
		//check that the collision is not with the player
		if (!currentCollision.gameObject.CompareTag ("Player")) {
			//if the velocity of the collision is higher that the limit
			if (currentCollision.relativeVelocity.magnitude > collisionForceLimit) {
				//set the collision audio with a random collision clip
				if (vehicleParts.crashClips.Length > 0) {
					setAudioState (vehicleParts.crashAudio, 5, 1, vehicleParts.crashClips [UnityEngine.Random.Range (0, vehicleParts.crashClips.Length)], false, true, false);
				}
			}
		}
	}

	//if the vehicle is using the boost, set the boost particles
	public override void usingBoosting ()
	{
		base.usingBoosting ();

		for (int i = 0; i < boostingParticles.Count; i++) {
			if (usingBoost) {
				if (!boostingParticles [i].isPlaying) {
					boostingParticles [i].gameObject.SetActive (true);
					boostingParticles [i].Play ();

					var boostingParticlesMain = boostingParticles [i].main;
					boostingParticlesMain.loop = true;
				}
			} else {
				if (boostingParticles [i].isPlaying) {
					var boostingParticlesMain = boostingParticles [i].main;
					boostingParticlesMain.loop = false;
				}
			}
		}
	}

	//CALL INPUT FUNCTIONS
	public override void inputHoldOrReleaseTurbo (bool holdingButton)
	{
		if (driving && isTurnedOn) {
			//boost input
			if (holdingButton) {
				usingBoost = true;

				//set the camera move away action

				mainVehicleCameraController.usingBoost (true, vehicleControllerSettings.boostCameraShakeStateName, 
					vehicleControllerSettings.useBoostCameraShake, vehicleControllerSettings.moveCameraAwayOnBoost);
			} else {
				//stop boost
				usingBoost = false;

				//disable the camera move away action
				mainVehicleCameraController.usingBoost (false, vehicleControllerSettings.boostCameraShakeStateName, 
					vehicleControllerSettings.useBoostCameraShake, vehicleControllerSettings.moveCameraAwayOnBoost);

				//disable the boost particles

				usingBoosting ();

				boostInput = 1;
			}
		}
	}

	public override void inputHoldOrReleaseBrake (bool holdingButton)
	{
		if (driving && isTurnedOn) {
			braking = holdingButton;
		}
	}

	public void inputSetRotateToLeftState (bool state)
	{
		if (driving && isTurnedOn) {
			if (state) {
				rollDirection = 1;
			} else {
				rollDirection = 0;
			}
		}
	}

	public void inputSetRotateToRightState (bool state)
	{
		if (driving && isTurnedOn) {
			if (state) {
				rollDirection = -1;
			} else {
				rollDirection = 0;
			}
		}
	}

	public void inputLandOrTakeOff ()
	{
		if (driving && isTurnedOn) {
			landingOrTakeOffAirCraft ();
		}
	}

	public override void inputSetTurnOnState ()
	{
		if (driving && !usingGravityControl) {
			if (mainVehicleHUDManager.canSetTurnOnState) {
				setEngineOnOrOffState ();
			}
		}
	}

	[System.Serializable]
	public class otherVehicleParts
	{
		public GameObject wings;
		public GameObject landingGear;
		public Transform rightJoystick;
		public Transform leftJoystick;
		public Transform COM;
		public GameObject chassis;
		public AudioClip engineStartClip;
		public AudioClip engineClip;
		public AudioClip windClip;
		public AudioClip engineEndClip;
		public AudioClip[] crashClips;
		public AudioSource engineStartAudio;
		public AudioSource engineAudio;
		public AudioSource windAudio;
		public AudioSource crashAudio;
		public GameObject exhausts;
		public List<TrailRenderer> trails = new List<TrailRenderer> ();
		public GameObject landMark;
		public Transform landMarkRaycasPosition;
	}

	[System.Serializable]
	public class airCraftSettings
	{
		public LayerMask layer;

		public bool useAnimations = true;
		public string wingsAnimation;
		public string landingGearAnimation;

		public GameObject vehicleCamera;
		public bool canRoll;
		public float extraTakeOffForce = 10;
		public float waitTimeToTakeOff = 2;
		public Vector2 joystickClamps = new Vector2 (30, 30);
		public float joysticksSensitivity = 5;
		public float trailsTime = 5;
		public float distanceToLand;
		public bool useLandMark;
		public bool rotateToSurfaceInLand;
	}

	[System.Serializable]
	public class airCraftNonRealisticSettings
	{
		public float maxForwardSpeed = 50;
		public float maxBoostSpeed = 200;
		public float brakeSpeed;
		public float boostAccelerationSpeed = 20;
		public float pitchForce = 4;
		public float yawForce = 4;
		public float rollForce = 2;
		public float initialDrag;
		public float initialAngularDrag;
	}

	[System.Serializable]
	public class airCraftRealisticSettings
	{
		public float initialDrag;
		public float initialAngularDrag;
	}

	[System.Serializable]
	public class exhaustsParticlesInfo
	{
		public ParticleSystem particles;
		public float startSpeed;
		public float currentSpeed;
	}
}