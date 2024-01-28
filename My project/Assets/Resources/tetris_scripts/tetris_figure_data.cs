using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;

public enum TetrisFigure {L, Z, I, O, T, RL, RZ}

public class tetris_figure_data : MonoBehaviour
{
    private GameObject pref_cube;
    private GameObject[] tetris_array;
    private int tetris_rotation;
 private TetrisFigure my_type;
 public Color my_color {get; private set;}

 private void Awake()
 {
     tetris_rotation=0;
     tetris_array=new GameObject [4];
     pref_cube=Resources.Load("tetris_prefab/tetris_cube") as GameObject;
 }


 public void MySetColor(Color _col)
{
    for (int i = 0; i < transform.childCount; i++)
    {
        GameObject GO = transform.GetChild(i).gameObject;
        Material mat=GO.GetComponent<MeshRenderer>().material;
        mat.color=_col;
        my_color=_col;
    }
}

public GameObject[] GetTetrisArray { get {return tetris_array;} }


public void TetrisRotation(bool _isPositive)
{
    if(_isPositive)
    {
        tetris_rotation++;
        tetris_rotation=tetris_rotation % 4;
    }
    else 
    {
        tetris_rotation--;
            if(tetris_rotation < 0)
            {
                tetris_rotation = 3;
            }
    }
    TetrisRotatoinType(my_type, tetris_rotation);
}

private void TetrisRotatoinType(TetrisFigure _figure, int _rot)
{
    switch (_rot)
    {
        case 0:
            if(_figure==TetrisFigure.L)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(0,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(1,-1,0);
            }
            else if(_figure==TetrisFigure.RL)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(0,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(-1,-1,0);
            }
            else if(_figure==TetrisFigure.T)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[3].transform.localPosition=new Vector3(0,1,0);
            }
            else if(_figure==TetrisFigure.I)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,-1,0);
                tetris_array[2].transform.localPosition=new Vector3(0,1,0);
                tetris_array[3].transform.localPosition=new Vector3(0,-2,0);
            }
            else if(_figure==TetrisFigure.Z)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,1,0);
                tetris_array[3].transform.localPosition=new Vector3(1,0,0);
            }
            else if(_figure==TetrisFigure.RZ)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(1,1,0);
                tetris_array[3].transform.localPosition=new Vector3(-1,0,0);
            }
            else if(_figure==TetrisFigure.O)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(0,-1,0);
            }
            break;
        case 1:
            if(_figure==TetrisFigure.L)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[3].transform.localPosition=new Vector3(1,1,0);
            }
            else if(_figure==TetrisFigure.RL)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(1,0,0);
                tetris_array[3].transform.localPosition=new Vector3(1,-1,0);
            }
            else if(_figure==TetrisFigure.T)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(0,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(1,0,0);
            }
            else if(_figure==TetrisFigure.I)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[3].transform.localPosition=new Vector3(2,0,0);
            }
            else if(_figure==TetrisFigure.Z)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(0,1,0);
            }
            else if(_figure==TetrisFigure.RZ)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,1,0);
                tetris_array[3].transform.localPosition=new Vector3(0,-1,0);
            }
            else if(_figure==TetrisFigure.O)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(0,-1,0);
            }
            break;
        case 2:
            if(_figure==TetrisFigure.L)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(0,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(-1,1,0);
            }
            else if(_figure==TetrisFigure.RL)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(0,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(1,1,0);
            }
            else if(_figure==TetrisFigure.T)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[3].transform.localPosition=new Vector3(0,-1,0);
            }
            else if(_figure==TetrisFigure.I)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,-1,0);
                tetris_array[2].transform.localPosition=new Vector3(0,1,0);
                tetris_array[3].transform.localPosition=new Vector3(0,-2,0);
            }
            else if(_figure==TetrisFigure.Z)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,1,0);
                tetris_array[3].transform.localPosition=new Vector3(1,0,0);
            }
            else if(_figure==TetrisFigure.RZ)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(0,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(-1,-1,0);
            }
            else if(_figure==TetrisFigure.O)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(0,-1,0);
            }
            break;
        case 3:
            if(_figure==TetrisFigure.L)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[3].transform.localPosition=new Vector3(-1,-1,0);
            }
            else if(_figure==TetrisFigure.RL)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[3].transform.localPosition=new Vector3(-1,1,0);
            }
            else if(_figure==TetrisFigure.T)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(0,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(-1,0,0);
            }
            else if(_figure==TetrisFigure.I)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[3].transform.localPosition=new Vector3(2,0,0);
            }
            else if(_figure==TetrisFigure.Z)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(0,1,0);
            }
            else if(_figure==TetrisFigure.RZ)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(0,1,0);
                tetris_array[2].transform.localPosition=new Vector3(1,0,0);
                tetris_array[3].transform.localPosition=new Vector3(1,-1,0);
            }
            else if(_figure==TetrisFigure.O)
            {
                tetris_array[0].transform.localPosition=new Vector3(0,0,0);
                tetris_array[1].transform.localPosition=new Vector3(-1,0,0);
                tetris_array[2].transform.localPosition=new Vector3(-1,-1,0);
                tetris_array[3].transform.localPosition=new Vector3(0,-1,0);
            }
            break;
        default:
            break;
    }
}
 public void TetrisInitialize(TetrisFigure _myType)
 {
    for (int ix = 0; ix < transform.childCount; ix++)
    {
        Destroy(transform.GetChild(ix).gameObject);
    }
    switch (_myType)
    {
        case TetrisFigure.L:
            my_type=_myType;
            GameObject obL=Instantiate(pref_cube, new Vector3(), Quaternion.identity);
            obL.AddComponent<tetris_segment>();
            obL.transform.SetParent(transform, false);

             GameObject obL2=Instantiate(pref_cube, new Vector3(0,1,0), Quaternion.identity);
            obL2.AddComponent<tetris_segment>();
            obL2.transform.SetParent(transform, false);

             GameObject obL3=Instantiate(pref_cube, new Vector3(0,-1,0), Quaternion.identity);
            obL3.AddComponent<tetris_segment>();
            obL3.transform.SetParent(transform, false);

             GameObject obL4=Instantiate(pref_cube, new Vector3(1,-1,0), Quaternion.identity);
            obL4.AddComponent<tetris_segment>();
            obL4.transform.SetParent(transform, false);
            for (int i = 0; i < tetris_array.Length; i++)
            {
                tetris_array[i]=transform.GetChild(i).gameObject;
            }
            break;
        case TetrisFigure.RL:
            my_type=_myType;
            GameObject obRL=Instantiate(pref_cube, new Vector3(), Quaternion.identity);
            obRL.AddComponent<tetris_segment>();
            obRL.transform.SetParent(transform, false);

             GameObject obRL2=Instantiate(pref_cube, new Vector3(0,1,0), Quaternion.identity);
            obRL2.AddComponent<tetris_segment>();
            obRL2.transform.SetParent(transform, false);

             GameObject obRL3=Instantiate(pref_cube, new Vector3(0,-1,0), Quaternion.identity);
            obRL3.AddComponent<tetris_segment>();
            obRL3.transform.SetParent(transform, false);

             GameObject obRL4=Instantiate(pref_cube, new Vector3(-1,-1,0), Quaternion.identity);
            obRL4.AddComponent<tetris_segment>();
            obRL4.transform.SetParent(transform, false);
            for (int i = 0; i < tetris_array.Length; i++)
            {
                tetris_array[i]=transform.GetChild(i).gameObject;
            }

            break;
        case TetrisFigure.Z:
            my_type=_myType;
            GameObject obZ=Instantiate(pref_cube, new Vector3(), Quaternion.identity);
            obZ.AddComponent<tetris_segment>();
            obZ.transform.SetParent(transform, false);

             GameObject obZ2=Instantiate(pref_cube, new Vector3(0,1,0), Quaternion.identity);
            obZ2.AddComponent<tetris_segment>();
            obZ2.transform.SetParent(transform, false);

             GameObject obZ3=Instantiate(pref_cube, new Vector3(-1,1,0), Quaternion.identity);
            obZ3.AddComponent<tetris_segment>();
            obZ3.transform.SetParent(transform, false);

             GameObject obZ4=Instantiate(pref_cube, new Vector3(1,0,0), Quaternion.identity);
            obZ4.AddComponent<tetris_segment>();
            obZ4.transform.SetParent(transform, false);
            for (int i = 0; i < tetris_array.Length; i++)
            {
                tetris_array[i]=transform.GetChild(i).gameObject;
            }
            break;
        case TetrisFigure.RZ:
        my_type=_myType;
            GameObject obRZ=Instantiate(pref_cube, new Vector3(), Quaternion.identity);
            obRZ.AddComponent<tetris_segment>();
            obRZ.transform.SetParent(transform, false);

             GameObject obRZ2=Instantiate(pref_cube, new Vector3(0,1,0), Quaternion.identity);
            obRZ2.AddComponent<tetris_segment>();
            obRZ2.transform.SetParent(transform, false);

             GameObject obRZ3=Instantiate(pref_cube, new Vector3(1,1,0), Quaternion.identity);
            obRZ3.AddComponent<tetris_segment>();
            obRZ3.transform.SetParent(transform, false);

            GameObject obRZ4=Instantiate(pref_cube, new Vector3(-1,0,0), Quaternion.identity);
            obRZ4.AddComponent<tetris_segment>();
            obRZ4.transform.SetParent(transform, false);
            for (int i = 0; i < tetris_array.Length; i++)
            {
                tetris_array[i]=transform.GetChild(i).gameObject;
            }
            break;
        case TetrisFigure.I:
            my_type=_myType;
            GameObject obI=Instantiate(pref_cube, new Vector3(), Quaternion.identity);
            obI.AddComponent<tetris_segment>();
            obI.transform.SetParent(transform, false);

             GameObject obI2=Instantiate(pref_cube, new Vector3(0,1,0), Quaternion.identity);
            obI2.AddComponent<tetris_segment>();
            obI2.transform.SetParent(transform, false);

             GameObject obI3=Instantiate(pref_cube, new Vector3(0,-1,0), Quaternion.identity);
            obI3.AddComponent<tetris_segment>();
            obI3.transform.SetParent(transform, false);

             GameObject obI4=Instantiate(pref_cube, new Vector3(0,2,0), Quaternion.identity);
            obI4.AddComponent<tetris_segment>();
            obI4.transform.SetParent(transform, false);
            for (int i = 0; i < tetris_array.Length; i++)
            {
                tetris_array[i]=transform.GetChild(i).gameObject;
            }
            break;
        case TetrisFigure.O:
            my_type=_myType;
            GameObject obO=Instantiate(pref_cube, new Vector3(), Quaternion.identity);
            obO.AddComponent<tetris_segment>();
            obO.transform.SetParent(transform, false);

             GameObject obO2=Instantiate(pref_cube, new Vector3(-1,0,0), Quaternion.identity);
            obO2.AddComponent<tetris_segment>();
            obO2.transform.SetParent(transform, false);

             GameObject obO3=Instantiate(pref_cube, new Vector3(-1,-1,0), Quaternion.identity);
            obO3.AddComponent<tetris_segment>();
            obO3.transform.SetParent(transform, false);

             GameObject obO4=Instantiate(pref_cube, new Vector3(0,-1,0), Quaternion.identity);
            obO4.AddComponent<tetris_segment>();
            obO4.transform.SetParent(transform, false);

            for (int i = 0; i < tetris_array.Length; i++)
            {
                tetris_array[i]=transform.GetChild(i).gameObject;
            }
            break;
        case TetrisFigure.T:
            my_type=_myType;
            GameObject obT=Instantiate(pref_cube, new Vector3(), Quaternion.identity);
            obT.AddComponent<tetris_segment>();
            obT.transform.SetParent(transform, false);

             GameObject obT2=Instantiate(pref_cube, new Vector3(1,0,0), Quaternion.identity);
            obT2.AddComponent<tetris_segment>();
            obT2.transform.SetParent(transform, false);

             GameObject obT3=Instantiate(pref_cube, new Vector3(-1,0,0), Quaternion.identity);
            obT3.AddComponent<tetris_segment>();
            obT3.transform.SetParent(transform, false);

             GameObject obT4=Instantiate(pref_cube, new Vector3(0,1,0), Quaternion.identity);
            obT4.AddComponent<tetris_segment>();
            obT4.transform.SetParent(transform, false);

            for (int i = 0; i < tetris_array.Length; i++)
            {
                tetris_array[i]=transform.GetChild(i).gameObject;
            }
            break;
        default:
             break;
    }
 }
}
