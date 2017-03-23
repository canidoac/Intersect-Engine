﻿using System.Collections.Generic;
using System.Linq;

namespace Intersect.GameObjects
{
    public class PlayerVariableBase : DatabaseObject<PlayerVariableBase>
    {
        //Core info
        public new const string DATABASE_TABLE = "player_variables";
        public new const GameObject OBJECT_TYPE = GameObject.PlayerVariable;
        protected static Dictionary<int, DatabaseObject> Objects = new Dictionary<int, DatabaseObject>();

        public PlayerVariableBase(int id) : base(id)
        {
            Name = "New Player Variable";
        }

        public override void Load(byte[] packet)
        {
            var myBuffer = new ByteBuffer();
            myBuffer.WriteBytes(packet);
            Name = myBuffer.ReadString();
            myBuffer.Dispose();
        }

        public byte[] Data()
        {
            var myBuffer = new ByteBuffer();
            myBuffer.WriteString(Name);
            return myBuffer.ToArray();
        }

        public static PlayerVariableBase GetVariable(int index)
        {
            if (Objects.ContainsKey(index))
            {
                return (PlayerVariableBase) Objects[index];
            }
            return null;
        }

        public static string GetName(int index)
        {
            if (Objects.ContainsKey(index))
            {
                return ((PlayerVariableBase) Objects[index]).Name;
            }
            return "Deleted";
        }

        public override byte[] BinaryData => Data();

        public override string DatabaseTableName
        {
            get { return DATABASE_TABLE; }
        }

        public override GameObject GameObjectType
        {
            get { return OBJECT_TYPE; }
        }

        public static DatabaseObject Get(int index)
        {
            if (Objects.ContainsKey(index))
            {
                return Objects[index];
            }
            return null;
        }

        public override void Delete()
        {
            Objects.Remove(Id);
        }

        public static void ClearObjects()
        {
            Objects.Clear();
        }

        public static void AddObject(int index, DatabaseObject obj)
        {
            Objects.Remove(index);
            Objects.Add(index, obj);
        }

        public static int ObjectCount()
        {
            return Objects.Count;
        }

        public static Dictionary<int, PlayerVariableBase> GetObjects()
        {
            Dictionary<int, PlayerVariableBase> objects = Objects.ToDictionary(k => k.Key,
                v => (PlayerVariableBase) v.Value);
            return objects;
        }
    }
}