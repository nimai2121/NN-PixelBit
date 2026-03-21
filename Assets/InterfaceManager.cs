using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    //public Image seekImage;
    //public GameObject npc;
    //public GameObject randomSpawn;

    //public Image collectible;
    //public GameObject showSprite;

    //[SerializeField]
    //private Sprite[] collectibleSource;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        //showSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Submit") && dialogueBox.activeInHierarchy)
        {
            dialogueBox.SetActive(false);
        }
    }


   /* public void ShowBox(string dialogue, int item)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
        seekImage.GetComponent<Image>().sprite = collectibleSource[item];

        if (npc.GetComponent<DialogueOpen>().begin)
        {
            scatterCoins();
        }
    }

    public void scatterCoins()
    {
        randomSpawn.GetComponent<RandomSpawn>().DistributeCollectibles();
        npc.GetComponent<DialogueOpen>().coinsScattered();
    }*/
}
