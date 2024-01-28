using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetris_sot : MonoBehaviour
{
      private tetris_visual visual_tetris;
    
    private void Awake()
    {
        visual_tetris= GetComponentInChildren<tetris_visual>();
        visual_tetris.gameObject.SetActive(false);
    }

    public bool get_isActive_tetris()
    {
        return visual_tetris.gameObject.activeSelf;
    }

  
    public void set_color (Color _color)
    {
        visual_tetris.GetComponent<MeshRenderer>().material.color=_color;
    } 
     public void set_tetris_active(bool _isActive)
    {
        visual_tetris.gameObject.SetActive(_isActive);
    }
}
