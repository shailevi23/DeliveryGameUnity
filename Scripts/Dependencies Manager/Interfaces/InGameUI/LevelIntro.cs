using System.Collections;
using TMPro;
using UnityEngine;

public class LevelIntro : MonoBehaviour, ILevelIntro
{
    [SerializeField]
    public TextMeshProUGUI levelIntroText;

    public object LevelIntroText { get; private set; }
    private Coroutine levelIntroCoroutine;

    void Awake()
    {
        LevelIntroText = levelIntroText;
        GameManager.Instance.GameStateChanged += OnGameStateChanged;
    }

    public void SetActive()
    {
        if (LevelIntroText is TextMeshProUGUI introText)
        {
            SetTextActive(introText);
        }
        else
        {
            Debug.LogError("Invalid parameters for StartCountdown");
        }
    }

    public void StartCountdown()
    {
        if (LevelIntroText is TextMeshProUGUI introText)
        {
            if (levelIntroCoroutine != null)
                StopCoroutine(levelIntroCoroutine);
            levelIntroCoroutine = StartCoroutine(LevelIntroCoroutine(introText));
        }
        else
        {
            Debug.LogError("Invalid parameters for StartCountdown");
        }
    }

    public void StopCountdown()
    {
        if (levelIntroCoroutine != null)
        {
            StopCoroutine(levelIntroCoroutine);
            SoundManager.Instance.StopSound("Countdown");
            levelIntroCoroutine = null;
        }
    }

    private IEnumerator LevelIntroCoroutine(TextMeshProUGUI levelIntroText)
    {
        SoundManager.Instance.PlaySound("Countdown");
        levelIntroText.color = Color.black;
        levelIntroText.text = "3";
        yield return new WaitForSeconds(1f);
        levelIntroText.color = Color.red;
        levelIntroText.text = "2";
        yield return new WaitForSeconds(1f);
        levelIntroText.color = Color.yellow;
        levelIntroText.text = "1";
        yield return new WaitForSeconds(1f);
        levelIntroText.color = Color.green;
        levelIntroText.text = "GO";
        GameManager.Instance.SetGameState(GameState.Gameplay);
        yield return new WaitForSeconds(0.7f);
        levelIntroText.text = "";
        levelIntroText.gameObject.SetActive(false);
        levelIntroCoroutine = null;
    }

    private void SetTextActive(TextMeshProUGUI levelIntroText)
    {
        levelIntroText.gameObject.SetActive(true);
    }

    private void OnGameStateChanged()
    {
        if (GameManager.Instance.CurrentState == GameState.Settings)
        {
            StopCountdown();
        }
        else if (GameManager.Instance.CurrentState == GameState.LevelIntro)
        {
            if (GameManager.Instance.PreviousState == GameState.Settings)
            {
                StartCountdown();
            }
            else
            {
                SetActive();
                StartCountdown();
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.GameStateChanged -= OnGameStateChanged;
    }
}