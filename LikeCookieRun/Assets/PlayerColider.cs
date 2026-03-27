using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

struct PlayerColider {
    public Vector2 Offset;
    public Vector2 Size;

    public PlayerColider(Vector2 offset, Vector2 size) { Offset = offset; Size = size; }
}