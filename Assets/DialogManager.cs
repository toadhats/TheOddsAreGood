using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    // Set this file to your compiled json asset
    private TextAsset inkAsset;

    // The ink story that we're wrapping
    private Story _inkStory;

    // Whether the story should advance on update
    private bool storyNeeded;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private float elementPadding;

    [SerializeField]
	private UnityEngine.UI.Text text;

	[SerializeField]
	private UnityEngine.UI.Button button;


	void Awake () {
        _inkStory = new Story(inkAsset.text);
		storyNeeded = true;
	}
    
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (storyNeeded == true)
        {
            RemoveChildren();

            float offset = 0;
            while (_inkStory.canContinue)
            {
                UnityEngine.UI.Text storyText = Instantiate(text) as UnityEngine.UI.Text;
                storyText.text = _inkStory.Continue();
                storyText.transform.SetParent(canvas.transform, false);
                storyText.transform.Translate(new Vector2(0, offset));
                offset -= (storyText.fontSize + elementPadding);
            }

            if (_inkStory.currentChoices.Count > 0)
            {
                for (int ii = 0; ii < _inkStory.currentChoices.Count; ++ii)
                {
                    UnityEngine.UI.Button choice = Instantiate(button) as UnityEngine.UI.Button;
                    choice.transform.SetParent(canvas.transform, false);
                    choice.transform.Translate(new Vector2(0, offset));

                    UnityEngine.UI.Text choiceText = choice.GetComponentInChildren<UnityEngine.UI.Text>();
                    choiceText.text = _inkStory.currentChoices[ii].text;

                    UnityEngine.UI.HorizontalLayoutGroup layoutGroup = choice.GetComponent<UnityEngine.UI.HorizontalLayoutGroup>();

                    int choiceId = ii;
                    choice.onClick.AddListener(delegate { ChoiceSelected(choiceId); });

                    offset -= (choiceText.fontSize + layoutGroup.padding.top + layoutGroup.padding.bottom + elementPadding);
                }
            }

            storyNeeded = false;
        }
    }

    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    public void ChoiceSelected(int id)
    {
        _inkStory.ChooseChoiceIndex(id);
        storyNeeded = true;
    }

}
