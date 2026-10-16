using UnityEngine;

public class DamageNode : TreeNode
{
    public float damageIncrease;
    public override void Activate()
    {
        base.Activate();

        GameManager.Player.GetComponent<LaserShooter>().BaseDamage += damageIncrease;
    }
}
