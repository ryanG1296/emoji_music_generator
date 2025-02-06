using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CameraMove : MonoBehaviour
{
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private float _moveTime = 1.5f; // 增加移动时间以减慢速度
    private float _elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !_isMoving && transform.position.x <=50)
        {
            _targetPosition = transform.position + new Vector3(-11, 0, 0);
            _isMoving = true;
            _elapsedTime = 0f;
        }
        if (Input.GetKeyDown(KeyCode.D) && !_isMoving)
        {
            HandleKeyDownD();
        }
        if (Input.GetKeyDown(KeyCode.S) && !_isMoving)
        {
            // RecordVisibleObjects(); // 注释掉保存小物体的代码
            _targetPosition = transform.position + new Vector3(0, -21.86f, 0);
            _isMoving = true;
            _elapsedTime = 0f;
        }

        if (_isMoving)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / _moveTime;
            transform.position = Vector3.Lerp(transform.position, _targetPosition, t);
            if (t >= 1f)
            {
                _isMoving = false;
            }
        }
    }

    private void HandleKeyDownD()
    {
        _targetPosition = transform.position + new Vector3(11, 0, 0);
        _isMoving = true;
        _elapsedTime = 0f;
    }

    // private void RecordVisibleObjects()
    // {
    //     List<string> _objectNames = new List<string>();

    //     for (int i = 0; i < gravity.slotObjects.Length; i++)
    //     {
    //         if (gravity.slotObjects[i] != null)
    //         {
    //             MiniObject _miniObjectScript = gravity.slotObjects[i].GetComponent<MiniObject>();
    //             if (_miniObjectScript != null)
    //             {
    //                 _objectNames.Add(_miniObjectScript.originalObjectName);
    //             }
    //         }
    //     }

    //     string _path = Path.Combine(Application.dataPath, "aminuosi.txt");
    //     File.WriteAllLines(_path, _objectNames);
    // }
}