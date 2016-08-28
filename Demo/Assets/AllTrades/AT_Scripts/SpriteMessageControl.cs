using UnityEngine;
using System.Collections;

public class SpriteMessageControl : MonoBehaviour 
{
	public string message = "";
	public string pathToChars = "";
	public float charSpacing = 1f;
	public bool alignLeft = false;

	private GameObject letter;
	private SpriteRenderer SR;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void SetMessage(string inputString)
	{
        inputString = inputString.ToUpper();

		// Delete previous message
		foreach(Transform child in this.transform)
		{
			Destroy(child.gameObject);
		}

		// Create new message
		if(alignLeft)
		{
			for(int ii=0; ii<inputString.Length; ii++)
			{
				if(inputString[ii]!=' ')
				{
					letter = new GameObject();
					letter.transform.parent = this.transform;
					letter.transform.localPosition = new Vector3(charSpacing*(float)ii, 0f, 0f);
					letter.transform.localScale = Vector3.one;
                    letter.transform.localRotation = Quaternion.identity;
                    SR = letter.AddComponent<SpriteRenderer>();
					switch(inputString[ii])
					{
                        case '!':
                            SR.sprite = Resources.Load<Sprite>(pathToChars + "exclamation");
                            break;
                        case '.':
                            SR.sprite = Resources.Load<Sprite>(pathToChars + "period");
                            break;
                        case ':':
						    SR.sprite = Resources.Load<Sprite>(pathToChars+"semicolon");
						    break;
					    default:
						    SR.sprite = Resources.Load<Sprite>(pathToChars+inputString[ii]);
						    break;
					}
				}
			}
		}
		else
		{
			for(int ii=inputString.Length-1; ii>=0; ii--)
			{
				if(inputString[ii]!=' ')
				{
					letter = new GameObject();
					letter.transform.parent = this.transform;
					letter.transform.localPosition = new Vector3(-charSpacing*(float)ii, 0f, 0f);
					letter.transform.localScale = Vector3.one;
					SR = letter.AddComponent<SpriteRenderer>();
					SR.sprite = Resources.Load<Sprite>(pathToChars+inputString[ii]);
				}
			}
		}

	}


}
