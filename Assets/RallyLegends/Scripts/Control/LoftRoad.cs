#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Splines;
#endif

using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Interpolators = UnityEngine.Splines.Interpolators;

namespace RallyLegends.Control
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SplineContainer), typeof(MeshRenderer), typeof(MeshFilter))]
    public class LoftRoad : MonoBehaviour
    {
        [SerializeField] private List<SplineData<float>> _widths = new();
        [SerializeField] private Mesh _mesh;

        [SerializeField] private int _segmentsPerMeter = 1;
        [SerializeField] private float _textureScale = 1f;

        private SplineContainer _spline;
        private List<Vector3> _positions = new();
        private List<Vector3> _normals = new();
        private List<Vector2> _textures = new();
        private List<int> _indices = new();

        public IReadOnlyList<Spline> LoftSplines
        {
            get
            {
                if (_spline == null)
                    _spline = GetComponent<SplineContainer>();

                if (_spline == null)
                {
                    Debug.LogError("Cannot loft road mesh because Spline reference is null");
                    return null;
                }

                return _spline.Splines;
            }
        }
        public Mesh LoftMesh
        {
            get
            {
                if (_mesh != null)
                    return _mesh;

                _mesh = new Mesh();
                GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<Material>("Road");
                return _mesh;
            }
        }
        public SplineContainer Container
        {
            get
            {
                if (_spline == null)
                    _spline = GetComponent<SplineContainer>();

                return _spline;
            }
            set => _spline = value;
        }
        public List<SplineData<float>> Widths
        {
            get
            {
                foreach (var width in _widths)
                {
                    if (width.DefaultValue == 0)
                        width.DefaultValue = 1f;
                }

                return _widths;
            }
        }
        public int SegmentsPerMeter => Mathf.Min(10, Mathf.Max(1, _segmentsPerMeter));

        private void Awake()
        {
            _spline = GetComponent<SplineContainer>();
        }

        private void OnEnable()
        {
            if (_mesh != null)
                _mesh = null;

            LoftAllRoads();

#if UNITY_EDITOR
            EditorSplineUtility.AfterSplineWasModified += OnAfterSplineWasModified;
            EditorSplineUtility.RegisterSplineDataChanged<float>(OnAfterSplineDataWasModified);
            Undo.undoRedoPerformed += LoftAllRoads;
#endif

            SplineContainer.SplineAdded += OnSplineContainerAdded;
            SplineContainer.SplineRemoved += OnSplineContainerRemoved;
            SplineContainer.SplineReordered += OnSplineContainerReordered;
            Spline.Changed += OnSplineChanged;
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            EditorSplineUtility.AfterSplineWasModified -= OnAfterSplineWasModified;
            EditorSplineUtility.UnregisterSplineDataChanged<float>(OnAfterSplineDataWasModified);
            Undo.undoRedoPerformed -= LoftAllRoads;
#endif

            if (_mesh != null)
#if UNITY_EDITOR
                DestroyImmediate(_mesh);
#else
                Destroy(_mesh);          
#endif

            SplineContainer.SplineAdded -= OnSplineContainerAdded;
            SplineContainer.SplineRemoved -= OnSplineContainerRemoved;
            SplineContainer.SplineReordered -= OnSplineContainerReordered;
            Spline.Changed -= OnSplineChanged;
        }

        public void LoftAllRoads()
        {
            LoftMesh.Clear();
            _positions.Clear();
            _normals.Clear();
            _textures.Clear();
            _indices.Clear();

            for (var i = 0; i < LoftSplines.Count; i++)
                Loft(LoftSplines[i], i);

            LoftMesh.SetVertices(_positions);
            LoftMesh.SetNormals(_normals);
            LoftMesh.SetUVs(0, _textures);
            LoftMesh.subMeshCount = 1;
            LoftMesh.SetIndices(_indices, MeshTopology.Triangles, 0);
            LoftMesh.UploadMeshData(false);

            GetComponent<MeshFilter>().sharedMesh = _mesh;
        }

        public void Loft(Spline spline, int widthDataIndex)
        {
            if (spline == null || spline.Count < 2)
                return;

            LoftMesh.Clear();

            float length = spline.GetLength();

            if (length <= 0.001f)
                return;

            var segmentsPerLength = SegmentsPerMeter * length;
            var segments = Mathf.CeilToInt(segmentsPerLength);
            var segmentStepT = (1f / SegmentsPerMeter) / length;
            var steps = segments + 1;
            var vertexCount = steps * 2;
            var triangleCount = segments * 6;
            var prevVertexCount = _positions.Count;

            _positions.Capacity += vertexCount;
            _normals.Capacity += vertexCount;
            _textures.Capacity += vertexCount;
            _indices.Capacity += triangleCount;

            var t = 0f;

            for (int i = 0; i < steps; i++)
            {
                SplineUtility.Evaluate(spline, t, out var pos, out var dir, out var up);

                if (math.length(dir) == 0)
                {
                    var nextPos = spline.GetPointAtLinearDistance(t, 0.01f, out _);
                    dir = math.normalizesafe(nextPos - pos);

                    if (math.length(dir) == 0)
                    {
                        nextPos = spline.GetPointAtLinearDistance(t, -0.01f, out _);
                        dir = -math.normalizesafe(nextPos - pos);
                    }

                    if (math.length(dir) == 0)
                        dir = new float3(0, 0, 1);
                }

                var scale = transform.lossyScale;
                var tangent = math.normalizesafe(math.cross(up, dir)) * new float3(1f / scale.x, 1f / scale.y, 1f / scale.z);
                var w = 1f;

                if (widthDataIndex < _widths.Count)
                {
                    w = _widths[widthDataIndex].DefaultValue;

                    if (_widths[widthDataIndex] != null && _widths[widthDataIndex].Count > 0)
                    {
                        w = _widths[widthDataIndex].Evaluate(spline, t, PathIndexUnit.Normalized, new Interpolators.LerpFloat());
                        w = math.clamp(w, .001f, 10000f);
                    }
                }

                _positions.Add(pos - (tangent * w));
                _positions.Add(pos + (tangent * w));
                _normals.Add(up);
                _normals.Add(up);
                _textures.Add(new Vector2(0f, t * _textureScale));
                _textures.Add(new Vector2(1f, t * _textureScale));

                t = math.min(1f, t + segmentStepT);
            }

            for (int i = 0, n = prevVertexCount; i < triangleCount; i += 6, n += 2)
            {
                _indices.Add((n + 2) % (prevVertexCount + vertexCount));
                _indices.Add((n + 1) % (prevVertexCount + vertexCount));
                _indices.Add((n + 0) % (prevVertexCount + vertexCount));
                _indices.Add((n + 2) % (prevVertexCount + vertexCount));
                _indices.Add((n + 3) % (prevVertexCount + vertexCount));
                _indices.Add((n + 1) % (prevVertexCount + vertexCount));
            }
        }

        private void OnSplineContainerAdded(SplineContainer container, int index)
        {
            if (container != _spline)
                return;

            if (_widths.Count < LoftSplines.Count)
            {
                var delta = LoftSplines.Count - _widths.Count;

                for (var i = 0; i < delta; i++)
                {
#if UNITY_EDITOR
                    Undo.RecordObject(this, "Modifying Widths SplineData");
#endif
                    _widths.Add(new SplineData<float>() { DefaultValue = 1f });
                }
            }

            LoftAllRoads();
        }

        private void OnSplineContainerRemoved(SplineContainer container, int index)
        {
            if (container != _spline)
                return;

            if (index < _widths.Count)
            {
#if UNITY_EDITOR
                Undo.RecordObject(this, "Modifying Widths SplineData");
#endif
                _widths.RemoveAt(index);
            }

            LoftAllRoads();
        }

        private void OnSplineContainerReordered(SplineContainer container, int previousIndex, int newIndex)
        {
            if (container != _spline)
                return;

            LoftAllRoads();
        }

        private void OnAfterSplineWasModified(Spline spline)
        {
            if (LoftSplines == null)
                return;

            foreach (var loftSpline in LoftSplines)
            {
                if (spline == loftSpline)
                {
                    LoftAllRoads();
                    break;
                }
            }
        }

        private void OnSplineChanged(Spline spline, int knotIndex, SplineModification modification)
        {
            OnAfterSplineWasModified(spline);
        }

        private void OnAfterSplineDataWasModified(SplineData<float> splineData)
        {
            foreach (var width in _widths)
            {
                if (splineData == width)
                {
                    LoftAllRoads();
                    break;
                }
            }
        }
    }
}