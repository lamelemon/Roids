using UnityEngine;

public class MultiShotNode : TreeNode
{
    public float Increase = 0.3f;
    public override void Activate()
    {
        base.Activate();

        GameManager.Player.GetComponent<LaserShooter>().multiShot += Increase;
    }
}
