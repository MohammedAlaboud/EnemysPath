// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-6554-OUT,emission-9755-OUT;n:type:ShaderForge.SFN_Tex2d,id:3042,x:31270,y:32129,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3042,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8bc18f9b52cb4824b9ecf7cd0bd481eb,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:2238,x:31542,y:33060,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_2238,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:6521,x:31479,y:32552,ptovrint:False,ptlb:Hue,ptin:_Hue,varname:_Brightness_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:9755,x:32352,y:32916,varname:node_9755,prsc:2|A-6554-OUT,B-2238-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:3367,x:31433,y:32377,varname:node_3367,prsc:2|IN-3042-RGB;n:type:ShaderForge.SFN_HsvToRgb,id:6554,x:32044,y:32389,varname:node_6554,prsc:2|H-7953-OUT,S-4240-OUT,V-7619-OUT;n:type:ShaderForge.SFN_Slider,id:7061,x:31479,y:32659,ptovrint:False,ptlb:Saturation,ptin:_Saturation,varname:node_7061,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:552,x:31479,y:32785,ptovrint:False,ptlb:Value ,ptin:_Value,varname:node_552,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:5237,x:31822,y:32267,varname:node_5237,prsc:2|A-3367-HOUT,B-6521-OUT;n:type:ShaderForge.SFN_Multiply,id:4240,x:31822,y:32399,varname:node_4240,prsc:2|A-3367-SOUT,B-7061-OUT;n:type:ShaderForge.SFN_Multiply,id:7619,x:31849,y:32584,varname:node_7619,prsc:2|A-3367-VOUT,B-552-OUT;n:type:ShaderForge.SFN_Add,id:7953,x:31770,y:32138,varname:node_7953,prsc:2|A-3367-HOUT,B-6521-OUT;proporder:3042-2238-6521-7061-552;pass:END;sub:END;*/

Shader "Custom/LowPolySciFi-Hue" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Brightness ("Brightness", Range(0, 1)) = 0
        _Hue ("Hue", Range(-1, 1)) = 1
        _Saturation ("Saturation", Range(0, 1)) = 1
        _Value ("Value ", Range(0, 1)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Brightness;
            uniform float _Hue;
            uniform float _Saturation;
            uniform float _Value;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_3367_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_3367_p = lerp(float4(float4(_MainTex_var.rgb,0.0).zy, node_3367_k.wz), float4(float4(_MainTex_var.rgb,0.0).yz, node_3367_k.xy), step(float4(_MainTex_var.rgb,0.0).z, float4(_MainTex_var.rgb,0.0).y));
                float4 node_3367_q = lerp(float4(node_3367_p.xyw, float4(_MainTex_var.rgb,0.0).x), float4(float4(_MainTex_var.rgb,0.0).x, node_3367_p.yzx), step(node_3367_p.x, float4(_MainTex_var.rgb,0.0).x));
                float node_3367_d = node_3367_q.x - min(node_3367_q.w, node_3367_q.y);
                float node_3367_e = 1.0e-10;
                float3 node_3367 = float3(abs(node_3367_q.z + (node_3367_q.w - node_3367_q.y) / (6.0 * node_3367_d + node_3367_e)), node_3367_d / (node_3367_q.x + node_3367_e), node_3367_q.x);;
                float3 node_6554 = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac((node_3367.r+_Hue)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),(node_3367.g*_Saturation))*(node_3367.b*_Value));
                float3 diffuseColor = node_6554;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (node_6554*_Brightness);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Brightness;
            uniform float _Hue;
            uniform float _Saturation;
            uniform float _Value;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_3367_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_3367_p = lerp(float4(float4(_MainTex_var.rgb,0.0).zy, node_3367_k.wz), float4(float4(_MainTex_var.rgb,0.0).yz, node_3367_k.xy), step(float4(_MainTex_var.rgb,0.0).z, float4(_MainTex_var.rgb,0.0).y));
                float4 node_3367_q = lerp(float4(node_3367_p.xyw, float4(_MainTex_var.rgb,0.0).x), float4(float4(_MainTex_var.rgb,0.0).x, node_3367_p.yzx), step(node_3367_p.x, float4(_MainTex_var.rgb,0.0).x));
                float node_3367_d = node_3367_q.x - min(node_3367_q.w, node_3367_q.y);
                float node_3367_e = 1.0e-10;
                float3 node_3367 = float3(abs(node_3367_q.z + (node_3367_q.w - node_3367_q.y) / (6.0 * node_3367_d + node_3367_e)), node_3367_d / (node_3367_q.x + node_3367_e), node_3367_q.x);;
                float3 node_6554 = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac((node_3367.r+_Hue)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),(node_3367.g*_Saturation))*(node_3367.b*_Value));
                float3 diffuseColor = node_6554;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
