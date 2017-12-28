using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private Player ownData;
    [SerializeField]
    private Player enemyData;

    [Header("Buttons")]
    [SerializeField]
    private Button defendButton;
    [SerializeField]
    private Button healButton;
    [SerializeField]
    private Button attackButton;

    public delegate void ActionExecuted();
    public event ActionExecuted onActionExecuted;

	void Awake () {
        defendButton.onClick.AddListener(Defend);
        healButton.onClick.AddListener(Heal);
        attackButton.onClick.AddListener(Attack);
	}

    private void Attack() {
        enemyData.Damage();
        EndTurn();
    }

    private void Heal() {
        ownData.Heal();
        EndTurn();
    }
    
    private void Defend() {
        ownData.Buff();
        EndTurn();
    }

    private void EndTurn() {
        if(onActionExecuted != null) {
            onActionExecuted();
        }
    }
}
