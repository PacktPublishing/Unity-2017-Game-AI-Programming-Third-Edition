using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviorTree : MonoBehaviour {
    
    private Player playerData;    
    private Player ownData;

    public RandomBinaryNode buffCheckRandomNode;
    public ActionNode buffCheckNode;
    public ActionNode healthCheckNode;
    public ActionNode attackCheckNode;
    public Sequence buffCheckSequence;
    public Selector rootNode;

    public delegate void TreeExecuted();
    public event TreeExecuted onTreeExecuted;

    public delegate void NodePassed(string trigger);
    
	void Start () {        
        /* First, the AI checks its own health. If it's low, it will want to heal */
        healthCheckNode = new ActionNode(CriticalHealthCheck);

        /* Next, the AI will check the player's heatlth. If it's low, it will always
         * go for the kill */
        attackCheckNode = new ActionNode(CheckPlayerHealth);

        /* If neither the player nor the AI are low in health, the AI will 
         * prioritize using a defensive buff. To avoid having the AI buff every turn, 
         we use a binary randomizer to only do it half the time. */
        buffCheckRandomNode = new RandomBinaryNode();
        buffCheckNode = new ActionNode(BuffCheck);
        buffCheckSequence = new Sequence(new List<Node> {
            buffCheckRandomNode,
            buffCheckNode,
        });

        rootNode = new Selector(new List<Node> {
            healthCheckNode,
            attackCheckNode,
            buffCheckSequence,
        });
	}

    public void SetPlayerData(Player human, Player ai) {
        playerData = human;
        ownData = ai;
    }
	
	public void Evaluate() {
        rootNode.Evaluate();
        StartCoroutine(Execute());
    }

    private IEnumerator Execute() {
        Debug.Log("The AI is thinking...");
        yield return new WaitForSeconds(2.5f);

        if(healthCheckNode.nodeState == NodeStates.SUCCESS) {
            Debug.Log("The AI decided to heal itself");
            ownData.Heal();
        } else if(attackCheckNode.nodeState == NodeStates.SUCCESS) {
            Debug.Log("The AI decided to attack the player!");
            playerData.Damage();
        } else if (buffCheckSequence.nodeState == NodeStates.SUCCESS) {
            Debug.Log("The AI decided to defend itself");
            ownData.Buff();
        } else {
            Debug.Log("The AI finally decided to attack the player");
            playerData.Damage();
        }
        if(onTreeExecuted != null) {
            onTreeExecuted();
        }
    }


    private NodeStates CriticalHealthCheck() {
        if(ownData.HasLowHealth) {
            return NodeStates.SUCCESS;
        } else {
            return NodeStates.FAILURE;
        }
    }

    private NodeStates CheckPlayerHealth() {
        if(playerData.HasLowHealth) {
            return NodeStates.SUCCESS;
        } else {
            return NodeStates.FAILURE;
        }
    }

    private NodeStates BuffCheck() {
        if(!ownData.IsBuffed) {
            return NodeStates.SUCCESS;
        } else {
            return NodeStates.FAILURE;
        }
    }
}
