using UnityEngine;
using UnityEngine.UI;

public class LetterUi : MonoBehaviour
{
    public char m_letter;  
    public void Setup(char value)
    {
        GetComponent<Text>().text = value.ToString();
        SetActive(false,new Color(0,0,0,0));
        m_letter = value;
    }
    
    public void SetActive(bool flag,Color color)
    {
        var text = GetComponent<Text>();
        var nextColor = text.color;
        switch (flag)
        {
            case true:
                nextColor = color;
                GetComponent<Text>().color = nextColor;
                break;
            case false:
                GetComponent<Text>().color = nextColor;
                break;
        }
    }
    
}
