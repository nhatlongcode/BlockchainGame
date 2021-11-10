﻿using UnityEngine;
using System;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using System.Reflection;
using System.Collections.Generic;

public partial class Serializer
{

    //*
    public static void RegisterCustomType(byte code, Type type)
    {
        MethodInfo[] methodInfos = type.GetMethods();

        Func<object, byte[]> serializeMethod = null;
        Func<byte[], object> deserializeMethod = null;

        foreach (MethodInfo methodInfo in methodInfos)
        {
            if (methodInfo.Name == "Serialize")
            {
                if (!methodInfo.IsStatic)
                {
                    Debug.LogError(string.Format("Serialize method must be static! Registering custom type \"{0}\" failed.", type.ToString()));
                    return;
                }

                serializeMethod = (Func<object, byte[]>)methodInfo.CreateDelegate(typeof(Func<object, byte[]>));
            }
            if (methodInfo.Name == "Deserialize")
            {
                if (!methodInfo.IsStatic)
                {
                    Debug.LogError(string.Format("Deserialize method must be static! Registering custom type \"{0}\" failed.", type.ToString()));
                    return;
                }

                deserializeMethod = (Func<byte[], object>)methodInfo.CreateDelegate(typeof(Func<byte[], object>));
            }
        };

        if (serializeMethod == null)
        {
            Debug.LogError(string.Format("There is no serialize method! Registering custom type \"{0}\" failed.", type.ToString()));
            return;
        }

        if (deserializeMethod == null)
        {
            Debug.LogError(string.Format("There is no serialize method! Registering custom type \"{0}\" failed.", type.ToString()));
            return;
        }

        PhotonPeer.RegisterType(
            type,
            code,
            x => serializeMethod(x),
            x => deserializeMethod(x)
       );
    }
    //*/

