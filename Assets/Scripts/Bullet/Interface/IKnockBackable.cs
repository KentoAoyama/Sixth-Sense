using UnityEngine;

public interface IKnockBackable
{
    //攻撃を受けた際のノックバック処理

    public void KnockBack(Collider collider, Vector3 dir);
}
