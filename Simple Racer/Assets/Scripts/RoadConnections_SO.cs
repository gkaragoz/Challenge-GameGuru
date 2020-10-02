using UnityEngine;

namespace SimpleRacer {

	[CreateAssetMenu(fileName = "RoadConnection", menuName = "ScriptableObjects/Road/Connection", order = 1)]
	public class RoadConnections_SO : ScriptableObject {

		public RoadShape m_Shape;
		public Road m_Road = null;
		public RoadShape m_ConnectionShape;
		public RoadShape[] connections = null;

		public void SetRandomConnection() {
			int randomIndex = Random.Range(0, connections.Length);
			m_ConnectionShape = connections[randomIndex];
		}

	}

}
