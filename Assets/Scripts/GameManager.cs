using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int cash = 750;
    public TextMeshProUGUI cashText;

    private void Start()
    {
        Instance = this;
    }
    void Update()
    {
        cashText.text = "CASH: £" + cash;
    }

    //Temporary method to add cash for testing
    public void AddCash()
    {
        cash += 100;
    }
}
