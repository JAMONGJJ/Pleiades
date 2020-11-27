using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 레시피 카테고리
enum Category
{
    Chicken,
    Pork,
    Beef,
    Others,
}

// 아이템 구조체
public struct Item
{
    public string name;     // always begin with capital letter
    public string info;
    public int code;      // begin with 0000
    public Sprite image;
    public Item(string n, string i, int c, Sprite img)
    {
        name = n;
        info = i;
        code = c;
        image = img;
    }
};

// 레시피 구조체
public struct Recipe
{
    private string Name;     // always begin with capital letter
    private string Info;
    private int Code;        // begin with 4000
    private int Earned;      // 0:not yet, 1:done
    private int Category;    // 0:chicken, 1:pork, 2:beef, 3:others
    private int Price;
    private Dictionary<int, int> Ingredients;    // <ingredient code, item count>
    private Sprite Image;

    public string name {
        get { return Name; }
        set { Name = value; }
    }
    public string info {
        get { return Info; }
        set { Info = value; }
    }
    public int code {
        get { return Code; }
        set { Code = value; }
    }
    public int earned{
        get { return Earned; }
        set { Earned = value; }
    }
    public int category{
        get { return Category; }
        set { Category = value; }
    }
    public int price {
        get { return Price; }
        set { Price = value; }
    }
    public Dictionary<int, int> ingredients{
        get { return Ingredients; }
        set { Ingredients = value; }
    }
    public Sprite image{
        get { return Image; }
        set { Image = value; }
    }
    public Recipe(string n, string i, int c, int e, int cat, int price, Sprite img)
    {
        this.Name = n;
        this.Info = i;
        this.Code = c;
        this.Earned = e;
        this.Category = cat;
        this.Price = price;
        this.Ingredients = new Dictionary<int, int>();
        this.Image = img;
    }
};

public class DatabaseScript : MonoBehaviour
{
    [SerializeField]
    Sprite[] itemImages;
    [SerializeField]
    Sprite[] recipeImages;
    
    [HideInInspector]
    public static int cookedCuisineNumber;
    [HideInInspector]
    public static int completedQuestNumber;
    [HideInInspector]
    public static int collectedRecipesNumber;

    public static List<Item> ITEMS;
    public static List<Recipe> RECIPES;
    public static Dictionary<int, int> Inventory_DB;        // 실제 인벤토리<아이템 코드, 소지하고 있는 갯수>
    public static int Money;                                        // 플레이어가 가진 돈
    
    bool dataLoaded;
    static int maxCapacity;

    // 아이템과 레시피 목록을 텍스트 파일에서 로드
    private void Start()
    {
        cookedCuisineNumber = 0;
        completedQuestNumber = 0;
        collectedRecipesNumber = 0;
        Money = 1000;

        dataLoaded = false;
        maxCapacity = 30;
        Inventory_DB = new Dictionary<int, int>();
        Add_Inventory(0, 2);
        Add_Inventory(1, 3);
        Add_Inventory(2, 4);
        Add_Inventory(3, 5);
        Add_Inventory(4, 10);

        if (!dataLoaded)
            Load();
    }
    private bool Load()
    {
        StreamReader sr;
        string input;
        int index = 0;
        ITEMS = new List<Item>();
        RECIPES = new List<Recipe>();
        sr = new StreamReader("ItemDatabase.txt");
        while (sr.EndOfStream == false)
        {
            input = sr.ReadLine();
            string[] subInput = input.Split('\t');
            Item tmp = new Item(subInput[0], subInput[2], Convert.ToInt32(subInput[1]), itemImages[index]);
            ITEMS.Add(tmp);
            index++;
        }
        sr.Close();

        index = 0;
        sr = new StreamReader("RecipeDatabase.txt");
        while (sr.EndOfStream == false)
        {
            input = sr.ReadLine();
            string[] subInput = input.Split('\t');
            Recipe tmp = new Recipe(subInput[0], subInput[1], Convert.ToInt32(subInput[2]),
                Convert.ToInt32(subInput[3]), Convert.ToInt32(subInput[4]), Convert.ToInt32(subInput[5]), recipeImages[index]);

            for (int i = 6; i < subInput.Length; i += 2)
            {
                tmp.ingredients[Convert.ToInt32(subInput[i])] = Convert.ToInt32(subInput[i + 1]);
            }
            RECIPES.Add(tmp);
            index++;
        }
        sr.Close();
        dataLoaded = true;
        return true;
    }
    
    public static void IncreaseCCN(int num)
    {
        cookedCuisineNumber += num;
    }
    public static void IncreaseCQN(int num)
    {
        completedQuestNumber += num;
    }
    public static void IncreaseCRN(int num)
    {
        collectedRecipesNumber += num;
    }


