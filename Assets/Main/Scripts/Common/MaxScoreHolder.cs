using Main.Containers;
using Main.Scripts.EventSystem;
using Main.Scripts.Events;
using Main.Scripts.SaveSystem;

public class MaxScoreHolder :  ISaveable
{
    private PlayerEconomy _economy;
    private SaveSystem _saveSystem;


    private MaxScore _maxScore;
    public string Key => "Player_Max_Score";
    public int Value => _maxScore.Val;
    
    public MaxScoreHolder(
            PlayerEconomy economy,
            SaveSystem saveSystem
            )
    {
        _economy = economy;
        _saveSystem = saveSystem;

        _maxScore ??= new MaxScore();
        saveSystem.Subscribe(this);
        Dispatcher.Subscribe<GameSuccessEvent>(HandleGameSuccess);
    }


    private void HandleGameSuccess(GameSuccessEvent @event)
    {
        var curr = _economy.Current;
        if (_maxScore.Val < curr)
            _maxScore.Val = curr;

        _saveSystem.SaveGame();
    }

    void Dispose()
    {
        _saveSystem.Unsubscribe(this);
        Dispatcher.Unsubscribe<GameSuccessEvent>(HandleGameSuccess);
    }

    public SaveContainer CreateSnapshot()
    {
        var data = _maxScore.Serialize();
        return new SaveContainer(Key, data);
    }

    public void Load(string data)
    {
        _maxScore = new MaxScore();
        _maxScore.Deserialize(data);
    }
}
