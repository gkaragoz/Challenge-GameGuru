using UnityEngine;

namespace SimpleRacer {

	public class CarController : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private CarMotor _carMotor = null;

		[SerializeField]
		private bool _hasStopped = true;

		private void Awake() {
			GameManager.onGameStateChanged += OnGameStateChanged;
			InputManager.onPressing += OnPressing;
		}

		private void OnGameStateChanged(GameState gameState) {
			switch (gameState) {
				case GameState.MapGeneration:
				case GameState.Prestage:
				case GameState.GameOver:
					_hasStopped = true;
					break;
				case GameState.Gameplay:
					_hasStopped = false;
					break;
			}

			SetCarState();
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
