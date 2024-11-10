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
    // ���� ȭ�� �� ��� UI �����ܵ� ����Ʈ
    //public List<Image> life = new List<Image>();
    public Image[] lifeImages;
    // ����� ���� ���� �̹���
    public Sprite live_flower;
    // ����� ���� ���� �̹���
    public Sprite death_flower;

    // �÷��̾��� �ʱ� ��ġ
    public Vector3 player_init_pos;
    // �� ������Ʈ��
    public List<GameObject> enemies = new List<GameObject>();
    // �� ������Ʈ���� �ʱ� ��ġ
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
                continue; // null�� ��� ���� �ݺ����� �Ѿ
            }
            // ��� �̹��� ���̰� ����
            lifeImages[i].enabled = i < player.max_life;
            // ��� �̹����� ����� �ִ� �̹����� ����
            lifeImages[i].sprite = live_flower;
        }
    }
    private void Start()
    {
        GameTime = 60;
        player.max_life = max_life;
        GameManager.instance.UpdateLife();
        // �÷��̾��� �ִ� ����� ��ŭ �ݺ��Ͽ�
        for (int i = 0; i < player.max_life; i++)
        {
            // ��� �̹��� ���̰� ����
            lifeImages[i].enabled = i < player.max_life;
            lifeImages[i].sprite = live_flower;
        }
    }

    // ȭ�� �� ��� UI ������Ʈ �Լ�
    public void UpdateLife()
    {
        if (lifeImages == null || player == null)
        {
            Debug.LogError("lifeImages or player is null in UpdateLife.");
            return;
        }
        // �÷��̾��� �ִ� ��� �� ��ŭ �ݺ�
        for (int i = 0; i < player.max_life; i++)
        {
            if (i < lifeImages.Length && lifeImages[i] != null)
            {
                if (i < player.now_life)
                {
                    // ����� �ִ� �̹����� ����
                    lifeImages[i].sprite = live_flower;
                    Debug.Log($"lifeImages[{i}] set to live_flower");
                }
                else
                {
                    // ����� ���� �̹����� ����
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
