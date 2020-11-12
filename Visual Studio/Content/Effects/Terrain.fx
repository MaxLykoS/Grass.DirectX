Texture2D Texture;
sampler TextureSampler = sampler_state
{
	AddressU = WRAP;
	AddressV = WRAP;
	Filter = D3D11_FILTER_MAXIMUM_ANISOTROPIC;
	MaxAnisotropy = 16;
};

Texture2D TextureAlpha;

float4x4 World;
float4x4 View;
float4x4 Projection;

float3 AmbientColor;
float AmbientIntensity;

float3 DiffuseColor;
float DiffuseIntensity;

float3 LightDirection;
float3 CameraWorldPos;

float TileFactor = 40.0f;

struct VSINPUT
{
	float4 Position : SV_POSITION;
	float3 Normal	: NORMAL0;
	float2 TexCoord	: TEXCOORD0;
};

struct PSINPUT {
    float3 WorldPos : Position;
	float3 WorldNormal	: NORMAL0;
	float2 TexCoord	: TEXCOORD0;
    float4 Position : SV_POSITION;
};

void VS_Shader(in VSINPUT input, out PSINPUT output)
{
    output.WorldPos = mul(input.Position, World);
    output.WorldNormal = mul(input.Normal, (float3x3)World);
	output.TexCoord = input.TexCoord;
	
    output.Position = (float4) mul(mul(float4(output.WorldPos,1.0f), View), Projection);
}

float4 PS_Shader(in PSINPUT input) : SV_TARGET
{
    input.WorldNormal = normalize(input.WorldNormal);
    
    float3 toEyeW = normalize(CameraWorldPos - input.WorldPos);
    float3 ambient, diffuse, spec;
    float3 texColor = Texture.Sample(TextureSampler, input.TexCoord).rgb;
	
    ambient = 0.5f;
    diffuse = saturate(dot(-LightDirection, input.WorldNormal)) * 0.1f;
    spec = 0.1f * pow(saturate(dot(reflect(LightDirection, input.WorldNormal), toEyeW)),100);
	
    float3 litColor = saturate(ambient * texColor + (spec + diffuse));
    return float4(litColor,1.0f);
}

technique Technique1
{
	pass Pass1
	{
		VertexShader = compile vs_4_0 VS_Shader();
		PixelShader = compile ps_4_0 PS_Shader();
		GeometryShader = 0;
	}
}