using UnityEngine;
using UnityEngine.UI;

public class PointTile : MonoBehaviour {

    public Text infoText;
    public Point point;


    void Start()
    {
        infoText = GameObject.Find("Info_Panel_Text").GetComponent<Text>();
    }

    public void OnMouseEnter()
    {
        if (point.owner != null)
        {
            infoText.text = gameObject.name + "\n" + "Owner: " + point.owner;
        }
        else {
            infoText.text = gameObject.name + "\n" + "Unoccupied";
        }
    }

    public void OnMouseExit()
    {
        infoText.text = "";
    }
}
