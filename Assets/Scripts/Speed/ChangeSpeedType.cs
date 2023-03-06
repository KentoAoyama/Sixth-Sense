/// <summary>
/// スピードの変更を行う際に指定するための列挙型
/// </summary>
public enum ChangeSpeedType
{
    All,
    NonPlayer,
    NonUI,
}

/// <summary>
/// そのスピードの値を持っているものの種類
/// </summary>
public enum SpeedType
{
    Player,
    Enemy,
    UI,
    Bullet
}
