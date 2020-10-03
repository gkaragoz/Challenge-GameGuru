using System;
using UnityEngine;

namespace SimpleRacer {

	public class InputManager : MonoBehaviour {

		public static Action onFirstTimePressed;
		public static Action onPressing;

		[SerializeField]
		private bool _hasBlocked = false;

		private void Awake() {
			GameManager.onGameStateChanged += OnGameStateChanged;
		}

		private void OnGameStateChanged(GameState gameState) {
			switch (gameState) {
				case GameState.LevelGeneration:
				case GameState.LevelFailed:
					_hasBlocked = true;
					break;
				case GameState.Prestage:
				case GameState.Gameplay:
					_hasBlocked = false;
					break;
			}
		}

		private void Update() {
			if (_hasBlocked) {
				return;
			}

			if (Input.GetMouseButton(0)) {
				if (GameManager.instance.GameState == GameState.Prestage) {
					onFirstTimePressed?.Invoke();
				}

				onPressing?.Invoke();
			}
		}

	}

}
