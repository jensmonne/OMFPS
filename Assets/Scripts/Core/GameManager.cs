using TMPro;
using UnityEngine;

/// <summary>
/// It's a gamemanager... what more do you want. I would like to remove the statics (its a fucking singleton so it only brings clutter) but im to lazy to do that right now...
/// I'll just add a to-do and hope that it magically fixes itself :D
/// </summary>
// TODO: Remove the statics in this script.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private NetworkedBomb _networkedBomb;

    public enum GameState
    {
        StartMenu,
        Playing,
        GameOver,
        GameWon
    }
    public GameState CurrentState { get; set; }

    public static bool IsBombActive { get; set; }
    
    [SerializeField] private float bombDuration = 10f;

    [SerializeField] private TMP_Text wristBandText;
    
    public static float bombTimer { get; private set; }

    public float RemainingTime => Mathf.Max(0f, bombDuration - bombTimer);

    public float Magcount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        CurrentState = GameState.StartMenu;
    }

    private void Update()
    {
        if (_networkedBomb)
        {
            _networkedBomb = FindFirstObjectByType<NetworkedBomb>();
        }
        if (!IsBombActive) return;
        
        bombTimer += Time.deltaTime * 6;
            
        if (wristBandText) wristBandText.text = RemainingTime.ToString("F2");

        if (bombTimer < bombDuration) return;
            
        BigKaboom();
    }

    private void BigKaboom()
    {
        // implement more shit here when bomb goes BOOM
        BigKaboomSnap.ActivateBlast();
        _networkedBomb.RequestBlast();
    }

    public void AddMag()
    {
        Magcount += 1f;
    }
}
