using UnityEngine;

public class Game : MonoBehaviour {
    [SerializeField]
    private Animator stateMachine;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private EnemyBehaviorTree enemyBehaviorTree;
    [SerializeField]
    private Player humanPlayer;
    [SerializeField]
    private Player aiPlayer;
    [SerializeField]
    private UIController uiController;
    private int turn = 0;

    private void Awake() {
        enemyBehaviorTree.SetPlayerData(humanPlayer, aiPlayer);
        enemyBehaviorTree.onTreeExecuted += EndTurn;
        playerController.onActionExecuted += EndTurn;
    }

    public void EvaluateAITree() {
        enemyBehaviorTree.Evaluate();        
    }

    private void EndTurn() {
        if(humanPlayer.CurrentHealth <= 0 || aiPlayer.CurrentHealth <= 0) {
            stateMachine.SetTrigger("EndGame");
            uiController.EndGame();
            return;
        }
        stateMachine.SetTrigger("EndTurn");
        turn ^= 1;
        uiController.SetTurn(turn);
    }
}
