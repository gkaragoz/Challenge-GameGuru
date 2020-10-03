using System;
using UnityEngine;

namespace SimpleRacer {

	public class UIManager : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private RectTransform _txtTapToStartRect = null;
		[SerializeField]
		private RectTransform _btnRetry = null;

		private void Awake() {
			GameManager.onGameStateChanged += OnGameStateChanged;
		}

		private void OnGameStateChanged(GameState gameState) {
			switch (gameState) {
				case GameState.Loading:
				case GameState.MapGeneration:
					_txtTapToStartRect.gameObject.SetActive(false);
					_btnRetry.gameObject.SetActive(false);
					break;
				case GameState.Prestage:
					_txtTapToStartRect.gameObject.SetActive(true);
					break;
				case GameState.Gameplay:
					_txtTapToStartRect.gameObject.SetActive(false);
					break;
				case GameState.GameOver:
					_btnRetry.gameObject.SetActive(true);
					break;
			}
		}
	}

}
