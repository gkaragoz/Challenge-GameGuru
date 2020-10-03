using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRacer {
	public class RoadGenerator : MonoBehaviour {

		public static Action onMapGenerated;

		[Header("Initializations")]
		[SerializeField]
		private Road _road = null;
		[SerializeField]
		private float _startRoadScale = 10f;
		[SerializeField]
		private float _straightRoadScale = 5f;
		[SerializeField]
		private float _levelUpRoadScale = 10f;
		[SerializeField]
		private int _levelUpFrequency = 15;

		[Header("Debug")]
		[SerializeField]
		private Road _lastSpawnedRoad = null;
		[SerializeField]
		private int _spawnedCornerCounter = 0;

		private Queue<Road> _roads = new Queue<Road>();

		private void Awake() {
			GameManager.onGameStateChanged += OnGameStateChanged;
			CarController.onCornerRoadCompleted += OnRoadCompleted;
			CarController.onStraightRoadCompleted += OnRoadCompleted;
		}

		private void OnGameStateChanged(GameState gameState) {
			if (gameState == GameState.MapGeneration) {
				Generate();
			}
		}

		private void OnRoadCompleted() {
			ObjectPool.Recycle(_roads.Dequeue());
			SpawnRoad();
		}

		private void SpawnStartRoad() {
			Road spawnedRoad = _road.Spawn(this.transform, Vector3.zero);
			spawnedRoad.SetRoadShape(RoadShape.UP_STRAIGHT);
			spawnedRoad.SetScale(_startRoadScale);
			spawnedRoad.SetRandomConnection();

			_roads.Enqueue(spawnedRoad);

			_lastSpawnedRoad = spawnedRoad;
		}

		private void SpawnMap() {
			int pooledReadyCount = _road.CountPooled();
			for (int ii = 0; ii < pooledReadyCount; ii++) {
				SpawnRoad();
			}
		}

		private void SpawnRoad() {
			Road spawnedRoad = _road.Spawn(this.transform, _lastSpawnedRoad.GetConnectionPoint());

			RoadShape nextShape = _lastSpawnedRoad.GetRoadConnectionShape();
			spawnedRoad.SetRoadShape(nextShape);

			if (ShouldSpawnLevelUpRoad() && nextShape.IsStraight()) {
				spawnedRoad.SetScale(_levelUpRoadScale);
				spawnedRoad.SetAsLevelUpRoad();
			} else {
				_spawnedCornerCounter++;
				spawnedRoad.SetScale(_straightRoadScale);
			}

			spawnedRoad.SetRandomConnection();

			_roads.Enqueue(spawnedRoad);

			_lastSpawnedRoad = spawnedRoad;
		}

		private bool ShouldSpawnLevelUpRoad() {
			if (_spawnedCornerCounter == 0) {
				return false;
			}

			return _spawnedCornerCounter % ((_levelUpFrequency * 2) - 1) == 0;
		}

		public void Generate() {
			SpawnStartRoad();
			SpawnMap();

			onMapGenerated?.Invoke();
		}

		private void OnDestroy() {
			GameManager.onGameStateChanged -= OnGameStateChanged;
			CarController.onCornerRoadCompleted -= OnRoadCompleted;
			CarController.onStraightRoadCompleted -= OnRoadCompleted;
		}

	}
}
