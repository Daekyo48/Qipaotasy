using UnityEngine;

[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("# Data")]
    [SerializeField] private SettingData _settingData;
    public SettingData SettingData => _settingData;

    [SerializeField] private MusicData _musicData;  // 추후 JSON 파싱 방식으로 변경
    public MusicData MusicData => _musicData;

    private ScoreData _scoreData;
    public ScoreData ScoreData => _scoreData;

    public double CurrentTime
    {
        get => AudioSettings.dspTime - _musicStartTime;
    }

    private AudioSource _audioSource;

    private double _musicStartTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.Stop();
    }

    private void Start()
    {
        // 임시 시작
        MusicStart();
    }

    private void MusicStart()
    {
        SceneEffector.Instance.FadeIn(2f);

        _musicStartTime = AudioSettings.dspTime + 0.1;
        _audioSource.PlayScheduled(_musicStartTime);
    }
}
