using UnityEngine;
using UnityEngine.UI;

public class LetterUi : MonoBehaviour
{
    public char m_letter;  
    public void Setup(char value)
    {
        GetComponent<Text>().text = value.ToString();
        SetActive(false);
        m_letter = value;
    }
    
    public void SetActive(bool flag)
    {
        var text = GetComponent<Text>();
        var color = text.color;
        switch (flag)
        {
            case true:
                color.a = 1f;
                GetComponent<Text>().color = color;
                break;
            case false:
                GetComponent<Text>().color = color;
                break;
        }
    }
    
}
