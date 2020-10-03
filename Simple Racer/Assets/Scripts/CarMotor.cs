using System;
using UnityEngine;

namespace SimpleRacer {

    [RequireComponent(typeof(Rigidbody))]
    public class CarMotor : MonoBehaviour {

        public static Action<Vector3> onHookStarted;
        public static Action onHookStopped;

        [Header("Initializations")]
        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _balanceRotationSpeed = 30f;
        [SerializeField]
        private AnimationCurve _balanceCurve = null;
        [SerializeField]
        private float _rotationSpeed = 15f;
        [SerializeField]
        private float _localRotationSpeed = 30f;
        [SerializeField]
        private float _localRotationClampMin = -25f;
        [SerializeField]
        private float _localRotationClampMax = 25f;
        [SerializeField]
        private float _driftTime = 1f;
        [SerializeField]
        private Transform _driftingPivotTransform = null;
        [SerializeField]
        private Rigidbody _rb = null;

        private Road _activeRoad = null;

        public bool IsTurningActive { get; private set; }
        public bool IsMoving { get { return _rb.velocity.magnitude > 0; } }
        public bool IsBoosted { get; private set; }

        public float Speed {
            get {
                return _speed;
            }
            set {
                _speed = value;
            }
        }

        private void Update() {
            if (GameManager.instance.GameState != GameState.Gameplay) {
                return;
            }

            if (IsTurningActive) {
                KeepTurning();
            } else {
                ApplyMovement();
            }
        }

        private void TurnLocalMesh() {
            float localTurnAmount = _localRotationSpeed * Time.deltaTime;
            _driftingPivotTransform.Rotate(Vector3.up * _activeRoad.GetTurnSide(), localTurnAmount);

            Vector3 currentRotation = _driftingPivotTransform.localRotation.eulerAngles.NormalizeAngleIn360();
            currentRotation.y = Mathf.Clamp(currentRotation.y, _localRotationClampMin, _localRotationClampMax);
            _driftingPivotTransform.localRotation = Quaternion.Euler(currentRotation);
        }

        private void KeepTurning() {
            StopDrifting();

            TurnLocalMesh();

            this.transform.RotateAround(_activeRoad.GetBarrelTransform().position, Vector3.up, _activeRoad.GetTurnSide() * _rotationSpeed * Time.deltaTime);
        }

        public void KeepCarBalancing(Vector3 balanceDirection) {
            if (IsMoving == false) {
                return;
            }

            if (IsTurningActive) {
                return;
            }

            Quaternion lookRotation = Quaternion.LookRotation(balanceDirection, Vector3.up);
            this.transform.localRotation = Quaternion.RotateTowards(this.transform.localRotation, lookRotation, _balanceCurve.Evaluate(_balanceRotationSpeed * Time.deltaTime));
        }

        public void StartDrifting() {
            StopDrifting();

            LeanTween.rotateLocal(_driftingPivotTransform.gameObject, Vector3.zero, _driftTime).setEaseOutElastic();
        }

        public void StopDrifting() {
            LeanTween.cancel(_driftingPivotTransform.gameObject);
        }

        public void ApplyMovement() {
            _rb.velocity = transform.forward * _speed;
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

            onHookStarted?.Invoke(_activeRoad.GetBarrelTransform().position);
        }

        public void StopTurning() {
            _activeRoad = null;

            IsTurningActive = false;

            onHookStopped?.Invoke();
        }

        public void BoostSpeed(Vector3 normalizePosition) {
            _speed *= 2.5f;

            this.transform.transform.position = normalizePosition;
            this.transform.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        public void NormalizeSpeed() {
            _speed /= 2.5f;
        }
        public void Lock() {
            StopMovement();
            StopTurning();
            StopDrifting();

            _rb.isKinematic = true;
        }

        public void Unlock() {
            _rb.isKinematic = false;
        }

    }

}
