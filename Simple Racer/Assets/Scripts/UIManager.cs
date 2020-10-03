using System;
using TMPro;
using UnityEngine;

namespace SimpleRacer {

	public class UIManager : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private RectTransform _txtTapToStartRect = null;
		[SerializeField]
		private RectTransform _btnRetry = null;
		[SerializeField]
		private TextMeshProUGUI _txtScore = null;

		private void Awake() {
			GameManager.onGameStateChanged += OnGameStateChanged;

			CarController.onRoadCompleted += OnRoadCompleted;
		}

		private void OnRoadCompleted() {
			_txtScore.text = GameManager.instance.PlayerScore.ToString();
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
