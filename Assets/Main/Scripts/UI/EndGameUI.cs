using Anim.UnityBindings;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using Main.Containers;
using Main.Scripts.SceneSystem;

public class EngGameUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Image img;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button mainMenuButton;

    [SerializeField] private TextMeshProUGUI currScore;
    [SerializeField] private TextMeshProUGUI maxScore;
    [SerializeField] private TextMeshProUGUI title;

    [Inject] private MaxScoreHolder _scoreHolder;
    [Inject] private PlayerEconomy _economy;
    [Inject] private SceneLoader _sceneLoader;

    private bool _isActive;

    void OnEnable()
    {
        Dispatcher.Subscribe<GameFailEvent>(HandleFail);
        Dispatcher.Subscribe<GameSuccessEvent>(HandleSuccess);
    }

    void OnDisable()
    {
        Dispatcher.Unsubscribe<GameFailEvent>(HandleFail);
        Dispatcher.Unsubscribe<GameSuccessEvent>(HandleSuccess);
    }

    void Start()
    {
        replayButton.onClick.AddListener(HandleRestartClick);
        mainMenuButton.onClick.AddListener(HandleMainMenu);
    }

    private void HandleMainMenu()
    {
        _sceneLoader.GoMain();
    }

    private void HandleRestartClick()
    {
        if (!_isActive)
            return;

        _isActive = false;

        img.FadeAnim(0f, 0.2f).Start();
        group.FadeAnim(0f, .2f).Start();

        group.gameObject.SetActive(false);
        Dispatcher.Dispatch<GameRestartEvent>(new GameRestartEvent());
    }

    private void HandleSuccess(GameSuccessEvent @event)
    {
        _isActive = true;
        img.FadeAnim(0.3f, 0.2f).Start();
        group.FadeAnim(1f, .3f, 0.2f).Start();
        group.gameObject.SetActive(true);

        currScore.SetText($"Your Score: {_economy.Current}");
        maxScore.SetText($"Max Score: {_scoreHolder.Value}");
        title.SetText("Congratz!!!");
    }

    private void HandleFail(GameFailEvent @event)
    {
        _isActive = true;

        group.gameObject.SetActive(true);
        img.FadeAnim(0.3f, 0.2f).Start();
        group.FadeAnim(1f, .3f, 0.2f).Start();
        currScore.SetText($"Your Score: {_economy.Current}");
        maxScore.SetText($"Max Score: {_scoreHolder.Value}");
        title.SetText("Failed");
    }
}
