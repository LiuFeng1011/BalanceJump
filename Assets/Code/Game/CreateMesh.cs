using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour {

    public int meshWidth = 3;

    public float _InvWaveLength = 5;

    //定义顶点列表  
    Vector3[] verts;
    float[,] pointRate; 
    public Material mat;  

    MeshCollider mc;

    float time = 0;
	// Use this for initialization
	void Awake () {

        pointRate = new float[meshWidth, meshWidth];

        gameObject.name = "Wave";
        //meshHeight = meshWidth;
        mat = Resources.Load<Material>("Materials/Custom_Water");  
        mc = gameObject.AddComponent<MeshCollider>();  
  
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();    
        mr.material = mat;    

        DrawSquare();

        transform.localPosition = new Vector3(-(meshWidth * transform.localScale.x) * 0.5f, transform.localPosition.y, transform.localPosition.z);
	

        InGameColorManager colormanager = new InGameColorManager();
        colormanager.Init();

        mat.SetColor("_Color",colormanager.objColor1);
        mat.SetColor("_BackColor", colormanager.objColor2);
        //mat.color = colormanager.objColor1;
        Camera.main.backgroundColor = colormanager.bgColor;

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        List<Vector3> pl = new List<Vector3>(); 
        for (int i = 0; i < verts.Length; i ++){
            float x = 0f, y = 0f, z = 0f;
            float rate = pointRate[(int)verts[i].x , (int)verts[i].z];

            y = Mathf.Sin(rate * (time + 100) * 3f) * _InvWaveLength 
                     + Mathf.Sin((time +(verts[i].x + verts[i].y) * 0.4f) * 2)*0.5f;  
            x = verts[i].x + Mathf.Sin(rate * time) * 0.2f;
            z = verts[i].z + Mathf.Sin(rate * time) * 0.2f;
            pl.Add(new Vector3(x,y,z));
        }

        mc.sharedMesh.vertices = pl.ToArray();
        mc.sharedMesh.RecalculateNormals();  
	}


    void DrawSquare()
    {
        //创建mesh  
        Mesh mesh = gameObject.AddComponent<MeshFilter>().mesh;
        mesh.Clear();

        //uv列表  
        List<Vector2> uvList = new List<Vector2>();  

        List<Vector3> pointList = new List<Vector3>(); 
        //三角形数组  
        List<int> triangleList = new List<int>();  

        for (int i = 0; i < meshWidth; i ++){

            for (int j = 0; j < meshWidth; j++) {
                pointList.Add(new Vector3(i, 0, j));
                //设置前2个点的uv  
                float rate = Random.Range(0f, 1f);

                pointRate[i,j] = rate;

                if(i > 0 && j > 0){
                    int startindex = (i) * meshWidth + j; 
                    triangleList.Add(startindex - meshWidth - 1);  
                    triangleList.Add(startindex - meshWidth );  
                    triangleList.Add(startindex -1);

                    triangleList.Add(startindex);  
                    triangleList.Add(startindex - 1);  
                    triangleList.Add(startindex - meshWidth);  

                }
            }

        }  

        verts = new Vector3[triangleList.Count];//用于保存新的顶点信息  
  
        for (int i = 0; i < triangleList.Count; i++)  
        {  
            verts[i] = pointList[triangleList[i]];  
            triangleList[i] = i;  
            uvList.Add(new Vector2((float)verts[i].x / (float)meshWidth, (float)verts[i].y / (float)meshWidth));
        }  


        //把最终的顶点和三角形数组赋予mesh;  
        mesh.vertices = verts;  
        mesh.triangles = triangleList.ToArray();  
        mesh.uv = uvList.ToArray();  
        mesh.RecalculateNormals();  
  
        //把mesh赋予MeshCollider  
        mc.sharedMesh = mesh; 
    }
}
