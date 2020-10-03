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
	}

}
