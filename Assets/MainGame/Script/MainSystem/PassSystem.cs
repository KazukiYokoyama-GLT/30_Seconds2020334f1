using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassSystem : MonoBehaviour
{
    static public int passitemid = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ItemPass(GameObject playerobj)
    {
        //Debug.Log("パス");
        //Debug.Log(playerobj.name);
        //Debug.Log(passitemid);

        if (passitemid == -1) return;

        bool passed = false;

        if (playerobj.GetComponent<User_A>())
        {
            //Debug.Log("Bに渡す");
            var item = ItemDataBase.Entity.GetData(passitemid);
            item.OwnerFlag = 2;
            passed = true;
        }
        else if (playerobj.GetComponent<User_B>())
        {
            //Debug.Log("Aに渡す");
            var item = ItemDataBase.Entity.GetData(passitemid);
            item.OwnerFlag = 1;
            passed = true;
        }
        else
        {
            return;
        }

        if (passed)
        {
            passitemid = -1;
        }
    }
}