    // 카테고리 숫자에 맞는 레시피 목록 반환
    public static List<Recipe> RECIPES_byCategory(int cat)
    {
        List<Recipe> recipes = new List<Recipe>();
        foreach(var r in RECIPES)
        {
            if(r.category == cat)
            {
                recipes.Add(r);
            }
        }
        return recipes;
    }

    public static bool SearchItembyCode(ref Item item, int code)
    {
        foreach(var i in ITEMS)
        {
            if (i.code == code)
            {
                item.name = i.name;
                item.info = i.info;
                item.code = i.code;
                item.image = i.image;
                return true;
            }
        }
        return false;
    }

    public static bool SearchRecipebyCode(ref Recipe recipe, int code)
    {
        foreach(var r in RECIPES)
        {
            if(r.code == code)
            {
                recipe.name = r.name;
                recipe.info = r.info;
                recipe.earned = r.earned;
                recipe.price = r.price;
                recipe.ingredients = r.ingredients;
                recipe.image = r.image;
                return true;
            }
        }
        return false;
    }

    public static bool SearchRecipebyImage(ref Recipe recipe, Sprite image)
    {
        foreach (var r in RECIPES)
        {
            if (r.image == image)
            {
                recipe.code = r.code;
                recipe.name = r.name;
                recipe.info = r.info;
                recipe.earned = r.earned;
                recipe.price = r.price;
                recipe.ingredients = r.ingredients;
                return true;
            }
        }
        return false;
    }

    public static bool RecipeEarned(int code)
    {
        foreach(var r in RECIPES)
        {
            if (r.code == code && r.earned == 1)
                return true;
        }
        return false;
    }

    public static int ItemsLength()
    {
        return ITEMS.Count;
    }

    public static int RecipesLength_ALL()
    {
        return RECIPES.Count;
    }

    public static int RecipesLength_Chicken()
    {
        int num = 0;
        foreach(var r in RECIPES)
        {
            if (r.category == (int)Category.Chicken)
                num++;
        }
        return num;
    }

    public static int RecipesLength_Pork()
    {
        int num = 0;
        foreach (var r in RECIPES)
        {
            if (r.category == (int)Category.Pork)
                num++;
        }
        return num;
    }

    public static int RecipesLength_Beef()
    {
        int num = 0;
        foreach (var r in RECIPES)
        {
            if (r.category == (int)Category.Beef)
                num++;
        }
        return num;
    }

    public static int RecipesLength_Others()
    {
        int num = 0;
        foreach (var r in RECIPES)
        {
            if (r.category == (int)Category.Others)
                num++;
        }
        return num;
    }
    


    public static bool Add_Inventory(int code, int count)
    {
        if(SearchInventory(code) == -1)
        {
            if(Inventory_DB.Count >= InventoryScript.maxCapacity)
            {
                return false;
            }
            else
            {
                Inventory_DB[code] = count;
                return true;
            }
        }
        else
        {
            Inventory_DB[code] += count;
            return true;
        }
    }

    public static bool Delete_Inventory(int code, int count)
    {
        foreach (var i in Inventory_DB)
        {
            if (i.Key == code)
            {
                if (Inventory_DB[code] >= count)
                {
                    Inventory_DB[i.Key] -= count;
                    return true;
                }
                else
                    return false;
            }
        }
        return false;
    }

    // code값을 갖는 아이템이 인벤토리에 amount만큼 있는지 확인하는 함수
    public static bool CheckAmount_Inventory(int code, int amount)
    {
        foreach (var i in Inventory_DB)
        {
            if(i.Key == code && i.Value >= amount)
            {
                return true;
            }
        }
        return false;
    }

    // 인벤토리에 code값과 같은 아이템의 갯수를 반환, 아이템이 없으면 -1 반환
    public static int SearchInventory(int code)
    {
        foreach (var i in Inventory_DB)
        {
            if (i.Key == code)
                return i.Value;
        }
        return -1;
    }

    // 레시피의 코드값으로 인벤토리에 가지고 있는 아이템을 가지고 해당 레시피의 음식을 제작할 수 있는지 체크 ( 가능하면 true 반환 )
    public static bool CheckCookable(int code)
    {
        Recipe recipe = new Recipe();
        SearchRecipebyCode(ref recipe, code);
        foreach(var i in recipe.ingredients)
        {
            if(!CheckAmount_Inventory(i.Key, i.Value))
            {
                return false;
            }
        }
        return true;
    }
    
    public static void RenewalInventory(ref Dictionary<int, int> iv)
    {
        iv = Inventory_DB;
    }
}
