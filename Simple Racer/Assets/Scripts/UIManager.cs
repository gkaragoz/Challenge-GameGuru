using System;
using TMPro;
using UnityEngine;

namespace SimpleRacer {

	public class UIManager : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private float _levelUpTextTime = 1f;
		[SerializeField]
		private RectTransform _txtTapToStartRect = null;
		[SerializeField]
		private RectTransform _btnRetry = null;
		[SerializeField]
		private TextMeshProUGUI _txtScore = null;
		[SerializeField]
		private RectTransform _txtLevelUpRect = null;

		private void Awake() {
			GameManager.onGameStateChanged += OnGameStateChanged;

			CarController.onRoadCompleted += OnRoadCompleted;
			CarController.onLevelUp += OnLevelUp;
		}

		private void OnLevelUp() {
			_txtLevelUpRect.gameObject.SetActive(true);

			_txtLevelUpRect.gameObject.LeanDelayedCall(_levelUpTextTime, () => {
				_txtLevelUpRect.gameObject.SetActive(false);
			});
		}

		private void OnRoadCompleted() {
			SetScore();
		}

		private void SetScore() {
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
					SetScore();
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

		private void OnDestroy() {
			GameManager.onGameStateChanged -= OnGameStateChanged;

			CarController.onRoadCompleted -= OnRoadCompleted;
			CarController.onLevelUp -= OnLevelUp;
		}
	}

}
