using UnityEngine;

[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("# Data")]
    // 추후 JSON 파싱 방식으로 변경
    [SerializeField] private MusicData _musicData;
    public MusicData MusicData => _musicData;

    [Header("# Components")]
    [SerializeField] private AudioSource _audioSource;

    private double _musicStartTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // 임시 시작
        MusicStart();
    }

    public double GetCurrentTime()
    {
        return AudioSettings.dspTime - _musicStartTime;
    }

    private void MusicStart()
    {
        _musicStartTime = AudioSettings.dspTime + 0.1;
        _audioSource.PlayScheduled(_musicStartTime);
    }
}
