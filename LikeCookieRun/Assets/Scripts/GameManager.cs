using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance => instance ?? FindFirstObjectByType<GameManager>();

    private float _maxHealth = 100;
    public float Health { get; private set; }
    public float Score { get; private set; }
    public readonly float HitEnergyReduce = 20.0f;
    public bool IsGameOver => Health <= 0f;

    [SerializeField]
    private float _energyReduceSpeed = 2f;

    [SerializeField]
    private TextMeshProUGUI _healthBarText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private void Awake() {
        Health = _maxHealth;
        Score = 0;
    }

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

        Health -= _energyReduceSpeed * Time.deltaTime;

        _healthBarText.text = new string('|', (int)(Health / _maxHealth * 75f));
    }

    public void AddScore(float score) {
        Score += score;

        _scoreText.text = $"Score : {Score}";
        Debug.Log(Score);
    }

    public void AddHealth(float energy) {
        Health += energy;

        Health = Mathf.Min(Health, 100f);

        Debug.Log(Health);
    }

    public void ReduceHealth(float energy) {
        Health -= energy;

        Debug.Log(Health);
    }
}
