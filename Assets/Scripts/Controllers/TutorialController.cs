using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    PlayerController playerController_script;

    [Header("Reading Tutorial")]
    [SerializeField] private RectTransform tutorialTextBackground_rectTrans = null;
    [SerializeField] private Text tutorialText_text = null;

    [Header("Notification Tutorial")]
    [SerializeField] private GameObject notifTutorial_GO = null;
    [SerializeField] private RectTransform notifTextBackground_rectTrans = null;
    [SerializeField] private Text notifHeadline = null;
    [SerializeField] private Text notifTutorialText_text = null;

    [Header("Reference RectTransforms")]
    [SerializeField] private RectTransform defaultTutorialTextBackground_rectTrans = null;

    private float timer1 = 0, timer2 = 0;

    private void Awake()
    {
        playerController_script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_script.enabled = false;

        StartCoroutine(DisplayTutorial());
    }

    private void ToggleReadingTutorial(bool value)
    {
        tutorialTextBackground_rectTrans.gameObject.SetActive(value);
    }

    private void ToggleNotificationTutorial(bool value)
    {
        notifTutorial_GO.gameObject.SetActive(value);
    }

    private void UpdateReadingText(string newText)
    {
        tutorialText_text.text = newText;
    }

    private void UpdateNotificationText(string headline, string newText)
    {
        notifHeadline.text = headline;
        notifTutorialText_text.text = newText;
    }

    private void UpdateHUDDimensions(RectTransform newRectTrans)
    {
        tutorialTextBackground_rectTrans.position = newRectTrans.position;
        tutorialTextBackground_rectTrans.sizeDelta = newRectTrans.sizeDelta;
    }

    private bool MovementTutorial()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0)
        {
            timer1 += Time.unscaledDeltaTime;
        }
        else if(h < 0)
        {
            timer2 += Time.unscaledDeltaTime;
        }

        if(timer1 >= 0.5f && timer2 >= 0.5f)
        {
            return true;
        }

        return false;
    }

    private bool JumpTutorial()
    {
        float v = Input.GetAxisRaw("Jump");

        if (v != 0) 
        {
            return true;
        }

        return false;
    }

    private void StopPlayerMovement()
    {
        playerController_script.enabled = false;

        Rigidbody2D playerRB = playerController_script.GetComponent<Rigidbody2D>();

        Vector2 finalVector = playerRB.velocity;
        finalVector.x = 0;

        playerRB.velocity = finalVector;
    }

    private IEnumerator DisplayTutorial()
    {
        ToggleNotificationTutorial(false);
        UpdateHUDDimensions(defaultTutorialTextBackground_rectTrans);
        UpdateReadingText("Welcome! Thanks for testing out our game!");
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        yield return new WaitForEndOfFrame();
        UpdateReadingText("You can press the 'A' and 'D' keys to move left and right. Try it out!");
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        playerController_script.enabled = true;

        yield return new WaitForEndOfFrame();
        ToggleReadingTutorial(false);
        UpdateNotificationText("Movement", "Move left and right with 'A' and 'D'");
        ToggleNotificationTutorial(true);

        //yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => MovementTutorial());

        StopPlayerMovement();
        UpdateReadingText("Good job!");
        ToggleNotificationTutorial(false);
        ToggleReadingTutorial(true);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        yield return new WaitForEndOfFrame();
        UpdateReadingText("You can also jump with the 'W' or 'Space' key!");
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        playerController_script.enabled = true;

        yield return new WaitForEndOfFrame();
        ToggleReadingTutorial(false);
        UpdateNotificationText("Jumping", "Jump using the 'W' or 'Space' key.");
        ToggleNotificationTutorial(true);


        yield return new WaitUntil(() => JumpTutorial());

        StopPlayerMovement();
        UpdateReadingText("Nice!");
        ToggleNotificationTutorial(false);
        ToggleReadingTutorial(true);


        //DELETE BELOW LATER
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        ToggleReadingTutorial(false);
        playerController_script.enabled = true;
        yield return new WaitForEndOfFrame();
    }
}
