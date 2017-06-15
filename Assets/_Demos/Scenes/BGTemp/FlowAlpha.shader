Shader "BlueBird/Demo/FlowAlpha" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TransFactor ("Lighting Factor", Range(0, 3)) = 1.0
		_TransCol ("Transparency Color", Color) = (0.1, 0, 0, 1)
		_TransVal ("Transparency Value", Range(0, 1)) = 0.5
		_ScrollXSpeed ("X Scroll Speed", Range(0, 10)) = 2
		_ScrollYSpeed ("Y Scroll Speed", Range(0, 10)) = 2
	}
	
	SubShader 
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200
		Cull Off Lighting Off ZWrite Off
		
		CGPROGRAM
		#pragma surface surf Direct alpha
		#pragma multi_compile_fog

		inline float4 LightingDirect(SurfaceOutput s, fixed3 lightDir, fixed atten) {
			float4 col;
			col.rgb = s.Albedo;
			col.a = s.Alpha;

			return col;
		}

		sampler2D _MainTex;
		float4 _TransCol;
		float _TransVal;
		float _TransFactor;
		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;

		struct Input 
		{
			float2 uv_MainTex;
			float fogCoord;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed2 scrolledUV = IN.uv_MainTex;
			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;
			scrolledUV += fixed2(xScrollValue, yScrollValue);
			half4 c = tex2D (_MainTex, scrolledUV);

			// half4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			UNITY_APPLY_FOG_COLOR(IN.fogCoord, c.rgb, fixed4(0,0,0,0)); // fog towards black due to our blend mode
			o.Albedo = c.rgb * _TransCol.rgb * _TransFactor;
			o.Alpha = c.a * _TransVal;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
