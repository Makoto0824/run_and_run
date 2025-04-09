using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{

    public static ObjectPooling instance;

    #region Prefab Ref
    public GameObject normal;       //ref to normal prefab
    public GameObject fourPieces;   //ref to fourPieces prefab
    public GameObject twoPieces;    //ref to twoPieces prefab
    public GameObject space;        //ref to space prefab
    public GameObject largeSpace;   //ref to largeSpace prefab
    public GameObject raised;       //ref to raised prefab
    public GameObject leftRaised;   //ref to leftRaised prefab
    public GameObject rightRaised;  //ref to rightRaised prefab
    public GameObject specialJump;  //ref to specialJump prefab
    #endregion

    public int count = 3; //total clones of each object to be spawned

    #region List

    List<GameObject> NormalList = new List<GameObject>();         //list to add them
    List<GameObject> FourPiecesList = new List<GameObject>();     //list to add them
    List<GameObject> TwoPiecesList = new List<GameObject>();      //list to add them
    List<GameObject> SpaceList = new List<GameObject>();          //list to add them
    List<GameObject> LargeSpaceList = new List<GameObject>();     //list to add them
    List<GameObject> RaisedList = new List<GameObject>();         //list to add them
    List<GameObject> LeftRaisedList = new List<GameObject>();     //list to add them
    List<GameObject> RightRaisedList = new List<GameObject>();    //list to add them
    List<GameObject> SpecialJumpList = new List<GameObject>();    //list to add them
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        #region Start Spawn

        //fourPiecess
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(fourPieces);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            FourPiecesList.Add(obj);
        }

        //twoPiecess
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(twoPieces);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            TwoPiecesList.Add(obj);
        }

        //space
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(space);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpaceList.Add(obj);
        }
        //largeSpace
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(largeSpace);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            LargeSpaceList.Add(obj);
        }
        //raised
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(raised);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            RaisedList.Add(obj);
        }
        //normals
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(normal);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            NormalList.Add(obj);
        }
        //leftRaised
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(leftRaised);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            LeftRaisedList.Add(obj);
        }
        //rightRaised
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(rightRaised);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            RightRaisedList.Add(obj);
        }
        //specialJump
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(specialJump);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpecialJumpList.Add(obj);
        }

        #endregion
    }

    //method which is used to call from other scripts to get the clone object

    //FourPieces
    public GameObject GetFourPieces()
    {
        for (int i = 0; i < FourPiecesList.Count; i++)
        {
            if (!FourPiecesList[i].activeInHierarchy)
            {
                return FourPiecesList[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(fourPieces);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        FourPiecesList.Add(obj);
        return obj;
    }

    //TwoPieces
    public GameObject GetTwoPieces()
    {
        for (int i = 0; i < TwoPiecesList.Count; i++)
        {
            if (!TwoPiecesList[i].activeInHierarchy)
            {
                return TwoPiecesList[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(twoPieces);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        TwoPiecesList.Add(obj);
        return obj;
    }

    //Space
    public GameObject GetSpace()
    {
        for (int i = 0; i < SpaceList.Count; i++)
        {
            if (!SpaceList[i].activeInHierarchy)
            {
                return SpaceList[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(space);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpaceList.Add(obj);
        return obj;
    }

    //LargeSpace
    public GameObject GetLargeSpace()
    {
        for (int i = 0; i < LargeSpaceList.Count; i++)
        {
            if (!LargeSpaceList[i].activeInHierarchy)
            {
                return LargeSpaceList[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(largeSpace);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        LargeSpaceList.Add(obj);
        return obj;
    }

    //Raised
    public GameObject GetRaised()
    {
        for (int i = 0; i < RaisedList.Count; i++)
        {
            if (!RaisedList[i].activeInHierarchy)
            {
                return RaisedList[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(raised);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        RaisedList.Add(obj);
        return obj;
    }

    //Normal
    public GameObject GetNormal()
    {
        for (int i = 0; i < NormalList.Count; i++)
        {
            if (!NormalList[i].activeInHierarchy)
            {
                return NormalList[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(normal);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        NormalList.Add(obj);
        return obj;
    }

    //LeftRaised
    public GameObject GetLeftRaised()
    {
        for (int i = 0; i < LeftRaisedList.Count; i++)
        {
            if (!LeftRaisedList[i].activeInHierarchy)
            {
                return LeftRaisedList[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(leftRaised);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        LeftRaisedList.Add(obj);
        return obj;
    }

    //RightRaised
    public GameObject GetRightRaised()
    {
        for (int i = 0; i < RightRaisedList.Count; i++)
        {
            if (!RightRaisedList[i].activeInHierarchy)
            {
                return RightRaisedList[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(rightRaised);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        RightRaisedList.Add(obj);
        return obj;
    }

    //SpecialJump
    public GameObject GetSpecialJump()
    {
        for (int i = 0; i < SpecialJumpList.Count; i++)
        {
            if (!SpecialJumpList[i].activeInHierarchy)
            {
                return SpecialJumpList[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(specialJump);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpecialJumpList.Add(obj);
        return obj;
    }

}//class