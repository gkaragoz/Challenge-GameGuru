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

		private bool HasSelectedRoad() {
			return _selectedRoadDetails ? true : false;
		}

		public void SetRoadShape(RoadShape shape) {
			_selectedRoadDetails = _roadDetails.Where(rd => rd.RoadConnectionsSO.m_Shape == shape).FirstOrDefault();

			_selectedRoadDetails.Show();
		}

		public Vector3 GetConnectionPoint() {
			if (!HasSelectedRoad()) {
				Debug.LogWarning("Selected road not found.");
				return Vector3.zero;
			}

			return _selectedRoadDetails.GetConnectionPoint();
		}

		public void SetScale(float size) {
			if (!HasSelectedRoad()) {
				Debug.LogWarning("Selected road not found.");
				return;
			}

			_selectedRoadDetails.SetScale(size);
		}

	}

}
