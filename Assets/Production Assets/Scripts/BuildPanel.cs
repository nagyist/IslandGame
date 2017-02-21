using UnityEngine;
using UnityEngine.UI;

public class BuildPanel : MonoBehaviour
{
    public Button yourButton;
    public delegate void BuildFunction();
    public BuildFunction buildfunction;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        buildfunction();
        Destroy(this.gameObject);
    }
}