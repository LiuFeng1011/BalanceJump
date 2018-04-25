Shader "Custom/Water" {  
    Properties {  
        _Color ("Color", Color) = (1,1,1,1)  
        _BackColor ("BackColor", Color) = (1,1,1,1)  
    }  
    SubShader {  
        Tags{"IgnoreProjector"="True"  "Queue"="Transparent" "RenderType"="Transparent"}  
          
        Pass{  
            CGPROGRAM  
 
            #pragma vertex vert  
            //#pragma geometry geom  
            #pragma fragment frag  
 
            #include "Lighting.cginc"  
    
            fixed4 _Color;  
            fixed4 _BackColor;
  
            struct a2v{  
                float4 vertex : POSITION;  
                float3 normal : NORMAL;  
                float4 texcoord : TEXCOORD0;  
            };  
  
            struct v2f {  
                float4 pos : SV_POSITION;  
                float3 worldNormal : TEXCOORD0;  
                float3 worldPos : TEXCOORD1;  
                float4 color : TEXCOORD2; 
            };  
  
            v2f vert(a2v v) {  
                v2f o;  
                o.pos = UnityObjectToClipPos(v.vertex); 
                o.worldNormal = UnityObjectToWorldNormal(v.normal);  

                o.worldPos =  mul(unity_ObjectToWorld,v.vertex).xyz;  

                o.color = float4(v.texcoord.x,0,0,1);
                return o;  
            }  
  
            fixed4 frag(v2f i) : SV_Target {  

                float3 viewDirection = normalize(_WorldSpaceCameraPos - i.worldPos);

                //获取法线方向  
                fixed3 worldNormal = normalize(i.worldNormal);  

                float r = dot(viewDirection,worldNormal);

                //float d = dot(worldLightDir,worldNormal);
                return  lerp(_Color,_BackColor,r) ;
            }  
            ENDCG  
        }  
    }  
    FallBack "Transparent/VertexLit"  
}  