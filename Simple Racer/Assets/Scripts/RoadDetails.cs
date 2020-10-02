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

		public void Show() {
			this.gameObject.SetActive(true);
		}

		public Vector3 GetConnectionPoint() {
			Vector3 connectionPoint = _connectionTransform.position;

			switch (_roadConnectionSO.m_Shape) {
				case RoadShape.LEFT_STRAIGHT:
                    connectionPoint.x -= this.transform.localScale.x * _roadScaleToWorldPosMultiplier;
					return connectionPoint;
				case RoadShape.RIGHT_STRAIGHT:
                    connectionPoint.x += this.transform.localScale.x * _roadScaleToWorldPosMultiplier;
                    return connectionPoint;
				case RoadShape.UP_STRAIGHT:
                    connectionPoint.z += this.transform.localScale.z * _roadScaleToWorldPosMultiplier;
                    return connectionPoint;
			}

            return connectionPoint;
		}

		public void SetScale(float size) {
			switch (_roadConnectionSO.m_Shape) {
				case RoadShape.LEFT_STRAIGHT:
					this.transform.localScale = this.transform.localScale.WithX(size);
					break;
				case RoadShape.RIGHT_STRAIGHT:
					this.transform.localScale = this.transform.localScale.WithX(size);
					break;
				case RoadShape.UP_STRAIGHT:
					this.transform.localScale = this.transform.localScale.WithZ(size);
					break;
			}
		}

	}

}
