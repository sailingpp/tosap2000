using System;
using System.Collections.Generic;
using System.Text;
using SAP2000v15;
using Teigha.DatabaseServices;
using Teigha.Geometry;
using Teigha.Runtime;

namespace Tube
{
    public class DrawTube
    {
        public string FilePath;

        public void test(double LeftWall,double MiddWall, double RightWall,double TopPlate, double BotPlate , double leftLength, double rightLength ,double Height)
        {
            using (Services svcs = new Services())
            {
    
                Database db = new Database();
                Polyline pl1 = new Polyline();
                pl1.AddVertexAt(0, Point2d.Origin, 0, 0, 0);
                pl1.AddVertexAt(1, Point2d.Origin + new Vector2d(leftLength, 0), 0, 0, 0);
                pl1.AddVertexAt(2, Point2d.Origin + new Vector2d(leftLength, Height), 0, 0, 0);
                pl1.AddVertexAt(3, Point2d.Origin + new Vector2d(0, Height), 0, 0, 0);
                pl1.Closed = true;

                Point2d base2=Point2d.Origin+new Vector2d(leftLength+MiddWall,0);
                Polyline pl2= new Polyline();
                pl2.AddVertexAt(0, base2, 0, 0, 0);
                pl2.AddVertexAt(1, base2 + new Vector2d(rightLength, 0), 0, 0, 0);
                pl2.AddVertexAt(2, base2 + new Vector2d(rightLength, Height), 0, 0, 0);
                pl2.AddVertexAt(3, base2 + new Vector2d(0, Height), 0, 0, 0);
                pl2.Closed = true;

                Point2d base3 = Point2d.Origin + new Vector2d(-LeftWall, -BotPlate);
                Polyline pl3 = new Polyline();
                pl3.AddVertexAt(0, base3, 0, 0, 0);
                pl3.AddVertexAt(1, base3 + new Vector2d(LeftWall+leftLength+MiddWall+rightLength+RightWall, 0), 0, 0, 0);
                pl3.AddVertexAt(2, base3 + new Vector2d(LeftWall + leftLength + MiddWall + rightLength + RightWall, BotPlate+Height+TopPlate), 0, 0, 0);
                pl3.AddVertexAt(3, base3 + new Vector2d(0, BotPlate + Height + TopPlate), 0, 0, 0);
                pl3.Closed = true;

                using (BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable)
                {
                    BlockTableRecord btr = bt[BlockTableRecord.ModelSpace].GetObject(OpenMode.ForWrite) as BlockTableRecord;
                    btr.AppendEntity(pl1);
                    btr.AppendEntity(pl2);
                    btr.AppendEntity(pl3);
                }
                db.SaveAs(@FilePath, DwgVersion.Current);
            }
        }
    }
}
