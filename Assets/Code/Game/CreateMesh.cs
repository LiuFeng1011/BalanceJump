using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour {

    public int meshWidth = 3;

    public Material mat;  
    MeshCollider mc;  
	// Use this for initialization
	void Awake () {
        //meshHeight = meshWidth;
        mat = Resources.Load<Material>("Materials/Custom_Water");  
        mc = gameObject.AddComponent<MeshCollider>();  
  
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();    
        mr.material = mat;    

        DrawSquare();

        transform.localPosition = new Vector3(-(meshWidth * transform.localScale.x) / 2, transform.localPosition.y, transform.localPosition.z);
	

        InGameColorManager colormanager = new InGameColorManager();
        colormanager.Init();

        mat.SetColor("_Color", colormanager.objColor1);
        mat.SetColor("_BackColor", colormanager.objColor2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void DrawSquare()
    {
        //创建mesh  
        Mesh mesh = gameObject.AddComponent<MeshFilter>().mesh;
        mesh.Clear();

        //定义顶点列表  
        List<Vector3> pointList = new List<Vector3>();  

        //uv列表  
        List<Vector2> uvList = new List<Vector2>();  

        //三角形数组  
        List<int> triangleList = new List<int>();  

        for (int i = 0; i < meshWidth; i ++){

            for (int j = 0; j < meshWidth; j++) {
                pointList.Add(new Vector3(i, 0, j)); 
                //设置前2个点的uv  
                uvList.Add(new Vector2((float)i/ (float)meshWidth, (float)j/(float)meshWidth));

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

        //把最终的顶点和三角形数组赋予mesh;  
        mesh.vertices = pointList.ToArray();  
        mesh.triangles = triangleList.ToArray();  
        mesh.uv = uvList.ToArray();  
        mesh.RecalculateNormals();  
  
        //把mesh赋予MeshCollider  
        mc.sharedMesh = mesh; 
    }
}
