using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Save
{
    public string stringData;
    public float floatData;
    public int intData;
    // ��ά����
    float[] pos = new float[3];
    // ��������
    public void PosSet(Vector3 vector3)
    {
        pos[0] = vector3.x;
        pos[1] = vector3.y;
        pos[2] = vector3.z;
    }
    // ��ȡ����
    public Vector3 PosGet()
    {
        return new Vector3(pos[0], pos[1], pos[2]);
    }


}

