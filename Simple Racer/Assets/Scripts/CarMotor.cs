using UnityEngine;

namespace SimpleRacer {

	[RequireComponent(typeof(Rigidbody))]
	public class CarMotor : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private float _speed = 1f;

		[Header("Debug")]
		[SerializeField]
		private bool _debugMode = false;

		private Rigidbody _rb = null;

		private void Awake() {
			_rb = GetComponent<Rigidbody>();
		}

		private void Update() {
			if (_debugMode) {
				ApplySpeed();
			}
		}

		public float Speed { 
			get {
				return _speed;
			}
			set {
				_speed = value;
			} 
		}

		public void StartMovement() {
			_rb.velocity = Vector3.forward * _speed;
		}

		public void StopMovement() {
			_rb.velocity = Vector3.zero;
		}

		public void HandleTurn() {

		}

		public void ApplySpeed() {
			_rb.velocity = _rb.velocity.normalized * _speed;
		}

	}

}
