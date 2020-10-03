using System;
using UnityEngine;

namespace SimpleRacer {

	public class CarController : MonoBehaviour {

		public static Action onRoadCompleted;

		[Header("Initializations")]
		[SerializeField]
		private CarMotor _carMotor = null;

		[SerializeField]
		private bool _hasStopped = true;

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

		private void OnGameStateChanged(GameState gameState) {
			switch (gameState) {
				case GameState.MapGeneration:
				case GameState.Prestage:
					_hasStopped = true;
					ResetCar();
					break;
				case GameState.GameOver:
					_hasStopped = true;
					break;
				case GameState.Gameplay:
					_hasStopped = false;
					break;
			}

			SetCarState();
		}

		private void ResetCar() {
			transform.position = _spawnPosition;
			transform.rotation = _spawnRotation;

			StopMovement();
		}

		private void SetCarState() {
			if (_hasStopped) {
				StopMovement();
				StopTurning();
			} else {
				StartMovement();
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
					StartMovement();
				}
			}
		}

		private void StartMovement() {
			_carMotor.StartMovement();
		}

		private void StopMovement() {
			_carMotor.StopMovement();
		}

		private void StartTurning() {
			if (_activeRoad == null && _carMotor.IsTurningActive) {
				return;
			}

			if (_activeRoad.HasBarrel()) {
				_carMotor.StartTurning(_activeRoad);
			}
		}

		private void StopTurning() {
			_carMotor.StopTurning();
		}

		private void OnCollisionEnter(Collision collision) {
			Debug.Log(collision.gameObject.name);

			GameManager.instance.GameOver();
		}

		private void OnTriggerEnter(Collider other) {
			_activeRoad = other.transform.GetComponentInParent<Road>();
		}

		private void OnTriggerExit(Collider other) {
			GameManager.instance.AddScore();

			onRoadCompleted?.Invoke();
		}

	}

}
