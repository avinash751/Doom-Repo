Shader "Unlit/Another Shader"
{
	Properties
	{
		_MainTex("Gradient", Gradient) = "white" {}
		_MyColor("Noise Color", Color) = (1,1,1,1)
		_AmbientColor("Ambient Color", Color) = (1,1,1,1)
		_ScrollingSpeed("Scroll Speed", float) = 1
	}
SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

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

				samplerGradient _MainTex;
				float _ScrollingSpeed;
				float4 _MyColor;
				float4 _AmbientColor;

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
					float3 worldPos = mul(unity_ObjectToWorld, i.vertex).xyz;
					float3 worldPosPlusNormal = worldPos + UnityObjectToWorldNormal(i.normal);
					float2 gradientCoords = float2(length(worldPos), length(worldPosPlusNormal));

					gradientCoords.r += _Time.y * _ScrollingSpeed;

					float3 NdotL = max(dot(_WorldSpaceLightPos0, i.worldNormal), 0);
					float3 ClampedNdotL = max(NdotL, 0);

					fixed4 col = tex2Dgrad(_MainTex, gradientCoords, ddx(gradientCoords), ddy(gradientCoords));
					fixed4 finalColor = _MyColor * _AmbientColor;

					// apply fog
					UNITY_APPLY_FOG(i.fogCoord, col);
					return float4(ClampedNdotL.rgb, 1) * col * finalColor;
				}
				ENDCG
			}
		}
}
