using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float GameTime = 11;
    public Text GameTimeText;
    public GameObject GameOverCanvas;
    public GameObject GameStopCanvas;
    public GameObject Player;
    public GameObject Enemy;
    public static GameManager instance;
    public Player player;
    // 게임 화면 내 목숨 UI 아이콘들 리스트
    public List<Image> life = new List<Image>();
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

    private void Start()
    {
        GameTime = 11;
        GameManager.instance.UpdateLife();
        // 플레이어의 최대 목숨수 만큼 반복하여
        for (int i = 0; i < player.max_life; i++)
        {
            // 목숨 이미지 보이게 설정
            life[i].enabled = true;
            // 목숨 이미지를 목숨이 있는 이미지로 설정
            life[i].sprite = live_flower;
        }

        GameManager.instance.UpdateLife();

        for (int i = 0; i < player.max_life; i++)
        {
            life[i].enabled = true;
            life[i].sprite = live_flower;
        }
    }
    // 화면 내 목숨 UI 업데이트 함수
    public void UpdateLife()
    {
        // 플레이어의 최대 목숨 수 만큼 반복
        for (int i = 0; i < player.max_life; i++)
        {
            // i가 현재 목숨 수 값보다 작은 값이면
            if (i < player.now_life)
            {
                // 목숨이 있는 이미지로 설정
                life[i].sprite = live_flower;
            }
            // i가 현재 목숨 수 값보다 크면
            else
            {
                // 목숨이 없는 이미지로 설정
                life[i].sprite = death_flower;
            }
        }
    }
}
