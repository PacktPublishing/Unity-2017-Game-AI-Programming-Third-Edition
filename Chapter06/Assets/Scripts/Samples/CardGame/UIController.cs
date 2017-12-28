using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    private const string playerTurnMessage = "Your turn";
    private const string aiTurnMessage = "Enemy's turn";
    private const string gameOverMessage = "GAME OVER";

    [SerializeField]
    private Player humanPlayer;
    [SerializeField]
    private Player aiPlayer;
    [SerializeField]
    private Text turnText;
    [SerializeField]
    private Text playerHealthText;
    [SerializeField]
    private Text enemyHealthText;
    [SerializeField]
    private GameObject playerControls;

    private void FixedUpdate() {
        playerHealthText.text = "Hit points " + humanPlayer.CurrentHealth.ToString();
        enemyHealthText.text = "Hit points " + aiPlayer.CurrentHealth.ToString();
    }

    public void EndGame() {
        turnText.text = gameOverMessage;
    }

    /* We change the controls and turn message dpending on whose turn it is currently */
    public void SetTurn(int turnNumber) {
        if(turnNumber == 0) {
            turnText.text = playerTurnMessage;
            SetPlayerControlState(true);
        } else {
            turnText.text = aiTurnMessage;
            SetPlayerControlState(false);
        }
    }

    /* We disable/enable the player controls based on whose turn it is currently */
    private void SetPlayerControlState(bool active) {
        playerControls.SetActive(active);
    }
}
