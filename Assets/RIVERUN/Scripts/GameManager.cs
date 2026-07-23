using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    public Transform player;

    [Header("UI")]
    public TMP_Text gemsText;
    public TMP_Text distanceText;

    [HideInInspector]
    public int gemsCollected = 0;

    private float startZ;
    private float distance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (player != null)
            startZ = player.position.z;
    }

    private void Update()
    {
        if (player == null)
            return;

        distance = Mathf.Max(0, player.position.z - startZ);

        if (distanceText != null)
            distanceText.text = (distance / 1000f).ToString("F2") + " km";

        if (gemsText != null)
            gemsText.text = gemsCollected.ToString();
    }

    public void AddGem(int amount)
    {
        gemsCollected += amount;
    }

    public float GetDistance()
    {
        return distance;
    }
}