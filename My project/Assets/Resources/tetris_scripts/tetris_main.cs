using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public struct MyHud
{
    public Text txt_score;
    public int int_score;
    public Text txt_line;
    private int int_line;
    public Text txt_level;
    public int int_level{get; private set;}
    public int hjklfvdcfvgbdsfgbl;
    public float speed_tetris;
    public int counter_line;

    public void UpdateScore(int _score)
    {
        int_score+=_score;
        txt_score.text=int_score.ToString();
    }
    public void AddLine(int _line)
    {
        int_line+=_line;
        txt_line.text=int_line.ToString();
        counter_line+=_line;
        if(counter_line>9)
            AddLevel(1);
        counter_line=counter_line%10;
    }
    public void AddLevel(int _level)
    {
        int_level+=_level;
        txt_level.text=int_level.ToString();
        speed_tetris-=int_level>5?0.02f:0.05f;
    }

}
public class tetris_main : MonoBehaviour
{
    private const int width = 13, height = 21;
    private float tetris_step =1;
    private float curr_time;
    private GameObject pref_tetris;
    private Object prefab_tetris_object;
    private tetris_figure t_figure;
    private TetrisFigure figure_random;
    private tetris_sot[,] my_array;
    private GameObject my_3d_cam;
    private GameObject ani_cam;
    private GameObject main_cam;
  private My_title_sc my_titile;
    private MyHud my_hud;
    private tetris_figure shadowF;
    private Quaternion shadowRot;
    private int playerScore;
      [SerializeField]
     public Leaderboard leaderboard;
private void Start()
    { 
        leaderboard.LoadHighScores();
        curr_time=0;
        my_array = new tetris_sot[width, height];
        pref_tetris = Resources.Load("tetris_prefab/tetris_figure") as GameObject;
        prefab_tetris_object=Resources.Load("tetris_prefab/tetris_sot");
        my_titile=FindObjectOfType<My_title_sc>();
        my_3d_cam=GameObject.FindGameObjectWithTag("cam");
        main_cam=GameObject.FindGameObjectWithTag("MainCamera");
        ani_cam=GameObject.FindGameObjectWithTag("cat_ani");
        ani_cam.SetActive(false);
        Camera(false);
        my_hud.txt_score=GameObject.FindGameObjectWithTag("my_score").GetComponent<Text>();
        my_hud.txt_line=GameObject.FindGameObjectWithTag("my_line").GetComponent<Text>();
        my_hud.txt_level=GameObject.FindGameObjectWithTag("my_level").GetComponent<Text>();
        my_hud.speed_tetris=0.5f;
        my_hud.AddLevel(1);
        figure_random=CreateRandomigure();
        CreateFigure(figure_random);//фигуры посмотреть потому что там что то во что то превращается лол 
        figure_random=CreateRandomigure();
        my_titile.GetComponentInChildren<tetris_figure_data>().TetrisInitialize(figure_random);
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            GameObject go=Instantiate(prefab_tetris_object, new Vector3(x*tetris_step,y*tetris_step,0),
            Quaternion.identity) as GameObject;
            my_array[x,y] = go.GetComponent<tetris_sot>();
        }
    }
    
    }
private TetrisFigure CreateRandomigure()
{
    return(TetrisFigure)Random.Range(0,7);
}
private void CreateFigure(TetrisFigure _figure)
    {
        
        t_figure = Instantiate(pref_tetris, new Vector3(tetris_step * 6, tetris_step* (height-2)),
         Quaternion.identity).GetComponent<tetris_figure>();
        t_figure.GetComponentInChildren<tetris_figure_data>().TetrisInitialize(_figure);
        if(my_hud.int_level<3)
                t_figure.GetComponentInChildren<tetris_figure_data>().MySetColor(Random.ColorHSV(0.4f,0.6f,1,1,1,1,1,1));
        else if(my_hud.int_level<5)
                t_figure.GetComponentInChildren<tetris_figure_data>().MySetColor(Random.ColorHSV(0.5f,0.7f,1,1,1,1,1,1));
        else if(my_hud.int_level<7)
                t_figure.GetComponentInChildren<tetris_figure_data>().MySetColor(Random.ColorHSV(0.8f,1.0f,1,1,1,1,1,1));
        else if(my_hud.int_level<10)
                t_figure.GetComponentInChildren<tetris_figure_data>().MySetColor(Random.ColorHSV(0.2f,0.4f,1,1,1,1,1,1));
        else if(my_hud.int_level>=10)
                t_figure.GetComponentInChildren<tetris_figure_data>().MySetColor(Random.ColorHSV(0.3f,0.5f,1,1,1,1,1,1));
       

        shadowF=Instantiate(pref_tetris, new Vector3(tetris_step*6, tetris_step*(height-2)),Quaternion.identity).GetComponent<tetris_figure>();
        shadowF.GetComponentInChildren<tetris_figure_data>().TetrisInitialize(_figure);
        shadowF.GetComponentInChildren<tetris_figure_data>().MySetColor(new Color(0.7f,0.7f,0.7f,0.7f));
        shadowF.enabled=false;

        StartCoroutine(tetris_update(my_hud.speed_tetris));
    }

 
