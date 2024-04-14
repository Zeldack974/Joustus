Shader "Custom/5ColorSwap"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Tolerance("Tolerance", Range(0, 1)) = 0.01
        _TargetColor1("Target Color 1", Color) = (1,1,1,1)
        _ReplaceColor1("Replace Color 1", Color) = (1,1,1,1)
        _TargetColor2("Target Color 2", Color) = (1,1,1,1)
        _ReplaceColor2("Replace Color 2", Color) = (1,1,1,1)
        _TargetColor3("Target Color 3", Color) = (1,1,1,1)
        _ReplaceColor3("Replace Color 3", Color) = (1,1,1,1)
        _TargetColor4("Target Color 4", Color) = (1,1,1,1)
        _ReplaceColor4("Replace Color 4", Color) = (1,1,1,1)
        _TargetColor5("Target Color 5", Color) = (1,1,1,1)
        _ReplaceColor5("Replace Color 5", Color) = (1,1,1,1)
    }
 
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
 
        Cull Off
        Pass
        {
            CGPROGRAM
            // Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
            #pragma exclude_renderers gles
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Tolerance;
            
            float4 _TargetColor1;
            float4 _ReplaceColor1;
            float4 _TargetColor2;
            float4 _ReplaceColor2;
            float4 _TargetColor3;
            float4 _ReplaceColor3;
            float4 _TargetColor4;
            float4 _ReplaceColor4;
            float4 _TargetColor5;
            float4 _ReplaceColor5;
 
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            int RandomInt(float2 uv) {
                return (int)(frac(sin(dot(uv, float2(12.9898,78.233))) * 43758.5453) * 5) + 1;
            }
 
            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);

                if (col.a == 0)
                {
                    return half4(0, 0, 0, 0);
                }

                // Liste des remplacements de couleurs
                float4 colorSwaps[5][2] = {
                    {_TargetColor1, _ReplaceColor1},
                    {_TargetColor2, _ReplaceColor2},
                    {_TargetColor3, _ReplaceColor3},
                    {_TargetColor4, _ReplaceColor4},
                    {_TargetColor5, _ReplaceColor5},
                };

                // int randomInt = RandomInt(i.uv);

                // return half4(colorSwaps[randomInt][1].rgb, col.a);


                for (int j = 0; j < 5; j++) {
                    if (length(col.rgb - colorSwaps[j][0].rgb) < _Tolerance) {
                        return half4(colorSwaps[j][1].rgb, col.a);
                    }
                }

                return col;
            }
 
            ENDCG
        }
 
 
    }
}