    public static byte[] JoinBytes(params byte[][] bytes)
    {
        byte[] rv = new byte[bytes.Sum(x => x.Length)];
        int offset = 0;

        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i].CopyTo(rv, offset);
            offset += bytes[i].Length;
        }

        return rv;
    }

    public static void JoinBytes(ref byte[] bytesArray, byte[] joinBytes)
    {
        bytesArray = bytesArray.Concat(joinBytes).ToArray();
    }

    public static void JoinBytes(ref byte[] bytesArray, params byte[][] joinBytes)
    {
        for (int i = 0; i < joinBytes.Length; i++)
        {
            bytesArray = bytesArray.Concat(joinBytes[i]).ToArray();
        }
    }

    #region Serialize
    public static void Serialize(dynamic value, ref byte[] bytes, bool dynamicLoad)
    {
        Serialize(value, ref bytes);
    }

    public delegate object Deserializable(byte[] bytes, ref int offset);
    public static Dictionary<Type, Deserializable> deserializeDictionary = new Dictionary<Type, Deserializable>();
    static Serializer()
    {
        deserializeDictionary[typeof(int)] = (byte[] bytes, ref int offset) => DeserializeInt(bytes, ref offset);
        deserializeDictionary[typeof(float)] = (byte[] bytes, ref int offset) => DeserializeFloat(bytes, ref offset);
        deserializeDictionary[typeof(string)] = (byte[] bytes, ref int offset) => DeserializeString(bytes, ref offset);
        deserializeDictionary[typeof(Vector2)] = (byte[] bytes, ref int offset) => DeserializeVector2(bytes, ref offset);
        deserializeDictionary[typeof(Vector3)] = (byte[] bytes, ref int offset) => DeserializeVector3(bytes, ref offset);
        deserializeDictionary[typeof(Quaternion)] = (byte[] bytes, ref int offset) => DeserializeQuaternion(bytes, ref offset);
        deserializeDictionary[typeof(bool)] = (byte[] bytes, ref int offset) => DeserializeBool(bytes, ref offset);
        deserializeDictionary[typeof(int[])] = (byte[] bytes, ref int offset) => DeserializeIntArray(bytes, ref offset);
        deserializeDictionary[typeof(float[])] = (byte[] bytes, ref int offset) => DeserializeFloatArray(bytes, ref offset);
        deserializeDictionary[typeof(string[])] = (byte[] bytes, ref int offset) => DeserializeStringArray(bytes, ref offset);
        deserializeDictionary[typeof(Vector2[])] = (byte[] bytes, ref int offset) => DeserializeVector2Array(bytes, ref offset);
        deserializeDictionary[typeof(Vector3[])] = (byte[] bytes, ref int offset) => DeserializeVector3Array(bytes, ref offset);
        deserializeDictionary[typeof(Quaternion[])] = (byte[] bytes, ref int offset) => DeserializeQuaternionArray(bytes, ref offset);
        deserializeDictionary[typeof(bool[])] = (byte[] bytes, ref int offset) => DeserializeBoolArray(bytes, ref offset);
    }

    public static T Deserialize<T>(byte[] bytes, ref int offset)
    {
        if (!deserializeDictionary.ContainsKey(typeof(T)))
            return default;
        return (T)deserializeDictionary[typeof(T)].Invoke(bytes, ref offset);
    }

    public static void Serialize(int value, ref byte[] bytes)
    {
        byte[] _bytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(_bytes);

        JoinBytes(ref bytes, _bytes);
    }

    public static void Serialize(float value, ref byte[] bytes)
    {
        byte[] _bytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(_bytes);

        JoinBytes(ref bytes, _bytes);
    }

    public static void Serialize(bool value, ref byte[] bytes)
    {
        byte[] _bytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(_bytes);

        JoinBytes(ref bytes, _bytes);
    }

    public static void Serialize(Vector3 value, ref byte[] bytes)
    {
        byte[] x = BitConverter.GetBytes(value.x);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(x);

        byte[] y = BitConverter.GetBytes(value.y);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(y);

        byte[] z = BitConverter.GetBytes(value.z);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(z);

        JoinBytes(ref bytes, x, y, z);
    }

    public static void Serialize(Vector2 value, ref byte[] bytes)
    {
        byte[] x = BitConverter.GetBytes(value.x);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(x);

        byte[] y = BitConverter.GetBytes(value.y);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(y);

        JoinBytes(ref bytes, x, y);
    }

    public static void Serialize(Quaternion value, ref byte[] bytes)
    {
        byte[] x = BitConverter.GetBytes(value.x);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(x);

        byte[] y = BitConverter.GetBytes(value.y);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(y);

        byte[] z = BitConverter.GetBytes(value.z);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(z);

        byte[] w = BitConverter.GetBytes(value.w);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(w);

        JoinBytes(ref bytes, x, y, z, w);
    }

    public static void Serialize(string value, ref byte[] bytes)
    {
        if (string.IsNullOrEmpty(value))
        {
            Serialize(0, ref bytes);
            return;
        }

        byte[] stringBytes = Encoding.UTF8.GetBytes(value);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(stringBytes);

        byte[] lengthBytes = BitConverter.GetBytes(stringBytes.Length);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(lengthBytes);

        JoinBytes(ref bytes, lengthBytes, stringBytes);
    }

    public static void Serialize(int[] values, ref byte[] bytes)
    {
        int length = values == null ? 0 : values.Length;
        if (length > 0)
        {
            Serialize(length, ref bytes);
            for (int i = 0; i < values.Length; i++)
            {
                Serialize(values[i], ref bytes);
            }
        }
        else
        {
            Serialize(0, ref bytes);
        }
    }

    public static void Serialize(float[] values, ref byte[] bytes)
    {
        int length = values == null ? 0 : values.Length;
        if (length > 0)
        {
            Serialize(length, ref bytes);
            for (int i = 0; i < values.Length; i++)
            {
                Serialize(values[i], ref bytes);
            }
        }
        else
        {
            Serialize(0, ref bytes);
        }
    }

    public static void Serialize(bool[] values, ref byte[] bytes)
    {
        int length = values == null ? 0 : values.Length;
        if (length > 0)
        {
            Serialize(length, ref bytes);
            for (int i = 0; i < values.Length; i++)
            {
                Serialize(values[i], ref bytes);
            }
        }
        else
        {
            Serialize(0, ref bytes);
        }
    }

    public static void Serialize(Vector2[] values, ref byte[] bytes)
    {
        int length = values == null ? 0 : values.Length;
        if (length > 0)
        {
            Serialize(length, ref bytes);
            for (int i = 0; i < values.Length; i++)
            {
                Serialize(values[i], ref bytes);
            }
        }
        else
        {
            Serialize(0, ref bytes);
        }
    }

    public static void Serialize(Vector3[] values, ref byte[] bytes)
    {
        int length = values == null ? 0 : values.Length;
        if (length > 0)
        {
            Serialize(length, ref bytes);
            for (int i = 0; i < values.Length; i++)
            {
                Serialize(values[i], ref bytes);
            }
        }
        else
        {
            Serialize(0, ref bytes);
        }
    }

    public static void Serialize(Quaternion[] values, ref byte[] bytes)
    {
        int length = values == null ? 0 : values.Length;
        if (length > 0)
        {
            Serialize(length, ref bytes);
            for (int i = 0; i < values.Length; i++)
            {
                Serialize(values[i], ref bytes);
            }
        }
        else
        {
            Serialize(0, ref bytes);
        }
    }

    public static void Serialize(string[] values, ref byte[] bytes)
    {
        int length = values == null ? 0 : values.Length;
        if (length > 0)
        {
            Serialize(length, ref bytes);
            for (int i = 0; i < values.Length; i++)
            {
                Serialize(values[i], ref bytes);
            }
        }
        else
        {
            Serialize(0, ref bytes);
        }
    }
    #endregion

    #region Deserialize
    public static int DeserializeInt(byte[] bytes, ref int offset)
    {
        byte[] _bytes = new byte[4];
        Array.Copy(bytes, offset, _bytes, 0, 4);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(_bytes);

        offset += 4;
        return BitConverter.ToInt32(_bytes, 0);

        // int, float, bool vecot3, ve2 ,qua, string
    }

    public static float DeserializeFloat(byte[] bytes, ref int offset)
    {
        byte[] _bytes = new byte[4];
        Array.Copy(bytes, offset, _bytes, 0, 4);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(_bytes);

        offset += 4;
        return BitConverter.ToSingle(_bytes, 0);
    }

    public static bool DeserializeBool(byte[] bytes, ref int offset)
    {
        byte[] _bytes = new byte[1];
        Array.Copy(bytes, offset, _bytes, 0, 1);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(_bytes);

        offset += 1;
        return BitConverter.ToBoolean(_bytes, 0);
    }

    public static Vector3 DeserializeVector3(byte[] bytes, ref int offset)
    {
        byte[] _xBytes = new byte[4];
        byte[] _yBytes = new byte[4];
        byte[] _zBytes = new byte[4];

        Array.Copy(bytes, offset, _xBytes, 0, 4);
        Array.Copy(bytes, offset + 4, _yBytes, 0, 4);
        Array.Copy(bytes, offset + 8, _zBytes, 0, 4);

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(_xBytes);
            Array.Reverse(_yBytes);
            Array.Reverse(_zBytes);
        }

        Vector3 o = new Vector3();
        o.x = BitConverter.ToSingle(_xBytes, 0);
        o.y = BitConverter.ToSingle(_yBytes, 0);
        o.z = BitConverter.ToSingle(_zBytes, 0);

        offset += 12;
        return o;
    }

    public static Vector2 DeserializeVector2(byte[] bytes, ref int offset)
    {
        byte[] _xBytes = new byte[4];
        byte[] _yBytes = new byte[4];

        Array.Copy(bytes, offset, _xBytes, 0, 4);
        Array.Copy(bytes, offset + 4, _yBytes, 0, 4);

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(_xBytes);
            Array.Reverse(_yBytes);
        }

        Vector2 o = new Vector2();
        o.x = BitConverter.ToSingle(_xBytes, 0);
        o.y = BitConverter.ToSingle(_yBytes, 0);

        offset += 8;
        return o;
    }

    public static Quaternion DeserializeQuaternion(byte[] bytes, ref int offset)
    {
        byte[] _xBytes = new byte[4];
        byte[] _yBytes = new byte[4];
        byte[] _zBytes = new byte[4];
        byte[] _wBytes = new byte[4];

        Array.Copy(bytes, offset, _xBytes, 0, 4);
        Array.Copy(bytes, offset + 4, _yBytes, 0, 4);
        Array.Copy(bytes, offset + 8, _zBytes, 0, 4);
        Array.Copy(bytes, offset + 12, _wBytes, 0, 4);

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(_xBytes);
            Array.Reverse(_yBytes);
            Array.Reverse(_zBytes);
            Array.Reverse(_wBytes);
        }

        Quaternion o = new Quaternion();
        o.x = BitConverter.ToSingle(_xBytes, 0);
        o.y = BitConverter.ToSingle(_yBytes, 0);
        o.z = BitConverter.ToSingle(_zBytes, 0);
        o.w = BitConverter.ToSingle(_wBytes, 0);

        offset += 16;
        return o;
    }

    public static string DeserializeString(byte[] bytes, ref int offset)
    {
        int length = DeserializeInt(bytes, ref offset);
        if (length > 0)
        {
            byte[] _bytes = new byte[length];
            Array.Copy(bytes, offset, _bytes, 0, length);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(_bytes);

            offset += length;
            return Encoding.UTF8.GetString(_bytes);
        }

        return "";
    }

    public static int[] DeserializeIntArray(byte[] bytes, ref int offset)
    {
        int length = DeserializeInt(bytes, ref offset);
        if (length > 0)
        {
            int[] array = new int[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = DeserializeInt(bytes, ref offset);
            }

            return array;
        }

        return new int[0];
    }

    public static float[] DeserializeFloatArray(byte[] bytes, ref int offset)
    {
        int length = DeserializeInt(bytes, ref offset);
        if (length > 0)
        {
            float[] array = new float[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = DeserializeFloat(bytes, ref offset);
            }

            return array;
        }

        return new float[0];
    }

    public static bool[] DeserializeBoolArray(byte[] bytes, ref int offset)
    {
        int length = DeserializeInt(bytes, ref offset);
        if (length > 0)
        {
            bool[] array = new bool[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = DeserializeBool(bytes, ref offset);
            }

            return array;
        }

        return new bool[0];
    }

    public static Vector3[] DeserializeVector3Array(byte[] bytes, ref int offset)
    {
        int length = DeserializeInt(bytes, ref offset);
        if (length > 0)
        {
            Vector3[] array = new Vector3[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = DeserializeVector3(bytes, ref offset);
            }

            return array;
        }

        return new Vector3[0];
    }

    public static Vector2[] DeserializeVector2Array(byte[] bytes, ref int offset)
    {
        int length = DeserializeInt(bytes, ref offset);
        if (length > 0)
        {
            Vector2[] array = new Vector2[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = DeserializeVector2(bytes, ref offset);
            }

            return array;
        }

        return new Vector2[0];
    }

    public static Quaternion[] DeserializeQuaternionArray(byte[] bytes, ref int offset)
    {
        int length = DeserializeInt(bytes, ref offset);
        if (length > 0)
        {
            Quaternion[] array = new Quaternion[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = DeserializeQuaternion(bytes, ref offset);
            }

            return array;
        }

        return new Quaternion[0];
    }

    public static string[] DeserializeStringArray(byte[] bytes, ref int offset)
    {
        int length = DeserializeInt(bytes, ref offset);
        if (length > 0)
        {
            string[] array = new string[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = DeserializeString(bytes, ref offset);
            }

            return array;
        }

        return new string[0];
    }
    #endregion
}
