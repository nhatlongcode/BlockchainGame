using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTerrain;

public class Terrain : MonoBehaviour
{
    private static Terrain inst;

    public BasicPaintableLayer logicLayer;
    public BasicPaintableLayer spriteLayer;
    public float time { get => History.Inst.time; }

    public static Terrain Inst { get => inst; }

    Terrain()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (logicLayer == null || spriteLayer == null)
        {
            Debug.LogError("Terrain layers not binded properly.");
        }
    }

    // TODO: Sync this function call
    public void DestroyTerrain(Vector2 position, Shape destroyShape)
    {
        /*
        Vector2 localScale = logicLayer.transform.localScale;
        Vector2 scale = new Vector2(1 / localScale.x, 1 / localScale.y);
        p.Scale(scale);
        //*/

        Vector2 targetPosLogic = logicLayer.transform.InverseTransformPoint(position);
        logicLayer.Paint(new PaintingParameters()
        {
            Color = Color.clear,
            Position = new Vector2Int((int)(targetPosLogic.x * logicLayer.PPU) + destroyShape.OffsetX, (int)(targetPosLogic.y * logicLayer.PPU) + destroyShape.OffsetY),
            Shape = destroyShape,
            PaintingMode = PaintingMode.NONE,
            DestructionMode = DestructionMode.DESTROY
        });

        Vector2 targetPosPaint = spriteLayer.transform.InverseTransformPoint(position);
        spriteLayer.Paint(new PaintingParameters()
        {
            Color = Color.clear,
            Position = new Vector2Int((int)(targetPosPaint.x * spriteLayer.PPU) + destroyShape.OffsetX, (int)(targetPosPaint.y * spriteLayer.PPU) + destroyShape.OffsetY),
            Shape = destroyShape,
            PaintingMode = PaintingMode.REPLACE_COLOR,
            DestructionMode = DestructionMode.NONE
        });
    }
}
