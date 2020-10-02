using System.Collections.Generic;
using UnityEngine;

namespace SimpleRacer {
	public class RoadGenerator : MonoBehaviour {

		[Header("Initializations")]
		[SerializeField]
		private Road _road = null;
		[SerializeField]
		private float _startRoadScale = 10f;

		[Header("Debug")]
		[SerializeField]
		private Road _lastSpawnedRoad = null;

		private Queue<Road> _roads = new Queue<Road>();

		private void Start() {
			SpawnStartRoad();
			SpawnMap();
		}

		private void SpawnStartRoad() {
			Road spawnedRoad = _road.Spawn(this.transform, Vector3.zero);
			spawnedRoad.SetRoadShape(RoadShape.UP_STRAIGHT);
			spawnedRoad.SetScale(_startRoadScale);

			_roads.Enqueue(spawnedRoad);

			_lastSpawnedRoad = spawnedRoad;
		}

		private void SpawnMap() {
			for (int ii = 0; ii < _road.CountPooled(); ii++) {
				//Road spawnedRoad = _road.Spawn(this.transform, )

				//_roads.Enqueue()

				//_lastSpawnedRoad = spawnedRoad;
			}
		}

	}
}
