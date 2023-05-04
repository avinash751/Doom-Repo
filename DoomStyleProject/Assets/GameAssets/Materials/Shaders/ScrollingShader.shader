Shader "Unlit/ScrollingShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_MyColor("Noise Color", Color) = (1,1,1,1)
		_AmbientColor("Ambient Color", Color) = (1,1,1,1)
		_ScrollingSpeed("Scroll Speed", float) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent" 
		"Queue" = "Transparent"}
		LOD 100

		Cull Back
		
		Blend One One
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
			#define TAU 6.2831855

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
				UNITY_FOG_COORDS(2)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _ScrollingSpeed;
			float4 _MyColor;
			float4 _AmbientColor;
			float _WaveAmp;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			fixed4 frag(v2f i) : SV_Target
			{
				i.uv.r += _Time.y * _ScrollingSpeed;
				float3 NdotL = max(dot(_WorldSpaceLightPos0, i.worldNormal),0);
				float3 ClampedNdotL = max(NdotL, 0);

				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 finalColor = _MyColor * _AmbientColor;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				float xOffset = i.uv.y;
				float2 centerUV = i.uv * 2 - 1; 
				float center = length(centerUV);
				float wave = cos((center + xOffset * TAU * 1));
				float ripple = cos((wave - _Time.x * 0.1) * TAU * 2);
				return float4(ClampedNdotL, 1) * col * (ripple * 10) * finalColor;
			}

		ENDCG
		}
	}
}
