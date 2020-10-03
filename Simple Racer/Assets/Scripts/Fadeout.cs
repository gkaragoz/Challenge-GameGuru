using UnityEngine;

namespace SimpleRacer {

	[RequireComponent(typeof(CanvasGroup))]
	public class Fadeout : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private float _delayTime = 0.5f;
		[SerializeField]
		private float _time = 1f;
		[SerializeField]
		private CanvasGroup _canvasGroup = null;

		private void Start() {
			_canvasGroup.alpha = 1f;

			LeanTween.cancel(this.gameObject);

			this.gameObject.LeanDelayedCall(_delayTime, () => {
				this.gameObject.LeanValue(1f, 0f, _time)
					.setOnUpdate((float value) => {
						_canvasGroup.alpha = value;
						_canvasGroup.blocksRaycasts = false;
					});
			});
		}

	}

}
