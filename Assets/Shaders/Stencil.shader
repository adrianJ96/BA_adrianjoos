Shader "Custom/Stencil"
{
    Properties
    {
        [IntRange] _StencilID("Stencil ID", Range(0, 255)) = 0
    }
    SubShader
    {
        Tags 
        {
             "RenderType"="Opaque" 
             "Queue"="Geometry"
             "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Blend Off
            ZWrite Off

            Stencil
            {
                Ref [_StencilID]
                Comp always
                Pass replace
                Fail keep
            }
        }
    }
}
