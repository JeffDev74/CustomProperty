using UnityEngine;
using System.Collections.Generic;

// Sqlite
using Mono.Data.Sqlite;
using System.Data;

namespace FPS
{
    public class SQLiteItemSerializer : MonoBehaviour
    {
        public static SQLiteItemSerializer instance;

        public string DB_PATH = "";
        public string DB_NAME = "";

        void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            DB_PATH = "StreamingAssets/Databases/";
            DB_NAME = "SerializedItems.bytes";
        }

        public string GetDBPath()
        {
            return "URI=file:" + Application.dataPath + "/StreamingAssets/Databases/SerializedItems.bytes";
        }

        public void CreateItem(IBaseData ItemData)
        {
            string _strDBName = GetDBPath();
            IDbConnection _connection = new SqliteConnection(_strDBName);
            IDbCommand _command = _connection.CreateCommand();
            string tableName = "items";
            string sql = "";

            _connection.Open();
            
            if (ItemData != null)
            {
                if (ItemData.Data is ISerializeData)
                {
                    ISerializeData itemSerializeDataInterface = ItemData.Data as ISerializeData;
                    if (itemSerializeDataInterface == null)
                    {
                        Debug.LogError("The external DB item data does not implement the interface ISerializeData");
                        return;
                    }

                    sql = string.Format("INSERT INTO " + tableName + " (item_uuid, type, data)" +
                    " VALUES ( \"{0}\", \"{1}\", \"{2}\");",
                    ItemData.Data.itemUUID,
                    ItemData.Data.Type,
                    itemSerializeDataInterface.SerializeItemData()
                    );
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                }
                else
                {
                    Debug.LogError("External DB item [" + ItemData.Data.ItemName + "] does not implement ISerializeData interface.");
                }

            }
            else
            {
                Debug.Log("The external DB item is null.");
            }

            _command.Dispose();
            _command = null;

            _connection.Close();
            _connection = null;
        }

        public BaseItem[] GetAllItems(string player_uuid = "")
        {
            List<BaseItem> items = new List<BaseItem>();

            string conn = GetDBPath();

            IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);

            dbconn.Open();

            #region DB Structure
            //CREATE TABLE `items` (
            // `id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            // `object_id`	TEXT DEFAULT '00000000-0000-0000-0000-000000000000',
            // `owner_id`	INTEGER NOT NULL,
            // `object_owner_id`	TEXT,
            // `type`	TEXT NOT NULL,
            // `data`	BLOB NOT NULL,
            // `position`	BLOB NOT NULL,
            // `rotation`	BLOB NOT NULL
            //);
            #endregion DB Structure

            IDbCommand dbcmd = dbconn.CreateCommand();
            string sql = "";
            if (string.IsNullOrEmpty(player_uuid))
            {
                sql = "SELECT id, item_uuid, type, data " + "FROM items;";
            }
            else
            {
                sql = "SELECT id, item_uuid, type, data " + "FROM items WHERE player_uuid=\"" + player_uuid + "\";";
            }

            dbcmd.CommandText = sql;

            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                //int id = reader.GetInt32(0);
                //string item_uuid = reader.GetString(1);
                //string type = reader.GetString(2);
                string data = reader.GetString(3);
                
                var newData = Helper.FactoreData<BaseData>(data);

                BaseItem extItemDB = null;
                if (newData != null)
                {
                    ISerializeData iSerializeInterface = newData as ISerializeData;
                    
                    if (iSerializeInterface != null)
                    {
                        extItemDB = iSerializeInterface.FactoryCloneItemFromData();
                    }
                    else
                    {
                        Debug.Log("The external DB item data does not implement the ISerializable interface");
                    }
                }

                if (items.Contains(extItemDB) == false && extItemDB != null)
                {
                    items.Add(extItemDB);
                }
                else
                {
                    Debug.LogError("Trying to add a duplicated external DB item skipping.");
                }
            }

            reader.Close();
            reader = null;

            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;

            return items.ToArray();
        }

        public void UpdateItem(string itemUUID, IBaseData itemDBData)
        {
            string sql = "";
            string tableName = "items";
            string conn = GetDBPath();
            IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();

            ISerializeData itemSerializeDataInterface = null;
            
            if (itemDBData.Data is ISerializeData)
            {
                itemSerializeDataInterface = itemDBData.Data as ISerializeData;
                
                if (itemSerializeDataInterface == null)
                {
                    Debug.LogError("The external DB item data does not implement the interface ISerializeData");
                    return;
                }
            }


            #region DB Structure
            //CREATE TABLE `items` (
            // `id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            // `object_id`	TEXT DEFAULT '00000000-0000-0000-0000-000000000000',
            // `owner_id`	INTEGER NOT NULL,
            // `object_owner_id`	TEXT,
            // `type`	TEXT NOT NULL,
            // `data`	BLOB NOT NULL,
            // `position`	BLOB NOT NULL,
            // `rotation`	BLOB NOT NULL,
            // `timeout` INTEGER
            //);
            #endregion DB Structure

            // should i add player uuid on the where statment????
            sql = string.Format("UPDATE \"" + tableName + "\" SET data=\"{0}\" WHERE item_uuid=\"{2}\";",
            itemSerializeDataInterface.SerializeItemData(), itemUUID);

            dbcmd.CommandText = sql;
            dbcmd.ExecuteNonQuery();

            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;
        }

        public void UpdateItem(string itemUUID, IBaseData[] itemDBData)
        {
            for (int i = 0; i < itemDBData.Length; i++)
            {
                UpdateItem(itemUUID, itemDBData[i]);
            }
        }

        public void DeleteItem(int externalDB_id)
        {
            string sql = "";
            string tableName = "items";
            string conn = GetDBPath();
            IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            sql = "DELETE FROM " + tableName + " WHERE item_uuid = " + externalDB_id + ";";

            dbcmd.CommandText = sql;
            dbcmd.ExecuteNonQuery();

            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;
        }

        public void DeleteItem(string itemUUID)
        {
            string sql = "";
            string tableName = "items";
            string conn = GetDBPath();
            IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            sql = "DELETE FROM " + tableName + " WHERE item_uuid = \"" + itemUUID + "\";";

            dbcmd.CommandText = sql;
            dbcmd.ExecuteNonQuery();


            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;
        }
    }
}