Shader "Unlit/PhongShader-Toon"
{

    Properties
    {
        _Color ("Object Color", Color) = (0, 0.6477987, 0.6163521, 1)
        _ShadowColor("ShadowColor", Color) = (0.4133144, 0.268759, 0.6729559, 1)
        _OutLineColor("OutLineColor", Color) = (0, 0, 0, 1)
        _ReflectionLevel("Reflection Level", Range(0, 10)) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float4 worldPos : TEXCOORD0;
            };
            
            float4 _Color;
            float _ReflectionLevel;
            float4 _ShadowColor;
            float4 _OutLineColor;
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); //頂点をMVP行列変換
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); //各頂点のワールド座標を代入
                o.normal = UnityObjectToWorldNormal(v.normal); //各頂点が持つ法線（オブジェクト座標系）をワールド座標系に変換
                
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                float4 finalColor = _Color;
                float3 ligDirection = normalize(_WorldSpaceLightPos0.xyz); //シーンのディレクショナルライト方向を取得
                fixed3 ligColor = _LightColor0.xyz; //ディレクショナルライトのカラーを取得

                float3 refVec = reflect(-ligDirection, normalize(i.normal)); //ライト方向と法線方向から反射ベクトルを計算

                float3 toEye = _WorldSpaceCameraPos - i.worldPos; //カメラからの視線ベクトルを計算
                toEye = normalize(toEye); //視線ベクトルを正規化

                float t = dot(refVec, toEye); //反射ベクトルと視線ベクトルで内積を計算
                float d = dot(i.normal, ligDirection);
                float c = dot(i.normal, toEye);
                t = max(0, t); //計算した内積のうち、t < 0は必要ないのでクランプ
                if (d < 0)
                {
                    finalColor = _ShadowColor;
                    return finalColor;
                }
                float diffLevel = d;
                if (diffLevel > 0.6f)
                {
                    diffLevel = 1.0f;
                }
                else
                {
                    diffLevel = 0.1f;
                }
                
                float3 diff = ligColor * diffLevel;

                t = pow(t, _ReflectionLevel); //反射の絞りを調整

                float spec = 0.0f;
                if (t > 0.9f)
                {
                    spec = 0.6f;
                }

                finalColor.xyz *= diffLevel;
                finalColor.xyz += spec * ligColor;

                return finalColor;
            }
            ENDCG

        }

    }

}