private IEnumerator tetris_update(float _time)
    {
        while(true)
        {  
        yield return new WaitForSeconds(_time);
        shadowF.transform.position=t_figure.transform.position;
        while(!ChecPreIntersect(shadowF))
        {
            shadowF.tetrisDrop(true);
        } 
        t_figure.tetrisDrop(true);
        if(ChecPreIntersect(t_figure))
            break;
        }
        AddToArray();
        Destroy(t_figure.gameObject);
        Destroy(shadowF.gameObject);
        RemoveFullLine();
        if(!IsGameOver())
        {
            CreateFigure(figure_random);
            figure_random=CreateRandomigure();
            my_titile.GetComponentInChildren<tetris_figure_data>().TetrisInitialize(figure_random);
        }
        else if(IsGameOver())
        {
            main_cam.SetActive(false);
            my_3d_cam.SetActive(false);
            ani_cam.SetActive(true);
            my_titile.gameObject.SetActive(false); 
            playerScore=my_hud.int_score;
            leaderboard.UpdateLeaderboard(playerScore);
        }
        
    }
private void AddToArray()
    {
        GameObject[] go = t_figure.GetComponentInChildren<tetris_figure_data>().GetTetrisArray;

        for (int i = 0; i < go.Length; i++)
        {
            int x = (int)go[i].transform.position.x;
            int y = (int)go[i].transform.position.y;
            my_array[x,y].set_tetris_active(true);
            my_array[x,y].set_color(t_figure.GetComponentInChildren<tetris_figure_data>().my_color);
        }
    }

private void RemoveFullLine()
{
    int[] remove_line = CheckLine();
    for (int ind = 0; ind < remove_line.Length; ind++)
    {
        for (int x = 0; x < width; x++)
        {
            my_array[x,remove_line[ind]].set_tetris_active(false);
            my_hud.UpdateScore(remove_line.Length == 4 ? 750 : 350);
        }
        }
    if(remove_line.Length != 0)
    {
        int[] empty_line=CheckEmptyLine();
        bool[,] arr_newTetris = new bool[width,height];
        int start_y=0;
        my_hud.AddLine(remove_line.Length);
        for (int y = 0; y < height; y++)
        {
            if(SkipTheLine(empty_line,y))
                continue;
            for (int x = 0; x < width; x++)
                arr_newTetris[x,start_y]=my_array[x,y].get_isActive_tetris();
            start_y++;
        }
        SetNewTetrisArray(arr_newTetris);
    }
}
public void Camera(bool _is3d)
{
    main_cam.SetActive(!_is3d);
    my_3d_cam.SetActive(_is3d);
}

public void StartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
public void Pause(bool _isPause)
{
    if(_isPause==true){
        Time.timeScale=0;
    }else if(_isPause==false){
        Time.timeScale=1;
    }
}
private bool IsGameOver()
{
    for (int i = 0; i < width; i++)
    {
       if( my_array[i,height-3].get_isActive_tetris())
       return true;
    }
    return false;
}
private void SetNewTetrisArray(bool[,] _arr_new)
{
    for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
            my_array[x,y].set_tetris_active(_arr_new[x,y]);
}
private bool SkipTheLine(int[] _empty_line, int _y)
{
    for (int y = 0; y < _empty_line.Length; y++)
    {
            if(_empty_line[y] == _y)
            return true;
    }
    return false;
}
private int[] CheckEmptyLine()
    {
        List<int> arr = new List<int>();
        for (int ind = 0; ind < height; ind++)
        {
           int count_line_x = 0; 
            for (int x = 0; x < width; x++)
            {
                if(my_array[x,ind].get_isActive_tetris())
                    break;
                else
                    count_line_x++;
            }
            if(count_line_x == width)
            {
                arr.Add(ind);
            }
        }
        return arr.ToArray();
    }
