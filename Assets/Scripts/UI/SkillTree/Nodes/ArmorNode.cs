using UnityEngine;

public class ArmorNode : TreeNode
{
    public float armorIncrease = 10;
    public override void Activate()
    {
        base.Activate();

        GameManager.Player.armor += armorIncrease;
    }
}
