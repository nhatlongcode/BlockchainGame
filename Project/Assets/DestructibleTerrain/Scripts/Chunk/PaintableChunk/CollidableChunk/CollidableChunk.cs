using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DTerrain
{
    /// <summary>
    /// CollidableChunk on each Paint execution deletes data from a list of columns, that is used to create BoxColliders2D.
    /// </summary>
    public class CollidableChunk : PaintableChunk
    {
        public float AlphaTreshold { get; set; } = 0.01f;
        public float DeltaTimeRemember = 10.0f;
        protected List<Column> columns;
        protected IChunkCollider chunkCollider;
        protected bool colliderChanged = true;

        public override void Init()
        {
            base.Init();
            chunkCollider = GetComponent<IChunkCollider>();
            PrepareColumns();
        }

        public override bool Paint(RectInt r, PaintingParameters pp)
        {
            bool b = base.Paint(r, pp);

            if(pp.DestructionMode==DestructionMode.DESTROY)
                DeleteFromColumns(r);
            if (pp.DestructionMode == DestructionMode.BUILD)
                AddToColumns(r);

            return b;
        }

        public override void Update()
        {
            base.Update();
            if(colliderChanged)
            {
                chunkCollider?.UpdateColliders(columns, TextureSource);

                DetectChanges();

                colliderChanged = false;
            }
        }

        public virtual bool IsOccupiedAt(Vector2Int at)
        {
            if (at.x >= 0 && at.x < columns.Count)
            {
                return columns[at.x].isWithin(at.y);
            }
            return false;
        }

        private void DeleteFromColumns(RectInt rect)
        {
            RectInt common;
            rect.Intersects(new RectInt(0, 0, TextureSource.Texture.width, TextureSource.Texture.height), out common);

            for(int i = 0; i<common.width;i++)
            {
                bool changed = columns[common.x + i].DelRange(new Range(common.y-1, common.y+common.height));

                if (changed)
                {
                    int column = common.x + i;
                    if (newEntry.ContainsKey(column))
                        newEntry[column] = new Column(columns[column]);

                    colliderChanged = true;
                }
            }
        }

        private void AddToColumns(RectInt rect)
        {
            RectInt common;
            rect.Intersects(new RectInt(0, 0, TextureSource.Texture.width, TextureSource.Texture.height), out common);

            for (int i = 0; i < common.width; i++)
            {
                bool changed = columns[common.x + i].SumRange(new Range(common.y, common.y + common.height - 1));

                if (changed)
                {
                    int column = common.x + i;
                    if (newEntry.ContainsKey(column))
                        newEntry[column] = new Column(columns[column]);

                    colliderChanged = true;
                }
            }
        }

        Dictionary<int, Column> newEntry = new Dictionary<int, Column>();
        private LinkedList<Tuple<float, Dictionary<int, Column>>> RevertTable = new LinkedList<Tuple<float, Dictionary<int, Column>>>();
        public void Revert(float time)
        {
            if (time < RevertTable.Last.Value.Item1)
            {
                //Debug.LogError("Revert to time before known");
                return;
            }    
            List<Column> revert = new List<Column>();
            foreach (var _ in columns)
                revert.Add(null);
            while (RevertTable.First.Value.Item1 > time)
            {
                var revertTableRow = RevertTable.First.Value;

                foreach (var line in revertTableRow.Item2)
                {
                    revert[line.Key] = line.Value;
                }

                RevertTable.RemoveFirst();
            }

            var lastRevertTableRow = RevertTable.First.Value;

            foreach (var line in lastRevertTableRow.Item2)
            {
                revert[line.Key] = line.Value;
            }

            RevertTable.RemoveFirst();

            for (int i = 0; i < revert.Count; i++)
                if (revert[i] != null)
                {
                    colliderChanged = true;
                    columns[i] = revert[i];
                }
        }

        private void DetectChanges()
        {
            RevertTable.AddFirst(Tuple.Create(History.Inst.time, newEntry));
            newEntry = new Dictionary<int, Column>();

            while (History.Inst.time - DeltaTimeRemember > RevertTable.Last.Value.Item1)
                RevertTable.RemoveLast();
        }

        /// <summary>
        /// Using terrainTexture creates a list of ranges (tiles that are egible for a collider).
        /// </summary>
        protected void PrepareColumns()
        {
            columns?.Clear();
            columns = new List<Column>();

            //Iterate texture
            for (int x = 0; x < TextureSource.Texture.width; x++)
            {
                Column c = new Column(x);
                for (int y = 0; y < TextureSource.Texture.height; y++)
                {
                    int potentialMin = y;
                    int potentialMax = y - 1;
                    while (y < TextureSource.Texture.height && TextureSource.Texture.GetPixel(x, y).a > AlphaTreshold)
                    {
                        y++;
                        potentialMax++;
                    }
                    if (potentialMin <= potentialMax)
                    {
                        c.AddRange(potentialMin, potentialMax); //Add range to a column...
                    }
                }
                columns.Add(c); //And add the column!
            }
        }
    }
}
