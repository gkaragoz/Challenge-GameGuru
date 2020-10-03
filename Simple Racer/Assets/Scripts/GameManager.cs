using System;
using UnityEngine;

namespace SimpleRacer {
    public class GameManager : MonoBehaviour {

        public static GameManager instance;

        public static Action<GameState> onGameStateChanged;

        [Header("Debug")]
        [SerializeField]
        private GameState _gameState;

        public GameState GameState {
            get { return _gameState; }
            private set {
                Debug.LogWarning("GameState has been changed from " + _gameState + " to " + value);

                _gameState = value;
                onGameStateChanged?.Invoke(GameState);
            }
        }

        private void Awake() {
            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(instance);

            RoadGenerator.onMapGenerated += OnMapGenerated;
            InputManager.onFirstTimePressed += OnFirstTimePressed;
        }

        private void Start() {
            InitNewGame(); 
        }

        private void OnMapGenerated() {
            GameState = GameState.Prestage;
        }

        private void OnFirstTimePressed() {
            GameState = GameState.Gameplay;
        }

        private void GenerateMap() {
            GameState = GameState.LevelGeneration;
        }

        public void InitNewGame() {
            GenerateMap();
        }

    }
}
