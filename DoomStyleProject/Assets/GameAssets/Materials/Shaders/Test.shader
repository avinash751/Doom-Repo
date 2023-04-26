Shader "Custom/Dissolve" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Cutoff("Alpha cutoff", Range(0,1)) = 0.5
        _DissolveAmount("Dissolve amount", Range(0,1)) = 0.0
        _DissolveColor("Dissolve color", Color) = (1,1,1,1)
        _DissolveEdge("Dissolve edge", Range(0,0.2)) = 0.1
        _DissolveSpeed("Dissolve speed", Range(0,10)) = 1.0
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
            LOD 100

            CGPROGRAM
            #pragma surface surf Standard alpha

            sampler2D _MainTex;
            float _Cutoff;
            float _DissolveAmount;
            float4 _DissolveColor;
            float _DissolveEdge;
            float _DissolveSpeed;

            struct Input {
                float2 uv_MainTex;
                float3 worldPos;
                float3 worldNormal;
            };

            void surf(Input IN, inout SurfaceOutputStandard o) {
                // Calculate dissolve value
                float dissolveValue = smoothstep(_DissolveAmount - _DissolveEdge, _DissolveAmount, sin(_Time.y * _DissolveSpeed) + IN.worldPos.x);

                // Set color
                o.Albedo = lerp(tex2D(_MainTex, IN.uv_MainTex).rgb, _DissolveColor.rgb, dissolveValue);

                // Set alpha
                o.Alpha = (o.Albedo.a < _Cutoff) ? 0 : o.Alpha;
            }
            ENDCG
        }

            FallBack "Diffuse"
}