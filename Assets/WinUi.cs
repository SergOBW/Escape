using UnityEngine;
using UnityEngine.UI;

public class WinUi : MonoBehaviour
{
    [SerializeField] private Text text;

    private void OnEnable()
    {
        text.text = GameManager.Instance.GetLevelGoal();
    }
}
