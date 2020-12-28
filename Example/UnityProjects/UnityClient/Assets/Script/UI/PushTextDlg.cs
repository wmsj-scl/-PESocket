using System;
using System.Collections.Generic;
using Protocol;
using UnityEngine;
using UnityEngine.UI;

public class PushTextDlg : MonoBehaviour
{
    private const string pushAnimatorName = "PushText";

    private Queue<string> queue = new Queue<string>();

    public Animator animator;
    public Text text;

    private void Start()
    {
        var clips = animator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clips.Length; i++)
        {
            var clip = clips[i];
            AnimationEvent evt = new AnimationEvent();
            evt.functionName = "OnAnimatorPlayOver";
            evt.stringParameter = clip.name;
            evt.time = clip.length;
            clip.AddEvent(evt);
        }
    }

    public void OnAnimatorPlayOver(string clipName)
    {
        gameObject.SetActive(false);
        Show();
    }

    public void ShowText(string str, params string[] data)
    {
        ShowText(string.Format(str,data));
    }

    public void ShowText(string str)
    {
        queue.Enqueue(str);
        Show();
    }

    private void Show()
    {
        if (queue.Count != 0)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            else
            {
                return;
            }


            if (!animator.enabled)
            {
                animator.enabled = true;
            }
            text.text = queue.Dequeue();

            animator.Play(pushAnimatorName);
        }
        else
        {
            gameObject.SetActive(false);
        }
   
    }

    public void ShowText(ErrorCode errorCode)
    {
        ShowText(ErrorStr.GetErrorStr(errorCode));
    }
}
