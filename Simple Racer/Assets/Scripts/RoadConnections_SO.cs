using UnityEngine;

namespace GK {

	[CreateAssetMenu(fileName = "RoadConnection", menuName = "ScriptableObjects/Road/Connection", order = 1)]
	public class RoadConnections_SO : ScriptableObject {

		[SerializeField]
		public RoadShape m_Shape;
		[SerializeField]
		public Road m_Road = null;
		[SerializeField]
		public RoadShape[] connections = null;

	}

}
