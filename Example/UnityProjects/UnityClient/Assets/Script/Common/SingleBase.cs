using UnityEngine;

public class SingleBase<T>:MonoBehaviour where T : new()
{
    private static T single;
    public static T Single
    {
        get
        {
            if (single == null)
            {
                single = GameObject.Find("Canvas").GetComponent<T>();

            }
            return single;
        }
    }
}