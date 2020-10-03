using System;
using UnityEngine;

namespace SimpleRacer {

    [RequireComponent(typeof(LineRenderer))]
    public class RopeLineRenderer : MonoBehaviour {

        [Header("Initializations")]
        [SerializeField]
        private LineRenderer _lineRenderer = null;
        [SerializeField]
        private Vector3 _centerOffset = Vector3.zero;
        [SerializeField]
        private Vector3 _targetOffset = Vector3.zero;

        [Header("Debug")]
        [SerializeField]
        private bool _isDrawing = false;
        [SerializeField]
        private Vector3 _hookedCenterPosition = Vector3.zero;

        private void Awake() {
            CarMotor.onHookStarted += OnHookStarted;
            CarMotor.onHookStopped += OnHookStopped;
        }

        private void Update() {
            if (_isDrawing) {
                Draw();
            }
        }

        private void Draw() {
            _lineRenderer.SetPositions(new Vector3[] {
                     transform.position + _centerOffset,
                     _hookedCenterPosition + _targetOffset
            });
        }

        private void OnHookStarted(Vector3 targetPosition) {
            this._hookedCenterPosition = targetPosition;

            _lineRenderer.positionCount = 2;

            _isDrawing = true;
        }

        private void OnHookStopped() {
            _isDrawing = false;

            _lineRenderer.positionCount = 0;
        }

    }

}
