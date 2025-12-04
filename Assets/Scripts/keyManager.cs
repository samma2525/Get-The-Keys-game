using UnityEngine;
using TMPro;

public class keyManager : MonoBehaviour
{
    public int keyCount = 0;
    public TextMeshProUGUI keyText;
    public GameObject door;
    public int requiredKeys;
    private bool doorDestroyed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = keyCount.ToString();
        if(keyCount >= requiredKeys && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(door);
        }
    }
}
