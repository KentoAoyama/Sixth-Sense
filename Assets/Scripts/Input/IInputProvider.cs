using UnityEngine;

public interface IInputProvider
{
    /// <summary>
    /// 移動方向の入力処理
    /// </summary>
    /// <returns>移動の方向</returns>
    Vector2 GetMoveDir();

    /// <summary>
    /// 攻撃の入力処理
    /// </summary>
    /// <returns>攻撃の入力判定</returns>
    bool GetFire();

    /// <summary>
    /// 目を閉じる入力処理
    /// </summary>
    /// <returns>目を閉じる入力判定</returns>
    bool GetCloseEye();

    /// <summary>
    /// エスケープ入力
    /// </summary>
    /// <returns>エスケープの入力判定</returns>
    bool GetEscape();
}
