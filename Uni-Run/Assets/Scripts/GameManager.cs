using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : MonoBehaviour {
    public static GameManager instance; // 싱글톤 인스턴스

    public bool isGameOver = false;
    public Text scoreText; // 점수를 표시할 UI 텍스트
    public GameObject gameoverUI; // 게임 오버 UI 게임 오브젝트

    private int score = 0; //게임 점수

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 GameManager가 존재합니다. 첫번째 GameManager를 사용합니다.");
            Destroy(gameObject); // 이미 존재하는 GameManager가 있으니, 현재의 GameManager를 파괴한다.
        }
    }

    void Update() {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if(isGameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 Active된 게임 씬을 로드함
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore) {
        if(!isGameOver)
        {
            score += newScore;
            scoreText.text = "Score: " + score;
        }
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead() {
        isGameOver = true;
        gameoverUI.SetActive(true);
    }
}