using UnityEngine;

public class RandomBinaryNode : Node {
    public override NodeStates Evaluate() {
        var roll = Random.Range(0, 2);
        return (roll == 0 ? NodeStates.SUCCESS : NodeStates.FAILURE);
    }
}
