using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

struct Order {
    private int orderNum;
    private int recipeCode;
    private int clientCode;
    private Sprite recipeImage;
    private float startTime;
    private float timeLimit;
    private GameObject orderObject;

    public int ordernum
    {
        get { return orderNum; }
        set { orderNum = value; }
    }
    public int recipecode {
        get { return recipeCode; }
        set { recipeCode = value; }
    }
    public int clientcode
    {
        get { return clientCode; }
        set { clientCode = value; }
    }
    public Sprite recipeimage {
        get { return recipeImage; }
        set { recipeImage = value; }
    }
    public float starttime {
        get { return startTime; }
        set { startTime = value; }
    }
    public float timelimit{
        get { return timeLimit; }
        set { timeLimit = value; }
    }
    public GameObject orderobject {
        get { return orderObject; }
        set { orderObject = value; }
    }
    public Order(int ordernum, int num, int client, Sprite image, float startTime, float timeLimit, GameObject orderObject)
    {
        this.orderNum = ordernum;
        this.recipeCode = num;
        this.clientCode = client;
        this.recipeImage = image;
        this.startTime = startTime;
        this.timeLimit = timeLimit;
        this.orderObject = orderObject;
    }
};

public class OrderListScript : MonoBehaviour
{
    public GameObject[] ClientPrefab;
    public GameObject[] Chairs;
    public GameObject OrderButtonPrefab;
    public GameObject OrderParent;
    public GameObject ClientParent;
    public GameObject SpawnPosition;

    private List<GameObject> ClientList;
    private List<Order> orderList;
    private static int orderNumber = 10000;

    int orderCount;
    float currentTime;
    RectTransform RT;
    float time;                     // 손님 생성되는 시간 간격
    int index;

    // Start is called before the first frame update
    private void Start()
    {
        currentTime = 0.0f;
        orderCount = 0;
        orderList = new List<Order>();
        ClientList = new List<GameObject>();
        time = UnityEngine.Random.Range(0.0f, 10.0f);
        index = 0;
    }
    
    // 실시간으로 주문의 제한시간 감소
    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if(orderCount > 0)
        {
            foreach (var order in orderList)
            {
                var tmp = order.orderobject;
                Slider tmpSlider = tmp.transform.GetChild(2).GetComponent<Slider>();
                tmpSlider.value = 1.0f - (currentTime - order.starttime) / order.timelimit;
                if(currentTime - order.starttime >= order.timelimit)    // 제한 시간이 다 한 주문은 삭제
                {
                    RemoveClient(order.clientcode, 2);
                    orderList.Remove(order);
                    orderCount--;
                    Destroy(tmp);
                    RearrangeViewport();
                    return;
                }
            }
        }

        if(currentTime >= time)
        {
            SpawnClient();
            time += UnityEngine.Random.Range(5.0f, 20.0f);
        }
    }

    public void order(int clientCode)
    {
        MakeOrder(clientCode);
    }

    private void MakeOrder(int clientCode)
    {
        System.Random r = new System.Random();

        while (true)
        {
            int randomNumber = 4000 + r.Next(0, DatabaseScript.RecipesLength_ALL() - 1);
            if (CheckIngredients(randomNumber))
            {
                foreach (var recipe in DatabaseScript.RECIPES)
                {
                    if (recipe.code == randomNumber && recipe.earned == 1)      // 주문 객체 생성
                    {
                        var tmp = Instantiate(OrderButtonPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                        tmp.transform.SetParent(OrderParent.transform, false);
                        tmp.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = recipe.image;
                        tmp.transform.GetChild(0).GetComponent<OrderButtonScript>().orderNum = orderNumber;

                        Order newOrder = new Order(orderNumber, recipe.code, clientCode, recipe.image, currentTime, 30.0f, tmp);
                        orderList.Add(newOrder);
                        orderCount++;
                        orderNumber++;
                        RearrangeViewport();
                        return;
                    }
                }
            }
        }
    }

    public void RemoveClient(int code, int flag)
    {
        for(int i = 0; i < ClientList.Count; i++)
        {
            if(ClientList[i].GetComponent<ClientScript>().code == code)
            {
                if (flag == 2)
                    ClientList[i].GetComponent<ClientScript>().timer += 30.0f;
                ClientList[i].GetComponent<ClientScript>().flag += 1;
                ClientList.Remove(ClientList[i]);
            }
        }
    }

    public void RemoveOrder(int num, int flag)          // flag 1: 요리 성공 / flag 2: 요리 실패
    {
        for (int i = 0; i < orderList.Count; i++)
        {
            if (orderList[i].ordernum == num)
            {
                RemoveClient(orderList[i].clientcode, flag);
                Destroy(orderList[i].orderobject);
                orderList.Remove(orderList[i]);
                orderCount--;
            }
        }
        RearrangeViewport();
    }

    private void RearrangeViewport()
    {
        RT = OrderParent.GetComponent<RectTransform>();
        int height = orderCount * 270;
        int posY = -height / 2;
        RT.anchoredPosition = new Vector2(0.0f, posY);
        RT.sizeDelta = new Vector2(250, height);

        float startPosY = (orderCount - 1) * 135.0f;
        for(int i = 0; i < orderCount; i++)
        {
            RectTransform rt;
            var tmp = orderList[i].orderobject;
            rt = tmp.GetComponent<RectTransform>();
            rt.anchoredPosition3D = new Vector3(0.0f, startPosY - i * 270, 0.0f);
        }
    }

    private void SpawnClient()
    {
        var tmp = Instantiate(ClientPrefab[index], SpawnPosition.transform.position, Quaternion.identity);
        tmp.transform.SetParent(ClientParent.transform, true);
        ClientList.Add(tmp);
        tmp.GetComponent<ClientScript>().code = index;
        tmp.GetComponent<ClientScript>().Chair = Chairs[index];
        index++;
        if (index >= 13)
            index = 0;
    }

    private bool CheckIngredients(int code)
    {
        if (DatabaseScript.CheckCookable(code))
            return true;
        return false;
    }
}
