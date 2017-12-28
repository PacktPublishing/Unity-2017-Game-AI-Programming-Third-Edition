using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MathTree : MonoBehaviour {
    public Color m_evaluating;
    public Color m_succeeded;
    public Color m_failed;

    public Selector m_rootNode;

    public ActionNode m_node2A;
    public Inverter m_node2B;
    public ActionNode m_node2C;
    public ActionNode m_node3;

    public GameObject m_rootNodeBox;
    public GameObject m_node2aBox;
    public GameObject m_node2bBox;
    public GameObject m_node2cBox;
    public GameObject m_node3Box;

    public  int m_targetValue = 20;
    private int m_currentValue = 0;

    [SerializeField]
    private Text m_valueLabel;

    /* We instantiate our nodes from the bottom up, and assign the children
     * in that order */
	void Start () {
        /** The deepest-level node is Node 3, which has no children. */
        m_node3 = new ActionNode(NotEqualToTarget);

        /** Next up, we create the level 2 nodes. */
        m_node2A = new ActionNode(AddTen);

        /** Node 2B is a selector which has node 3 as a child, so we'll pass 
         * node 3 to the constructor */
        m_node2B = new Inverter(m_node3);

        m_node2C = new ActionNode(AddTen);

        /** Lastly, we have our root node. First, we prepare our list of children
         * nodes to pass in */
        List<Node> rootChildren = new List<Node>();
        rootChildren.Add(m_node2A);
        rootChildren.Add(m_node2B);
        rootChildren.Add(m_node2C);

        /** Then we create our root node object and pass in the list */
        m_rootNode = new Selector(rootChildren);

        m_valueLabel.text = m_currentValue.ToString();

        m_rootNode.Evaluate();

        UpdateBoxes();
	}

    private void UpdateBoxes() {
        /** Update root node box */
        if (m_rootNode.nodeState == NodeStates.SUCCESS) {
            SetSucceeded(m_rootNodeBox);
        } else if (m_rootNode.nodeState == NodeStates.FAILURE) {
            SetFailed(m_rootNodeBox);
        }

        /** Update 2A node box */
        if (m_node2A.nodeState == NodeStates.SUCCESS) {
            SetSucceeded(m_node2aBox);
        } else if (m_node2A.nodeState == NodeStates.FAILURE) {
            SetFailed(m_node2aBox);
        }

        /** Update 2B node box */
        if (m_node2B.nodeState == NodeStates.SUCCESS) {
            SetSucceeded(m_node2bBox);
        } else if (m_node2B.nodeState == NodeStates.FAILURE) {
            SetFailed(m_node2bBox);
        }

        /** Update 2C node box */
        if (m_node2C.nodeState == NodeStates.SUCCESS) {
            SetSucceeded(m_node2cBox);
        } else if (m_node2C.nodeState == NodeStates.FAILURE) {
            SetFailed(m_node2cBox);
        }

        /** Update 3 node box */
        if (m_node3.nodeState == NodeStates.SUCCESS) {
            SetSucceeded(m_node3Box);
        } else if (m_node3.nodeState == NodeStates.FAILURE) {
            SetFailed(m_node3Box);
        }
    }

    private NodeStates NotEqualToTarget() {        
        if (m_currentValue != m_targetValue) {
            return NodeStates.SUCCESS;
        } else {
            return NodeStates.FAILURE;
        }
    }

    private NodeStates AddTen() {
        m_currentValue += 10;
        m_valueLabel.text = m_currentValue.ToString();
        if (m_currentValue == m_targetValue) {
            return NodeStates.SUCCESS;
        } else {
            return NodeStates.FAILURE;
        }
    }

    private void SetEvaluating(GameObject box) {
        box.GetComponent<Renderer>().material.color = m_evaluating;  
    }

    private void SetSucceeded(GameObject box) {
        box.GetComponent<Renderer>().material.color = m_succeeded;
    }

    private void SetFailed(GameObject box) {
        box.GetComponent<Renderer>().material.color = m_failed;
    }

    public void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
