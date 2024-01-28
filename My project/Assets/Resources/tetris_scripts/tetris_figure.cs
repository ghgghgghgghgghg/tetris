using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MyDirectionTetris: int {LEFT=-1, RIGHT=1, DOWN}
public class tetris_figure : MonoBehaviour
{
    public void tetrisDrop(bool _isPositive)
    {
        if (_isPositive)
            transform.Translate(0, -1, 0);
        else
            transform.Translate(0, 1, 0);
    }

    public void MySetDirection(MyDirectionTetris _dir)
    {
        transform.Translate((int)_dir,0,0);
    }
    public tetris_segment[] GetSegments()
    {
        return GetComponentsInChildren<tetris_segment>();
    }
    public void tetr(tetris_figure other)
    {
        transform.position=other.transform.position;
        transform.rotation=other.transform.rotation;
    }
}
