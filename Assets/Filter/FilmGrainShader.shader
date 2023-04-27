Shader "Custom/FilmGrainShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _GrainIntensity("Grain Intensity", Range(0, 10)) = 0.5
        _GrainSize("Grain Size", Range(1, 100)) = 50
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100

            CGPROGRAM
            #pragma surface surf Lambert

            sampler2D _MainTex;
            float _GrainIntensity;
            float _GrainSize;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                float grain = (tex2D(_MainTex, IN.uv_MainTex * _GrainSize).r - 0.5) * _GrainIntensity;
                o.Albedo = c.rgb + grain;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
