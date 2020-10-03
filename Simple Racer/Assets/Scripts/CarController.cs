using UnityEngine;

namespace SimpleRacer {

	public class CarController : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private CarMotor _carMotor = null;

		[SerializeField]
		private bool _hasStopped = true;

		private Vector3 _spawnPosition = Vector3.zero;
		private Quaternion _spawnRotation = Quaternion.identity;

		private void Awake() {
			GameManager.onGameStateChanged += OnGameStateChanged;
			InputManager.onPressing += OnPressing;

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
			} else {
				StartMovement();
			}
		}

		private void OnPressing() {
			HandleTurn();
		}

		private void StartMovement() {
			_carMotor.StartMovement();
		}

		private void StopMovement() {
			_carMotor.StopMovement();
		}

		private void HandleTurn() {
			_carMotor.HandleTurn();
		}

		private void OnCollisionEnter(Collision collision) {
			Debug.Log(collision.gameObject.name);

			GameManager.instance.GameOver();
		}

	}

}
