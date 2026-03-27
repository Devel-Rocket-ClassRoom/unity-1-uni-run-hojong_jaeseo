using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance => instance ?? FindFirstObjectByType<GameManager>();

    public float Energy { get; private set; } = 100f;
    public float Score { get; private set; } = 0f;
    public readonly float HitEnergyReduce = 20.0f;
    public bool IsGameOver => Energy <= 0f;

    [SerializeField]
    private float _energyReduceSpeed = 2f;

    private void Update() {
        if (IsGameOver) {
            // GameOver UI 띄우기

            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        Energy -= _energyReduceSpeed * Time.deltaTime;
    }

    public void AddScore(float score) {
        Score += score;

        Debug.Log(Score);
    }

    public void AddEnergy(float energy) {
        Energy += energy;

        Energy = Mathf.Min(Energy, 100f);

        Debug.Log(Energy);
    }

    public void ReduceEnergy(float energy) {
        Energy -= energy;

        Debug.Log(Energy);
    }
}
