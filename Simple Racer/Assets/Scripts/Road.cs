using System;
using System.Linq;
using UnityEngine;

namespace SimpleRacer {

	public class Road : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private RoadDetails[] _roadDetails = null;

		[Header("Debug")]
		[SerializeField]
		private RoadDetails _selectedRoadDetails = null;
		[SerializeField]
		private bool _isLevelUpRoad = false;

		private bool HasSelectedRoad() {
			return _selectedRoadDetails ? true : false;
		}

		public void SetRoadShape(RoadShape shape) {
			_selectedRoadDetails = _roadDetails.Where(rd => rd.RoadConnectionsSO.m_Shape == shape).FirstOrDefault();

			foreach (RoadDetails roadDetails in _roadDetails) {
				roadDetails.Hide();
			}

			_selectedRoadDetails.Show();
		}

		public void SetTurnedRoadShape() {
			RoadShape[] roadShapes = new RoadShape[] {
				RoadShape.FROM_LEFT_TO_RIGHT_U_SHAPE,
				RoadShape.FROM_LEFT_TO_TURN_UP_CORNER,
				RoadShape.FROM_RIGHT_TO_LEFT_U_SHAPE,
				RoadShape.FROM_RIGHT_TO_TURN_UP_CORNER,
				RoadShape.FROM_UP_TO_TURN_LEFT_CORNER,
				RoadShape.FROM_UP_TO_TURN_RIGHT_CORNER,
			};

			RoadShape randomTurnedRoadShape = roadShapes[UnityEngine.Random.Range(0, roadShapes.Length)];

			SetRoadShape(randomTurnedRoadShape);
		}

		public Vector3 GetConnectionPoint() {
			if (!HasSelectedRoad()) {
				Debug.LogWarning("Selected road not found.");
				return Vector3.zero;
			}

			return _selectedRoadDetails.GetConnectionPoint();
		}

		public RoadShape GetRoadConnectionShape() {
			if (!HasSelectedRoad()) {
				Debug.LogWarning("Selected road not found.");
				return RoadShape.UP_STRAIGHT;
			}

			return _selectedRoadDetails.RoadConnectionShape;
		}

		public Transform GetBarrelTransform() {
			if (!HasSelectedRoad()) {
				Debug.LogWarning("Selected road not found.");
				return null;
			}

			return _selectedRoadDetails.Barrel;
		}

		public bool HasBarrel() {
			if (!HasSelectedRoad()) {
				Debug.LogWarning("Selected road not found.");
				return false;
			}

			return _selectedRoadDetails.Barrel ? true : false;
		}

		public void SetScale(float size) {
			if (!HasSelectedRoad()) {
				Debug.LogWarning("Selected road not found.");
				return;
			}

			_selectedRoadDetails.SetScale(size);
		}

		public void SetRandomConnection() {
			if (!HasSelectedRoad()) {
				Debug.LogWarning("Selected road not found.");
				return;
			}

			_selectedRoadDetails.SetRandomConnection();
		}

		public int GetTurnSide() {
			if (!HasSelectedRoad()) {
				Debug.LogWarning("Selected road not found.");
				return 0;
			}

			return _selectedRoadDetails.GetTurnSide();
		}

		public void SetAsLevelUpRoad() {
			_isLevelUpRoad = true;
		}

		public bool IsLevelUpRoad() {
			return _isLevelUpRoad;
		}

	}

}
