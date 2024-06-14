using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HLVR.Interaction
{
    public static class Render 
    {
        /// <summary>
        /// 两点线段渲染
        /// </summary>
        /// <param name="line">lineRender对象</param>
        /// <param name="starPoint">两点线段的起始点</param>
        /// <param name="endPoint">两点线段的终点</param>
        /// <param name="starWidth">两点线段的起始宽度</param>
        /// <param name="endWidth">两点线段的结尾宽度</param>
        /// <param name="material">线段的材质球</param>
        /// <param name="starColor">两点线段的起始颜色</param>
        /// <param name="endColor">两点线段的结尾颜色</param>
        public static void RenderTwoPointSegment(LineRenderer line,Vector3 starPoint,Vector3 endPoint,float starWidth,float endWidth,Material material, Gradient gradientColor)//两点线段渲染
        {
            line.SetPosition(0,starPoint);
            line.SetPosition(1,endPoint);
            line.startWidth = starWidth;
            line.endWidth = endWidth;
            line.material = material;
            line.colorGradient = gradientColor;
        }
        /// <summary>
        /// 多点线段渲染
        /// </summary>
        /// <param name="line">lineRender对象</param>
        /// <param name="points">多点线段的构成点数组</param>
        /// <param name="starWidth">两点线段的起始宽度</param>
        /// <param name="endWidth">两点线段的结尾宽度</param>
        /// <param name="material">线段的材质球</param>
        /// <param name="starColor">两点线段的起始颜色</param>
        /// <param name="endColor">两点线段的结尾颜色</param>
        public static void RenderMultiPointSegment(LineRenderer line,Vector3[] points, float starWidth, float endWidth, Material material,Gradient gradientColor)//多点线段渲染
        {
            line.SetPositions(points);
            line.startWidth = starWidth;
            line.endWidth = endWidth;
            line.material = material;
            line.colorGradient = gradientColor;
           
        }

        public static void RenderSegment(LineRenderer line,  float starWidth, Material material, Gradient gradientColor)//两点线段渲染
        {
            line.startWidth = starWidth;
            line.endWidth = starWidth;
            line.material = material;
            line.colorGradient = gradientColor;
        }


        public static void LineRenderState(LineRenderer line ,bool state)
        {
            line.enabled = state;
        }

        public static Vector3 Bezier(Vector3 star,Vector3 end,Vector3 lerpPoint,float t)
        {
            return Mathf.Pow(1 - t,2) * star+2*t*(1-t)*lerpPoint+Mathf.Pow(t,2)*end;
        }

        public static void BezierLineRender(ref LineRenderer line,Vector3 star, Vector3 end, Vector3 lerpPoint, float t)
        {
            for (int i = 0; i < line.positionCount; i++)
            {    
               line.SetPosition(i, Bezier(star, end, lerpPoint, i / t));
            }
        }

    }
}

