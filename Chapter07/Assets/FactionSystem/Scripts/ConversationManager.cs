using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour {
    [SerializeField]
    private Question[] questions;
    [Header("UI")]
    [SerializeField]
    private GameObject questionPanel;
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private Text resultText;
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Button firstAnswerButton;
    [SerializeField]
    private Button secondAnswerButton;
    [SerializeField]
    private Button thirdAnswerButton;

    [Header("Morality Gradient")]
    [SerializeField]
    private AnimationCurve good;
    [SerializeField]
    private AnimationCurve neutral;
    [SerializeField]
    private AnimationCurve evil;

    private int questionIndex = 0;
    private float answerTotal = 0;

    private void Start() 
    {
        LoadQuestion(questionIndex);        
    }

    private void LoadQuestion(int index) 
    {
        if (index < questions.Length) 
        {
            questionText.text = questions[index].questionText;
            firstAnswerButton.GetComponentInChildren<Text>().text = questions[index].answers[0].answerText;
            secondAnswerButton.GetComponentInChildren<Text>().text = questions[index].answers[1].answerText;
            thirdAnswerButton.GetComponentInChildren<Text>().text = questions[index].answers[2].answerText;
        } 
        else 
        {
            EndConversation();
        }
    }

    public void OnAnswerSubmitted(int answerIndex) 
    {
        answerTotal += questions[questionIndex].answers[answerIndex].moralityValue;
        questionIndex++;
        LoadQuestion(questionIndex);
    }

    private void EndConversation() 
    {
        questionPanel.SetActive(false);
        float average = answerTotal / questions.Length;
        float goodRating = good.Evaluate(average);
        float neutralRating = neutral.Evaluate(average);
        float evilRating = evil.Evaluate(average);
        string alignmentText = "";
        Debug.Log(answerTotal + " " + average);
        if(goodRating > neutralRating) 
        {
            if(goodRating > evilRating) 
            {
                //good wins
                alignmentText = "GOOD";
            }
            else 
            {
                //evil wins
                alignmentText = "EVIL";
            }
        }
        else 
        {
            if(neutralRating > evilRating) 
            {
                //neutral wins
                alignmentText = "NEUTRAL";
            }
            else 
            {
                //evil win
                alignmentText = "EVIL";
            }
        }
        resultPanel.SetActive(true);
        resultText.text = "Your morality alignment is: " + alignmentText;
    }
    
}
