using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Linq;
using System.IO;

public class gravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private float bounceForce = 2f; // 初始弹跳力
    private float bounceDamping = 0.5f; // 弹跳力衰减系数
    public VideoClip endVideo; // 视频剪辑
    public bool isClicked = false;
    private static Vector3[] slotPositions = new Vector3[18]; // 修改为18个位置
    public static GameObject[] slotObjects = new GameObject[18]; // 修改为18个位置
    public float originalGravityScale;
    public Vector3 originalPosition; // 记录原始位置
    private GameObject miniObject; // 缩小后的物体
    private static int clickedCount = 0; // 记录已点击的大物体数量
    private static List<string> selectedObjectNames = new List<string>(); // 记录已选中大物体文件名称
    private bool hasTriggered = false; // 记录事件是否已触发
    private bool isInView = false; // 记录物体是否在视野内

    // Define the MiniObject class
    public class MiniObject : MonoBehaviour
    {
        public GameObject originalObject;
        public int slotIndex;
        public string originalObjectName;

        void OnMouseDown()
        {
            // 恢复原始大物体
            originalObject.SetActive(true);
            originalObject.GetComponent<Rigidbody2D>().gravityScale = originalObject.GetComponent<gravity>().originalGravityScale;
            originalObject.GetComponent<Collider2D>().enabled = true;
            originalObject.transform.position = originalObject.GetComponent<gravity>().originalPosition;

            // 销毁小物体
            Destroy(gameObject);

            // 更新槽位和计数
            slotObjects[slotIndex] = null;
            clickedCount--;
            selectedObjectNames.Remove(originalObjectName);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
        if (col == null)
        {
            col = gameObject.AddComponent<Collider2D>();
        }
        rb.gravityScale = 1; // 设置重力
        originalGravityScale = rb.gravityScale;
        originalPosition = transform.position; // 记录原始位置

        // 初始化18个槽位位置
        Vector3 cameraPosition = Camera.main.transform.position;
        // 上面一排（原有的9个位置，y坐标降低4个单位）
        slotPositions[0] = new Vector3(cameraPosition.x - 4.4f, cameraPosition.y + 5.22f, 0);
        slotPositions[1] = new Vector3(cameraPosition.x - 3.26f, cameraPosition.y + 5.22f, 0);
        slotPositions[2] = new Vector3(cameraPosition.x - 2.08f, cameraPosition.y + 5.22f, 0);
        slotPositions[3] = new Vector3(cameraPosition.x - 0.94f, cameraPosition.y + 5.22f, 0);
        slotPositions[4] = new Vector3(cameraPosition.x + 0.15f, cameraPosition.y + 5.22f, 0);
        slotPositions[5] = new Vector3(cameraPosition.x + 1.29f, cameraPosition.y + 5.22f, 0);
        slotPositions[6] = new Vector3(cameraPosition.x + 2.47f, cameraPosition.y + 5.22f, 0);
        slotPositions[7] = new Vector3(cameraPosition.x + 3.61f, cameraPosition.y + 5.22f, 0);
        slotPositions[8] = new Vector3(cameraPosition.x + 4.82f, cameraPosition.y + 5.22f, 0);
        // 下面一排（新的9个位置，y坐标再降低4个单位，并错开一些距离）
        slotPositions[9] = new Vector3(cameraPosition.x - 4.4f, cameraPosition.y + 3.72f, 0);
        slotPositions[10] = new Vector3(cameraPosition.x - 3.26f, cameraPosition.y + 3.72f, 0);
        slotPositions[11] = new Vector3(cameraPosition.x - 2.08f, cameraPosition.y + 3.72f, 0);
        slotPositions[12] = new Vector3(cameraPosition.x - 0.94f, cameraPosition.y + 3.72f, 0);
        slotPositions[13] = new Vector3(cameraPosition.x + 0.15f, cameraPosition.y + 3.72f, 0);
        slotPositions[14] = new Vector3(cameraPosition.x + 1.29f, cameraPosition.y + 3.72f, 0);
        slotPositions[15] = new Vector3(cameraPosition.x + 2.47f, cameraPosition.y + 3.72f, 0);
        slotPositions[16] = new Vector3(cameraPosition.x + 3.61f, cameraPosition.y + 3.72f, 0);
        slotPositions[17] = new Vector3(cameraPosition.x + 4.82f, cameraPosition.y + 3.72f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfInView();

        // 更新槽位位置
        Vector3 cameraPosition = Camera.main.transform.position;
        // 上面一排（原有的9个位置，y坐标降低4个单位）
        slotPositions[0] = new Vector3(cameraPosition.x - 4.4f, cameraPosition.y + 5.22f, 0);
        slotPositions[1] = new Vector3(cameraPosition.x - 3.26f, cameraPosition.y + 5.22f, 0);
        slotPositions[2] = new Vector3(cameraPosition.x - 2.08f, cameraPosition.y + 5.22f, 0);
        slotPositions[3] = new Vector3(cameraPosition.x - 0.94f, cameraPosition.y + 5.22f, 0);
        slotPositions[4] = new Vector3(cameraPosition.x + 0.15f, cameraPosition.y + 5.22f, 0);
        slotPositions[5] = new Vector3(cameraPosition.x + 1.29f, cameraPosition.y + 5.22f, 0);
        slotPositions[6] = new Vector3(cameraPosition.x + 2.47f, cameraPosition.y + 5.22f, 0);
        slotPositions[7] = new Vector3(cameraPosition.x + 3.61f, cameraPosition.y + 5.22f, 0);
        slotPositions[8] = new Vector3(cameraPosition.x + 4.82f, cameraPosition.y + 5.22f, 0);
        // 下面一排（新的9个位置，y坐标再降低4个单位，并错开一些距离）
        slotPositions[9] = new Vector3(cameraPosition.x - 4.4f, cameraPosition.y + 3.72f, 0);
        slotPositions[10] = new Vector3(cameraPosition.x - 3.26f, cameraPosition.y + 3.72f, 0);
        slotPositions[11] = new Vector3(cameraPosition.x - 2.08f, cameraPosition.y + 3.72f, 0);
        slotPositions[12] = new Vector3(cameraPosition.x - 0.94f, cameraPosition.y + 3.72f, 0);
        slotPositions[13] = new Vector3(cameraPosition.x + 0.15f, cameraPosition.y + 3.72f, 0);
        slotPositions[14] = new Vector3(cameraPosition.x + 1.29f, cameraPosition.y + 3.72f, 0);
        slotPositions[15] = new Vector3(cameraPosition.x + 2.47f, cameraPosition.y + 3.72f, 0);
        slotPositions[16] = new Vector3(cameraPosition.x + 3.61f, cameraPosition.y + 3.72f, 0);
        slotPositions[17] = new Vector3(cameraPosition.x + 4.82f, cameraPosition.y + 3.72f, 0);

        // 更新槽位中的物体位置
        for (int i = 0; i < slotObjects.Length; i++)
        {
            if (slotObjects[i] != null)
            {
                slotObjects[i].transform.position = slotPositions[i];
            }
        }

        // 检查按下 D 键时 MainCamera 的 x 坐标
        if (Camera.main.transform.position.x > 50 && !hasTriggered)
        {
            hasTriggered = true;
            string aminuosiPath = Path.Combine(Application.streamingAssetsPath, "aminuosi.txt");
            File.WriteAllText(aminuosiPath , string.Empty); // 清空文件内容
            File.AppendAllLines(aminuosiPath , selectedObjectNames); // 添加新的内容
        }
    }

    void CheckIfInView()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        float cameraWidth = 9.86f; // 视线范围宽度的一半
        float cameraHeight = 9.69f; // 视线范围高度的一半

        bool onScreen = transform.position.x > cameraPosition.x - cameraWidth &&
                        transform.position.x < cameraPosition.x + cameraWidth &&
                        transform.position.y > cameraPosition.y - cameraHeight &&
                        transform.position.y < cameraPosition.y + cameraHeight;

        if (onScreen)
        {
            isInView = true;
            rb.gravityScale = originalGravityScale; // 设置重力
        }
        else
        {
            isInView = false;
            rb.gravityScale = 0; // 取消重力
        }
    }

    void OnMouseDown()
    {
        if (!isClicked)
        {
            // 查找空闲槽位
            int emptySlotIndex = -1;
            for (int i = 0; i < slotPositions.Length; i++)
            {
                if (slotObjects[i] == null)
                {
                    emptySlotIndex = i;
                    break;
                }
            }

            if (emptySlotIndex != -1)
            {
                isClicked = true;
                rb.gravityScale = 0; // 取消重力
                col.enabled = false; // 禁用碰撞体积
                gameObject.SetActive(false); // 隐藏原始物体

                // 创建缩小后的物体
                miniObject = new GameObject("MiniObject");
                SpriteRenderer miniRenderer = miniObject.AddComponent<SpriteRenderer>();
                miniRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
                miniRenderer.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

                miniObject.transform.position = slotPositions[emptySlotIndex];
                miniObject.transform.localScale = transform.localScale * 0.5f;
                BoxCollider2D miniCollider = miniObject.AddComponent<BoxCollider2D>();
                miniCollider.isTrigger = true; // 设置为触发器，防止碰撞

                MiniObject miniObjectScript = miniObject.AddComponent<MiniObject>();
                miniObjectScript.originalObject = this.gameObject;
                miniObjectScript.slotIndex = emptySlotIndex;
                miniObjectScript.originalObjectName = this.gameObject.name; // 记录大物体的名字

                slotObjects[emptySlotIndex] = miniObject;
                clickedCount++; // 增加计数
                selectedObjectNames.Add(this.gameObject.name); // 记录已选中大物体文件名称
            }
            else
            {
                StartCoroutine(ShakeObject());
            }
        }
        else
        {
            isClicked = false;
            rb.gravityScale = originalGravityScale; // 恢复重力
            col.enabled = true; // 启用碰撞体积
            gameObject.SetActive(true); // 显示原始物体
            transform.position = originalPosition; // 恢复原始位置

            // 查找并销毁对应的缩小后的物体
            for (int i = 0; i < slotObjects.Length; i++)
            {
                if (slotObjects[i] != null && slotObjects[i].GetComponent<MiniObject>().originalObject == this.gameObject)
                {
                    Destroy(slotObjects[i]);
                    slotObjects[i] = null;
                    break;
                }
            }

            clickedCount--; // 减少计数
            selectedObjectNames.Remove(this.gameObject.name); // 移除已选中大物体文件名称
        }

        // 检查计数是否达到18
        if (clickedCount >= 18)
        {
            StartCoroutine(ShakeObject());
        }
    }

    IEnumerator ShakeObject()
    {
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;
        float duration = 0.5f;
        float magnitude = 0.1f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
    }
}