// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "GameCore/SpecialEffect/Reflection Mirror"  
{  
    Properties {  
        _ReflectionTex ("Reflection", 2D) = "white" {TexGen ObjectLinear }  
        _ReflectionColor("Color",Color) = (1,1,1,1)  
    }  
    //PC  
    SubShader {  
        Tags {  
            "RenderType"="Opaque"}  
        LOD 100  
        Pass {  
            CGPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag  
            #include "UnityCG.cginc"  
  
            uniform float4x4 _ProjMatrix;  
            uniform sampler2D _ReflectionTex;  
            float4 _ReflectionColor;  
            struct outvertex {  
                float4 pos : SV_POSITION;  
                float4 uv : TEXCOORD0;  
            };  
            outvertex vert(appdata_tan v) {  
                outvertex o;  
                o.pos = UnityObjectToClipPos (v.vertex);                  
                float3 viewDir = ObjSpaceViewDir(v.vertex);  
                o.uv = mul(_ProjMatrix,float4(viewDir,0));  
                return o;  
            }  
                                      
            float4 frag(outvertex i) : COLOR {  
                half4 reflcol = tex2Dproj(_ReflectionTex,i.uv);               
                return reflcol*_ReflectionColor;  
            }  
            ENDCG  
        }  
    }  
}  