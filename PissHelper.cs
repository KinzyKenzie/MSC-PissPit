using UnityEngine;

namespace PissPitMod
{
    static class PissHelper
    {
        // <Summary>
        //     Creates a flat plane GameObject - The entire point of the mod
        // </Summary>
        public static GameObject CreatePlane( string name, Vector3 size, Material mat ) {

            GameObject go = new GameObject( name );

            MeshFilter mf = go.AddComponent( typeof( MeshFilter ) ) as MeshFilter;
            MeshRenderer mr = go.AddComponent( typeof( MeshRenderer ) ) as MeshRenderer;

            Mesh m = new Mesh() {
                vertices = new Vector3[] {
                    new Vector3(
                        -0.5f * size.x,
                        -0.5f * size.y,
                        0.5f * size.z
                    ),
                    new Vector3(
                        0.5f * size.x,
                        -0.5f * size.y,
                        0.5f * size.z
                    ),
                    new Vector3(
                        0.5f * size.x,
                        0.5f * size.y,
                        -0.5f * size.z
                    ),
                    new Vector3(
                        -0.5f * size.x,
                        0.5f * size.y,
                        -0.5f * size.z
                    )
                },
                uv = new Vector2[] {
                    new Vector2( 0,                     0 ),
                    new Vector2( 0,                     Mathf.Round( size.x ) ),
                    new Vector2( Mathf.Round( size.z ), Mathf.Round( size.x ) ),
                    new Vector2( Mathf.Round( size.z ), 0 )
                }
            };

            m.triangles = new int[] {
                0,1,2,0,2,3
            };

            mf.mesh = m;
            mr.material = mat;

            m.RecalculateBounds();
            m.RecalculateNormals();

            return go;
        }
    }
}
