using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterMesh : MonoBehaviour
{
    [SerializeField]
    GameObject _playerObj = null;

    [SerializeField]
    GameObject _watersObjManager = null;

    [SerializeField]
    Material _waterMaterial = null;

    [SerializeField]
    Material _iceMaterial = null;

    [SerializeField]
    Material _airMaterial = null;

    Mesh _mesh;

    private int _count = 0;

    // Use this for initialization
    void Start()
    {
        _mesh = new Mesh();

        Is2DRenderMeshUpdate();
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        GetComponent<MeshFilter>().sharedMesh = _mesh;
        GetComponent<MeshFilter>().sharedMesh.name = "myMesh";

        if (_playerObj == null)
        {
            _playerObj = GameObject.Find("Player");
        }
        if(_watersObjManager == null)
        {
            _watersObjManager = GameObject.Find("PlayerWaters");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var playerMode = _playerObj.GetComponent<Player>();
        if (playerMode._playerMode != playerMode._PLAYER_MODE_ICE)
        {
            Is2DRenderMeshUpdate();
        }
        ChangeMaterial();
    }

    void Is2DRenderMeshUpdate()
    {
        // プレイヤーの水を配列で確保
        var waters = _watersObjManager.GetComponentsInChildren<PlayerWaterMover>();

        // 配列数が２以下だと△ポリゴンが作れないので抜ける(３つ頂点が必要)
        int triangleNum = (waters.Length - 2) * 3;
        if (triangleNum <= 2)
        {
            // 前に描画した水が残るので _mesh を空にする
            Mesh resetMesh = new Mesh();
            _mesh = resetMesh;

            _mesh.RecalculateNormals();
            _mesh.RecalculateBounds();

            GetComponent<MeshFilter>().sharedMesh = _mesh;
            GetComponent<MeshFilter>().sharedMesh.name = "myMesh";
            return;
        }

        //Vector3 AveragePosition = GameObject.Find("Player").transform.position;

        // + 1 は AveragePosition の分
        Vector3[] vertices = new Vector3[waters.Length + 1];
        Vector2[] uv = new Vector2[waters.Length + 1];
        // 0,1,2 → 0,2,3 → 0,3,4 と要素数３つずつでつないでいるため * 3 している
        int[] triangles = new int[waters.Length * 3];

        //Debug.Log(triangles.Length / 3);

        // 頂点座標指定
        // - 1 は要素数外にでないように
        for (int i = 0; i < waters.Length; ++i)
        {
            var vertex = waters[i].transform.position - _playerObj.transform.position;
            //Debug.Log(offset);
            vertices[i + 1] = vertex;
        }

        // 水の中心(平均値)を求める
        Vector3 SumPosition = Vector3.zero;
        for (int i = 0; i < waters.Length; ++i)
        {
            SumPosition += vertices[i + 1];
        }
        Vector3 AveragePosition = SumPosition / waters.Length;
        vertices[0] = AveragePosition;

        // UVの指定
        Vector2[] uvs =
        {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
        };
        for (int i = 0; i < uv.Length; ++i)
        {
            uv[i] = uvs[i % 4];
        }

        // 三角形の頂点インデックスの指定
        // count は 0,1,2 → 0,2,3 → 0,3,4　とつなげるためのカウント
        int index_count = 1;
        for (int i = 0; i < triangles.Length - 3; i += 3)
        {
            triangles[i] = 0;
            triangles[i + 1] = index_count;
            triangles[i + 2] = ++index_count;
        }
        triangles[triangles.Length - 3] = 0;
        triangles[triangles.Length - 2] = index_count;
        triangles[triangles.Length - 1] = 1;

        //Debug.Log(count);
        // いきなり _mesh にデータを入れるとエラーがでる
        // _mesh.vertices = vertices;　をすると
        // uv と vertices の要素数が一致しないといわれるため
        // 新しい Mesh を作り _mesh に入れる

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        _mesh = mesh;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        GetComponent<MeshFilter>().sharedMesh = _mesh;
        GetComponent<MeshFilter>().sharedMesh.name = "myMesh";

    }

    void ChangeMaterial()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null) { return; }
        var playerMode = player.GetComponent<Player>();
        if (playerMode._playerMode == playerMode._PLAYER_MODE_WATER)
        {
            GetComponent<MeshRenderer>().material = _waterMaterial;
        }
        else if (playerMode._playerMode == playerMode._PLAYER_MODE_ICE)
        {
            GetComponent<MeshRenderer>().material = _iceMaterial;
        }
        else if (playerMode._playerMode == playerMode._PLAYER_MODE_AIR)
        {
            GetComponent<MeshRenderer>().material = _airMaterial;

            if (playerMode.ChangeAir)
            {
                _count++;
                //Debug.Log(_count / 2 % 2);
                if (_count / 6 % 2 <= 0)
                {
                    GetComponent<MeshRenderer>().material = _waterMaterial;
                }
                else
                {
                    GetComponent<MeshRenderer>().material = _airMaterial;
                }
            }
        }
    }
}
