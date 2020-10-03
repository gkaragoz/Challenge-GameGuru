using UnityEngine;

namespace SimpleRacer {

	[RequireComponent(typeof(Rigidbody))]
	public class CarMotor : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private float _speed = 1f;
		[SerializeField]
		private float _rotationSpeed = 15f;

		private Rigidbody _rb = null;
		private Road _activeRoad = null;

		public bool IsTurningActive { get; private set; }

		private void Awake() {
			_rb = GetComponent<Rigidbody>();
		}

		private void Update() {
			if (IsTurningActive) {
				KeepTurning();
			}
		}

		private void KeepTurning() {
			this.transform.RotateAround(_activeRoad.GetBarrelTransform().position, Vector3.up, _activeRoad.GetTurnSide() * _rotationSpeed * Time.deltaTime);
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
			_rb.velocity = transform.forward * _speed;
		}

		public void StopMovement() {
			_rb.velocity = Vector3.zero;
			_rb.angularVelocity = Vector3.zero;
		}

		public void StartTurning(Road enteredRoad) {
			_activeRoad = enteredRoad;

			StopMovement();

			IsTurningActive = true;
		}

		public void StopTurning() {
			_activeRoad = null;

			IsTurningActive = false;
		}

	}

}