private int[] CheckLine()
    {
        List<int> arr = new List<int>();
        for (int ind = 0; ind < height; ind++)
        {
           int count_line_x = 0; 
            for (int x = 0; x < width; x++)
            {
                if(my_array[x,ind].get_isActive_tetris())
                    count_line_x++;
                else
                    break;
            }

            if(count_line_x == width)
            {
                arr.Add(ind);
            }
        }
        return arr.ToArray();
    }
private void Update()
    {
        if(t_figure)
        {
            if(Input.GetButtonDown("RevTetr"))
            {
                shadowF.GetComponentInChildren<tetris_figure_data>().TetrisRotation(true);
                t_figure.GetComponentInChildren<tetris_figure_data>().TetrisRotation(true);
                if(ChecIntersect(t_figure)){
                    t_figure.GetComponentInChildren<tetris_figure_data>().TetrisRotation(false);}

            }
            if(Input.GetButtonDown("Left_tetris"))
            {
                curr_time=0;
                t_figure.MySetDirection(MyDirectionTetris.LEFT);
                if(ChecIntersect(t_figure))
                    t_figure.MySetDirection(MyDirectionTetris.RIGHT);
            }
            else if(Input.GetButtonDown("Right_tetris"))
            {
                curr_time=0;
                t_figure.MySetDirection(MyDirectionTetris.RIGHT);
                if(ChecIntersect(t_figure))
                    t_figure.MySetDirection(MyDirectionTetris.LEFT);
            }
            if (Input.GetButton("Down_tetris"))
            {
                InputPress(MyDirectionTetris.DOWN,0.04f);
            }
            if(Input.GetButton("moment"))
            {
                InputPress(MyDirectionTetris.DOWN,0.00000004f);
            }
            else if (Input.GetButton("Left_tetris"))
            {
                InputPress(MyDirectionTetris.LEFT,0.1f);
            }
            if (Input.GetButton("Right_tetris"))
            {
                InputPress(MyDirectionTetris.RIGHT,0.1f);
            }
            if(Input.GetButtonUp("Left_tetris")|| Input.GetButtonUp("Right_tetris"))
                    curr_time=0;  
        }
    }
private void InputPress(MyDirectionTetris _dir,float _time)
{
curr_time+=Time.deltaTime;
    if(curr_time>_time)
    {
    curr_time=0;
    if(_dir==MyDirectionTetris.LEFT)
        {
            t_figure.MySetDirection(MyDirectionTetris.LEFT);
            if(ChecIntersect(t_figure))
                t_figure.MySetDirection(MyDirectionTetris.RIGHT);
        }
        else if(_dir==MyDirectionTetris.RIGHT)
        {
            t_figure.MySetDirection(MyDirectionTetris.RIGHT);
            if(ChecIntersect(t_figure))
                t_figure.MySetDirection(MyDirectionTetris.LEFT);
        }
        else if(_dir==MyDirectionTetris.DOWN)
        {
            t_figure.tetrisDrop(true);
            if(ChecIntersect(t_figure))
            {
                t_figure.tetrisDrop(false);
            }
        }
    }
}
private bool ChecIntersect(tetris_figure _figure)
    {
        for (int ind = 0; ind < _figure.GetSegments().Length; ind++)
        {
            int  x =(int)_figure.GetSegments()[ind].transform.position.x;
            int y =(int)_figure.GetSegments()[ind].transform.position.y;

            bool Is_Intersect = IsIntersect(x,y);
            if(Is_Intersect)
                return Is_Intersect;
        }
        return false;
    }
private bool ChecPreIntersect(tetris_figure _figure)
    {
        for (int ind = 0; ind < _figure.GetSegments().Length; ind++)
        {
            int  x =(int)_figure.GetSegments()[ind].transform.position.x;
            int y =(int)_figure.GetSegments()[ind].transform.position.y;

            bool Is_Intersect = IsIntersect(x,y);
            if(Is_Intersect)
            {
                _figure.tetrisDrop(false);
                return Is_Intersect;
            }
        }
        return false;
    }
private bool IsIntersect(int _x, int _y)
    {
        try
        {
            if(my_array[_x,_y].get_isActive_tetris())
            return true;
        }
        catch (System.Exception ){return true;}
        return false;
    }
}
