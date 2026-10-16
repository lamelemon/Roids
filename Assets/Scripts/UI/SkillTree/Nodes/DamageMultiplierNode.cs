using UnityEngine;

public class DamageMultiplierNode : TreeNode
{
    public float multiplierIncrease = 0.1f;
    public override void Activate()
    {
        base.Activate();

        GameManager.Player.GetComponent<LaserShooter>().DamageMultiplier += multiplierIncrease;
    }
}
