﻿using System;
using UnityEngine;

namespace SimpleRacer {

	public class CarController : MonoBehaviour {

		public static Action onCornerRoadCompleted;
		public static Action onStraightRoadCompleted;
		public static Action onLevelUp;

		[Header("Initializations")]
		[SerializeField]
		private CarMotor _carMotor = null;

		[Header("Debug")]
		[SerializeField]
		private Road _activeRoad = null;

		private Vector3 _spawnPosition = Vector3.zero;
		private Quaternion _spawnRotation = Quaternion.identity;

		private void Awake() {
			GameManager.onGameStateChanged += OnGameStateChanged;
			InputManager.onPressing += OnPressing;
			InputManager.onReleased += OnReleased;

			_spawnPosition = this.transform.position;
			_spawnRotation = this.transform.rotation;
		}

		private void Update() {
			if (GameManager.instance.GameState == GameState.Gameplay) {
				KeepCarBalancing();
			}
		}

		private void OnGameStateChanged(GameState gameState) {
			switch (gameState) {
				case GameState.GameOver:
					LockCar();
					break;
				case GameState.Gameplay:
					UnlockCar();
					StartMovement();
					break;
			}
		}

		private void OnPressing() {
			if (GameManager.instance.GameState == GameState.Gameplay) {
				StartTurning();
			}
		}

		private void OnReleased() {
			if (GameManager.instance.GameState == GameState.Gameplay) {
				if (_carMotor.IsTurningActive) {
					StopTurning();
					StartDrifting();
					StartMovement();
				}
			}
		}

		private void LockCar() {
			_carMotor.Lock();
		}

		private void UnlockCar() {
			_carMotor.Unlock();
		}

		private void StartMovement() {
			_carMotor.StartMovement();
		}

		private void StopMovement() {
			_carMotor.StopMovement();
		}

		private void StartTurning() {
			if (_activeRoad == null || _carMotor.IsTurningActive) {
				return;
			}

			if (_activeRoad.HasBarrel()) {
				_carMotor.StartTurning(_activeRoad);
			}
		}

		private void StopTurning() {
			_carMotor.StopTurning();
		}

		private void StartDrifting() {
			_carMotor.StartDrifting();
		}

		private void StopDrifting() {
			_carMotor.StopDrifting();
		}

		private void LevelUp() {
			Debug.Log("Level up");

			if (_carMotor.IsTurningActive) {
				StopTurning();
			}

			_carMotor.BoostSpeed(_activeRoad.transform.position);

			onLevelUp?.Invoke();
		}

		private void KeepCarBalancing() {
			if (_activeRoad == null) {
				return;
			}

			switch (_activeRoad.GetRoadShape()) {
				case RoadShape.LEFT_STRAIGHT:
					_carMotor.KeepCarBalancing(RoadShape.LEFT_STRAIGHT.GetDirectionFromStraightRoadShape());
					break;
				case RoadShape.RIGHT_STRAIGHT:
					_carMotor.KeepCarBalancing(RoadShape.RIGHT_STRAIGHT.GetDirectionFromStraightRoadShape());
					break;
				case RoadShape.UP_STRAIGHT:
					_carMotor.KeepCarBalancing(RoadShape.UP_STRAIGHT.GetDirectionFromStraightRoadShape());
					break;
			}
		}

		private void OnCollisionEnter(Collision collision) {
			GameManager.instance.GameOver();
		}

		private void OnTriggerEnter(Collider other) {
			if (GameManager.instance.GameState != GameState.Gameplay) {
				return;
			}

			_activeRoad = other.transform.GetComponentInParent<Road>();

			if (_activeRoad.IsLevelUpRoad()) {
				LevelUp();
			}
		}

		private void OnTriggerExit(Collider other) {
			if (GameManager.instance.GameState != GameState.Gameplay) {
				return;
			}

			Road road = other.transform.GetComponentInParent<Road>();
			RoadShape roadShape = road.GetRoadShape();

			if (road.IsLevelUpRoad()) {
				_carMotor.NormalizeSpeed();
			}

			switch (roadShape) {
				case RoadShape.FROM_UP_TO_TURN_RIGHT_CORNER:
				case RoadShape.FROM_UP_TO_TURN_LEFT_CORNER:
				case RoadShape.FROM_RIGHT_TO_TURN_UP_CORNER:
				case RoadShape.FROM_LEFT_TO_TURN_UP_CORNER:
				case RoadShape.FROM_LEFT_TO_RIGHT_U_SHAPE:
				case RoadShape.FROM_RIGHT_TO_LEFT_U_SHAPE:
					GameManager.instance.AddScore();
					onCornerRoadCompleted?.Invoke();
					break;
				case RoadShape.LEFT_STRAIGHT:
				case RoadShape.RIGHT_STRAIGHT:
				case RoadShape.UP_STRAIGHT:
					onStraightRoadCompleted?.Invoke();
					break;
			}
		}

		private void OnDestroy() {
			GameManager.onGameStateChanged -= OnGameStateChanged;
			InputManager.onPressing -= OnPressing;
			InputManager.onReleased -= OnReleased;
		}

	}

}
