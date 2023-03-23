using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonClicker : MonoBehaviour
{
    UIDocument buttonDocument;
    Button uiButton;

    private void OnEnable()
    {
        buttonDocument = GetComponent<UIDocument>();

        if (buttonDocument == null)
        {
            Debug.LogError("No button tho");
        }

        uiButton = buttonDocument.rootVisualElement.Q("TestButton") as Button;

        if (uiButton != null)
        {
            Debug.Log("Button found!");
        }

        uiButton.RegisterCallback<ClickEvent>(OnButtonClick);
    }

    public void OnButtonClick(ClickEvent evt)
    {
        Debug.Log("The UI button was clicked");
    }    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
