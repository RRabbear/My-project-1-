using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Reflection;

using Assets.Scripts.BaseUtils;

//所有表数据的基类  存储的属性
public class TableDatabase
{
    public int ID;
}

//表格基类<存储的属性,具体表类>
public class ConfigTable<TDatabase> where TDatabase : TableDatabase , new()
{
    //id，数据条目
    public Dictionary<int, TDatabase> _cache = new Dictionary<int, TDatabase>();

    protected void load(string tablePath)
    {
        MemoryStream tableStream;

        //开发期，读Progect/Config下的csv文件
        var srcPath = Application.dataPath + "/../" + tablePath;
        tableStream = new MemoryStream(File.ReadAllBytes(srcPath));

        //内存流读取器 using 自动关闭流
        using (var reader = new StreamReader(tableStream, Encoding.GetEncoding("gb2312")))
        {
            //存储第一行的属性
            var fieldNameStr = reader.ReadLine();
            //，号分割各个属性
            var fieldNameArray = fieldNameStr.Split(',');
            //每个属性对应 所属于的类型  列表
            List<FieldInfo> allFieldInfo = new List<FieldInfo>();
            //遍历对应的TDatabase对应的表属性 所属的类型 存储到列表中
            foreach (var fieldName in fieldNameArray)
            {
                allFieldInfo.Add(typeof(TDatabase).GetField(fieldName));
            }

            //下面是正式数据
            var lineStr = reader.ReadLine();
            while (lineStr != null)
            {   //读取每一条数据 存储到缓存中
                TDatabase DataDB = readLine(allFieldInfo, lineStr);
                _cache[DataDB.ID] = DataDB;
                lineStr = reader.ReadLine();
            }
        }
    }

    /// <summary>
    /// 读取每行的数据 
    /// </summary>
    /// <param name="allFieldInfo">每条属性对应的类型列表</param>
    /// <param name="lineStr">一条数据</param>
    private static TDatabase readLine(List<FieldInfo> allFieldInfo, string lineStr)
    {
        //读到内存 （，分割当前行数据）
        var itemStrArray = lineStr.Split(',');
        var DataDB = new TDatabase();
        //对每个字段解析
        for (int i = 0; i < allFieldInfo.Count; ++i)
        {
            var field = allFieldInfo[i];//当前属性的类型
            var data = itemStrArray[i];//当前属性对应的具体数据
            //整数
            if (field.FieldType == typeof(int))
            {
                field.SetValue(DataDB, int.Parse(data));
            }
            //字符串
            else if (field.FieldType == typeof(string))
            {
                field.SetValue(DataDB, data);
            }
            //浮点型
            else if (field.FieldType == typeof(float))
            {
                field.SetValue(DataDB, float.Parse(data));
            }
            //布尔型
            else if (field.FieldType == typeof(bool))
            {
                //var v = int.Parse(data);
                //field.SetValue(DataDB, v != 0);
                field.SetValue(DataDB, bool.Parse(data));
            }
            //整数数组
            else if (field.FieldType == typeof(List<int>))
            {
                var list = new List<int>();
                //1$2$3$4$ 以$分割数组中的数据
                foreach (var itemStr in data.Split('$'))
                {
                    list.Add(int.Parse(itemStr));
                }
                field.SetValue(DataDB, list);
            }
            //浮点数数组
            else if (field.FieldType == typeof(List<float>))
            {
                var list = new List<float>();
                foreach (var itemStr in data.Split('$'))
                {
                    list.Add(float.Parse(itemStr));
                }
                field.SetValue(DataDB, list);
            }
            //字符串数组
            else if (field.FieldType == typeof(List<string>))
            {
                field.SetValue(DataDB, new List<string>(data.Split('$')));
            }//同一dll的Type类型
            else if (field.FieldType == typeof(Type))
            {
                //Type type = Type.GetType();
                //field.SetValue(DataDB, type);
            }
        }

        return DataDB;
    }

    //获取表格数据
    public TDatabase this[int index]
    {
        get
        {
            TDatabase db;
            _cache.TryGetValue(index, out db);
            return db;
        }
    }
    //得到整张表
    public Dictionary<int, TDatabase> GetAll()
    {
        return _cache;
    }

}

//角色表内数据结构
public class RoleDatabase : TableDatabase
{
    public string Name;//名称
    public string ModelPath;//模型路径
}

public class GameTableConfig : Singleton<GameTableConfig>
{
    //角色表
    public class RoleTable : ConfigTable<RoleDatabase, RoleTable>
    {
        void Awake()
        {
            //加载对应表所对应的路径
            load("Config/RoleTable.csv");
        }
    }
}

