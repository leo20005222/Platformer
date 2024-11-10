using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float GameTime = 360;
    public int max_life = 3;
    public Text GameTimeText;
    public GameObject GameOverCanvas;
    public GameObject GameStopCanvas;
    public GameObject Player;
    public GameObject Enemy;
    public static GameManager instance;
    public Player player;
    // 게임 화면 내 목숨 UI 아이콘들 리스트
    //public List<Image> life = new List<Image>();
    public Image[] lifeImages;
    // 목숨이 있을 때의 이미지
    public Sprite live_flower;
    // 목숨이 없을 때의 이미지
    public Sprite death_flower;

    // 플레이어의 초기 위치
    public Vector3 player_init_pos;
    // 적 오브젝트들
    public List<GameObject> enemies = new List<GameObject>();
    // 적 오브젝트들의 초기 위치
    public List<Vector3> enemy_init_pos = new List<Vector3>();
    public GameObject success_window;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Avoid having multiple instances of GameManager
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameTime -= Time.deltaTime;
        GameTimeText.text = "Time: " + (int)GameTime;
        if ((int)GameTime == 0)
        {
            GameOverCanvas.SetActive(true);
            Player.SetActive(false);
            Enemy.SetActive(false);
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                GameStopCanvas.SetActive(false);
                Player.SetActive(true);
                Enemy.SetActive(true);
                Time.timeScale = 1f;
            }
            else
            {
                GameStopCanvas.SetActive(true);
                if(Player)
                {
                    Player.SetActive(false);
                }
                if(Enemy)
                {
                    Enemy.SetActive(false);
                }
               
                Time.timeScale = 0f;
            }
        }
    }
    public void ResetHealth()
    {
        if (player == null)
        {
            Debug.LogError("Player is null in ResetHealth.");
            return;
        }

        if (lifeImages == null)
        {
            Debug.LogError("lifeImages array is null in ResetHealth.");
            return;
        }
        player.max_life = max_life;
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (lifeImages[i] == null)
            {
                Debug.LogError($"lifeImages[{i}] is null in ResetHealth.");
                continue; // null인 경우 다음 반복으로 넘어감
            }
            // 목숨 이미지 보이게 설정
            lifeImages[i].enabled = i < player.max_life;
            // 목숨 이미지를 목숨이 있는 이미지로 설정
            lifeImages[i].sprite = live_flower;
        }
    }
    private void Start()
    {
        GameTime = 60;
        player.max_life = max_life;
        GameManager.instance.UpdateLife();
        // 플레이어의 최대 목숨수 만큼 반복하여
        for (int i = 0; i < player.max_life; i++)
        {
            // 목숨 이미지 보이게 설정
            lifeImages[i].enabled = i < player.max_life;
            lifeImages[i].sprite = live_flower;
        }
    }

    // 화면 내 목숨 UI 업데이트 함수
    public void UpdateLife()
    {
        if (lifeImages == null || player == null)
        {
            Debug.LogError("lifeImages or player is null in UpdateLife.");
            return;
        }
        // 플레이어의 최대 목숨 수 만큼 반복
        for (int i = 0; i < player.max_life; i++)
        {
            if (i < lifeImages.Length && lifeImages[i] != null)
            {
                if (i < player.now_life)
                {
                    // 목숨이 있는 이미지로 설정
                    lifeImages[i].sprite = live_flower;
                    Debug.Log($"lifeImages[{i}] set to live_flower");
                }
                else
                {
                    // 목숨이 없는 이미지로 설정
                    lifeImages[i].sprite = death_flower;
                    Debug.Log($"lifeImages[{i}] set to death_flower");
                }
            }
            else
            {
                Debug.LogError($"lifeImages[{i}] is out of bounds or null.");
            }
        }
    }
}
