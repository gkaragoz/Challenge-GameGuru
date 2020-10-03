namespace SimpleRacer {

	public enum RoadShape {
		FROM_UP_TO_TURN_RIGHT_CORNER,
		FROM_UP_TO_TURN_LEFT_CORNER,
		FROM_RIGHT_TO_TURN_UP_CORNER,
		FROM_LEFT_TO_TURN_UP_CORNER,
		FROM_LEFT_TO_RIGHT_U_SHAPE,
		FROM_RIGHT_TO_LEFT_U_SHAPE,
		LEFT_STRAIGHT,
		RIGHT_STRAIGHT,
		UP_STRAIGHT
	}

	public enum GameState {
		Loading,
		MapGeneration,
		Prestage,
		Gameplay,
		GameOver,
	}

	public static class EnumsHelper {
		public static RoadShape GetRandomRoadShape() {
			return (RoadShape)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(RoadShape)).Length);
		}

		public static UnityEngine.Vector3 GetDirectionFromStraightRoadShape(this RoadShape roadShape) {
			switch (roadShape) {
				case RoadShape.LEFT_STRAIGHT:
					return UnityEngine.Vector3.left;
				case RoadShape.RIGHT_STRAIGHT:
					return UnityEngine.Vector3.right;
				case RoadShape.UP_STRAIGHT:
					return UnityEngine.Vector3.forward;
			}
			return UnityEngine.Vector3.zero;
		}

		public static bool IsStraight(this RoadShape roadShape) {
			switch (roadShape) {
				case RoadShape.LEFT_STRAIGHT:
				case RoadShape.RIGHT_STRAIGHT:
				case RoadShape.UP_STRAIGHT:
					return true;
				default:
					return false;
			}
		}
	}

}
