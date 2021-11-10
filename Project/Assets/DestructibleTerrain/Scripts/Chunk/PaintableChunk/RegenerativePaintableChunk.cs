using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTerrain
{
    public class RegenerativePaintableChunk : PaintableChunk
    {
        private LinkedList<Tuple<float, Texture2D>> RevertTable = new LinkedList<Tuple<float, Texture2D>>();
        bool reverted = false;

        public override void Init()
        {
            base.Init();

            SaveTextureToRevertTable();
        }

        public override void Update()
        {
            Revert(Terrain.Inst.time);

            if (painted && !reverted)
            {
                DetectChanges();
            }
            reverted = false;
            base.Update();
        }

        public void Revert(float time)
        {
            if (RevertTable.Count == 0)
                return;
            if (time >= RevertTable.First.Value.Item1)
            {
                //Debug.LogWarning("Revert nothing.");
                return;
            }
            if (time < RevertTable.Last.Value.Item1)
            {
                //Debug.LogWarning("Revert to time before known.");
                return;
            }

            while (RevertTable.First.Value.Item1 > time)
            {
                RevertTable.RemoveFirst();
            }

            painted = true;
            Graphics.CopyTexture(RevertTable.First.Value.Item2, TextureSource.Texture);

            RevertTable.RemoveFirst();
            reverted = true;
        }

        private void DetectChanges()
        {
            SaveTextureToRevertTable();

            while (History.Inst.time - Constant.deltaTimeMemory > RevertTable.Last.Value.Item1)
                RevertTable.RemoveLast();
        }

        private void SaveTextureToRevertTable()
        {
            Texture2D src = TextureSource.Texture;
            Texture2D newEntry = new Texture2D(src.width, src.height);
            newEntry.filterMode = src.filterMode;
            Graphics.CopyTexture(src, newEntry);
            RevertTable.AddFirst(Tuple.Create(History.Inst.time, newEntry));
        }
    }
}