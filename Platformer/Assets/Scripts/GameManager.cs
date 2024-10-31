using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float GameTime = 11;
    public Text GameTimeText;
    public GameObject GameOverCanvas;
    public GameObject GameStopCanvas;
    public static GameManager instance;
    public Player player;
    // ���� ȭ�� �� ��� UI �����ܵ� ����Ʈ
    public List<Image> life = new List<Image>();
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
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                GameStopCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                GameStopCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    private void Start()
    {
        // �÷��̾��� �ִ� ����� ��ŭ �ݺ��Ͽ�
        for (int i = 0; i < player.max_life; i++)
        {
            // ��� �̹��� ���̰� ����
            life[i].enabled = true;
            // ��� �̹����� ����� �ִ� �̹����� ����
            life[i].sprite = live_flower;
        }
    }

    // ȭ�� �� ��� UI ������Ʈ �Լ�
    public void UpdateLife()
    {
        // �÷��̾��� �ִ� ��� �� ��ŭ �ݺ�
        for (int i = 0; i < player.max_life; i++)
        {
            // i�� ���� ��� �� ������ ���� ���̸�
            if (i < player.now_life)
            {
                // ����� �ִ� �̹����� ����
                life[i].sprite = live_flower;
            }
            // i�� ���� ��� �� ������ ũ��
            else
            {
                // ����� ���� �̹����� ����
                life[i].sprite = death_flower;
            }
        }
    }
}
