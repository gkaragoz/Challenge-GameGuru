using UnityEngine;

namespace SimpleRacer {

	public class RoadDetails : MonoBehaviour {

		[SerializeField]
		private float _roadScaleToWorldPosMultiplier = 10f;
		[SerializeField]
		private RoadConnections_SO _roadConnectionSO = null;
		[SerializeField]
		private Transform _barrel = null;
		[SerializeField]
		private Transform _connectionTransform = null;

		public RoadConnections_SO RoadConnectionsSO { get { return _roadConnectionSO; } }
		public Transform Barrel { get { return _barrel; } }
		public RoadShape RoadShape { get { return _roadConnectionSO.m_Shape; } }
		public RoadShape RoadConnectionShape { get { return _roadConnectionSO.m_ConnectionShape; } }

		public void Show() {
			this.gameObject.SetActive(true);
		}

		public void Hide() {
			this.gameObject.SetActive(false);
		}

		public Vector3 GetConnectionPoint() {
			switch (_roadConnectionSO.m_Shape) {
				case RoadShape.LEFT_STRAIGHT:
					_connectionTransform.position -= _connectionTransform.localPosition.WithX(this.transform.localScale.z * 2 * _roadScaleToWorldPosMultiplier);
					break;
				case RoadShape.RIGHT_STRAIGHT:
					_connectionTransform.position += _connectionTransform.localPosition.WithX(this.transform.localScale.z * 2 * _roadScaleToWorldPosMultiplier);
					break;
				case RoadShape.UP_STRAIGHT:
					_connectionTransform.position += _connectionTransform.localPosition.WithZ(this.transform.localScale.z * 2 * _roadScaleToWorldPosMultiplier);
					break;
			}

			return _connectionTransform.position;
		}

		public void SetScale(float size, bool isFirstRoad = false) {
			if (isFirstRoad) {
				this.transform.localScale = this.transform.localScale.WithZ(size);
				_roadScaleToWorldPosMultiplier = size / 2;
				return;
			}

			switch (_roadConnectionSO.m_Shape) {
				case RoadShape.UP_STRAIGHT:
				case RoadShape.LEFT_STRAIGHT:
				case RoadShape.RIGHT_STRAIGHT:
					this.transform.localScale = this.transform.localScale.WithZ(size);
					_roadScaleToWorldPosMultiplier = size;
					break;
			}
		}

		public void SetRandomConnection() {
			_roadConnectionSO.SetRandomConnection();
		}

		public int GetTurnSide() {
			switch (_roadConnectionSO.m_Shape) {
				case RoadShape.FROM_UP_TO_TURN_RIGHT_CORNER:
				case RoadShape.FROM_LEFT_TO_RIGHT_U_SHAPE:
				case RoadShape.FROM_LEFT_TO_TURN_UP_CORNER:
					return 1;
				case RoadShape.FROM_UP_TO_TURN_LEFT_CORNER:
				case RoadShape.FROM_RIGHT_TO_TURN_UP_CORNER:
				case RoadShape.FROM_RIGHT_TO_LEFT_U_SHAPE:
					return -1;
			}
			return 0;
		}

	}

}
