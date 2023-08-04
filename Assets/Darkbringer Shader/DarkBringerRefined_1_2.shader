// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/DarkBringerRefined_1_2"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		//_Lut("Lut Texture", 3D) = "white" {}	
		_Lut2D("Lut 2d Texture", 2D) = "white" {}
		_BayerTex("Bayer 8x8 tex", 2D) = "black" {}
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target	3.0	
			#include "UnityCG.cginc"
			//static int dither[8][8] = {
			//	{ 0, 32, 8, 40, 2, 34, 10, 42 }, /* 8x8 Bayer ordered dithering */
			//	{ 48, 16, 56, 24, 50, 18, 58, 26 }, /* pattern. Each input pixel */
			//	{ 12, 44, 4, 36, 14, 46, 6, 38 }, /* is scaled to the 0..63 range */
			//	{ 60, 28, 52, 20, 62, 30, 54, 22 }, /* before looking in this table */
			//	{ 3, 35, 11, 43, 1, 33, 9, 41 }, /* to determine the action. */
			//	{ 51, 19, 59, 27, 49, 17, 57, 25 },
			//	{ 15, 47, 7, 39, 13, 45, 5, 37 },
			//	{ 63, 31, 55, 23, 61, 29, 53, 21 } };

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}	

			sampler2D _MainTex;
			sampler2D _BayerTex;
			sampler2D _Lut2D;
			//sampler3D _Lut;
			float4 _MainTex_TexelSize, _Lut2D_TexelSize, _Lut2D_ST, _ScreenSize, _Stretching, _BayerTex_ST;
			float _ColorBleed, _ColorShift, _Offset, _Scale, _LinesMult;
			float _ARC, _CSIZE, _DITHER, _PALETTE, _BIT1, _VERTLINES, _HORLINES;
			float Scale = 1.0;

			float _ScaleRG;
			//float _Offset;
			float _Dim;



			float closestPerm( int x, int y)
			{
				

				float resDither = floor( (tex2Dgrad(_BayerTex, float2(x / 8.0, y / 8.0), ddx(_BayerTex_ST.x), ddy(_BayerTex_ST.y)).r)*255.0);

				float limit = 0.0;
				if (x < 8)
				{
					limit = (resDither + 1) / 64.0;
				}
				return limit;
			}

			float closestPermLum(int x, int y, float c0, float lumShift)
			{
				float limit = closestPerm(x, y);

				if (c0 < limit) return 1.0-lumShift;
				return 1.0;
			}

			float find_closest(int x, int y, float c0)
			{

				float limit = closestPerm(x, y);

				if (c0 < limit)
					return 0.0;
				return 1.0;
			}

			float2 Get2DUV(float4 c)
			{
				//c.g = 1 - saturate(c.g);

				float b = floor(c.b * _Dim * _Dim);
				float by = floor(b / _Dim);
				float bx = floor(b - by * _Dim);

				float2 uv = c.rg * _ScaleRG + _Offset;
				uv += float2(bx, by) / _Dim;

				return uv;
			}


			fixed4 frag (v2f i) : SV_Target
			{
				float2 xy = floor(i.uv*_MainTex_TexelSize.zw);
				if (_CSIZE)
				{
					if (_ARC)
					{
						float2 cSC = i.uv*_MainTex_TexelSize.zw;
						float nWidth = (_ScreenSize.x*_Stretching.x / _ScreenSize.y*_Stretching.y)*_MainTex_TexelSize.w;
						float horDistance = (_MainTex_TexelSize.z - nWidth) / 2;
						if (_ARC && (horDistance > 0 &&
							(cSC.x<horDistance || cSC.x>(_MainTex_TexelSize.z - horDistance))))
							return fixed4(0, 0, 0, 0);
					}
					float dnm = (((_MainTex_TexelSize.z) / (_MainTex_TexelSize.w)) / _Stretching.x) / ((_ScreenSize.x / _ScreenSize.y));
					if ((((_MainTex_TexelSize.z) / (_MainTex_TexelSize.w)) / _Stretching.x) != ((_ScreenSize.x / _ScreenSize.y)))
						_ScreenSize.x = floor(_ScreenSize.x * dnm);
					xy = floor(i.uv * _ScreenSize.xy);

					i.uv.x = xy.x / _ScreenSize.x; //Floating point precision error avoidance.
					i.uv.y = xy.y / _ScreenSize.y;

				}
				else
				{
					_ScreenSize.xy = _MainTex_TexelSize.zw;
					_Stretching.xy = 1;
				}

				if (_BIT1)
				{
					float4 lum = float4(0.299, 0.587, 0.114, 0.0);
					float grayscale = dot(tex2D(_MainTex, i.uv), lum);
					int x = floor(fmod(xy.x, 8.0));
					int y = floor(fmod(xy.y, 8.0));
					float bit1 = find_closest(x, y, grayscale);
					return float4(bit1, bit1, bit1, 1);
				}

				float3 rgb = tex2D(_MainTex, i.uv).rgb;
						
				if (_DITHER)
				{
					float4 lum = float4(0.299, 0.587, 0.114, 0.0);
					float grayscale = dot(tex2D(_MainTex, i.uv), lum);

					int x = floor(fmod(xy.x, 8.0));
					int y = floor(fmod(xy.y, 8.0));
					rgb = rgb*(closestPermLum(x, y, grayscale, _ColorBleed) + _ColorShift);
				}
				if (_HORLINES)
				{
					if (fmod(floor(i.uv.y / (1 / (_ScreenSize.y*_Stretching.y))), 2) != 0)
						rgb *= (_LinesMult);
				}
				if (_VERTLINES)
				{
					if (fmod(floor(i.uv.x / (1 / (_ScreenSize.x*_Stretching.x))), 2) != 0)
						rgb *= (_LinesMult);
				}

				if (_PALETTE)
				{
					rgb = clamp(rgb, 0, 1);
					float bScale = (_Lut2D_TexelSize.w - 1.0) / (1.0*_Lut2D_TexelSize.w);
					float bOffset = 1.0 / (2.0 * _Lut2D_TexelSize.w);
					float3 uvRGB = clamp(rgb.rgb * bScale + bOffset, 0, 1);

					rgb = tex2Dgrad(_Lut2D, Get2DUV(float4(rgb, 1)), ddx(_Lut2D_ST.x), ddy(_Lut2D_ST.y)).rgb;  //tex3Dgrad(_Lut, uvRGB, ddx(_Lut_ST.x), ddy(_Lut_ST.y)).rgb;

				}
				
				return  float4(rgb, 1.0);

			}
			ENDCG
		}
	}
}
