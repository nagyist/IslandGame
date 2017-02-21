using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Destroy(this.gameObject);
    }
}