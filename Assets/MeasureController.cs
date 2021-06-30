using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class MeasureController : MonoBehaviour
{

    [SerializeField] GameObject cube;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //個数制限
    private int count = 0;

    List<GameObject> lists_toggle = new List<GameObject>();

    //オブジェクトの座標
    private Vector3 pos = new Vector3(0, 0, 0);

    //UIを入れる
    GameObject distance;
    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        this.distance = GameObject.Find("Distance");
    }

    // Update is called once per frame
    void Update()
    {
        //タップされたときの挙動
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began && count <= 1) 
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.All))
                {
                    count++;
                    Pose hitPose = hits[0].pose;
                    var gameObj = Instantiate(cube, hitPose.position, hitPose.rotation);
                    lists_toggle.Add(gameObj);
                }
            }
            if(count >= 2)
            {
                //長さ計算
                float length = (lists_toggle[0].transform.position - lists_toggle[1].transform.position).magnitude;

                this.distance.GetComponent<Text>().text = (length * 100f).ToString("F2") + "cm";
            }
        }
    }
}
