using Mono.Cecil.Cil;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField]
    private Map[] _mapPrefabs;
    [SerializeField]
    private Map _startMap;
    private Map[] _maps;

    private void Awake() {
        // 스타트맵도 사라질 때 새 맵 만들어줘야함
        _startMap.OnDisapper += SpawnNewMap;
        _maps = new Map[_mapPrefabs.Length];

        for (int i = 0; i < _mapPrefabs.Length; i++) {
            // 처음 시작할 때 각 맵 생성 후 비활성화
            _maps[i] = Instantiate(_mapPrefabs[i]);
            _maps[i].gameObject.SetActive(false);

            // 각각의 맵이 사라질 때 다음 맵 생성을 호출하도록 설정
            _maps[i].OnDisapper += SpawnNewMap;
        }
    }

    private void Start() {
        // 시작하면 맵 하나 스폰
        SpawnNewMap();
    }

    public void SpawnNewMap() {
        Map spawningMap;
        while (true) {
            spawningMap = _maps[Random.Range(0, _maps.Length)];
            if (spawningMap.isActiveAndEnabled) { continue; }
            break;
        }
        
        spawningMap.gameObject.SetActive(true);
        spawningMap.transform.position = new Vector2(Camera.ScreenWidth, 0);
    }
}
