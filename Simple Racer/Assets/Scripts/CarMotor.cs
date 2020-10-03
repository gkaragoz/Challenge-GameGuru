using UnityEngine;

namespace SimpleRacer {

	[RequireComponent(typeof(Rigidbody))]
	public class CarMotor : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private float _speed = 1f;

		private Rigidbody _rb = null;

		private void Awake() {
			_rb = GetComponent<Rigidbody>();
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
			_rb.angularVelocity = Vector3.zero;
		}

		public void HandleTurn() {

		}

	}

}
