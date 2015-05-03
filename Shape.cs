using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace CSGame{
  class Shape{
    // Modify Line 53 of Game.cs to match the class name above
    Setting game;
    Point3D[] p,r;
    int[,] edges;
    int x, y;
    public int K;
    public bool displayPoints = true;
    public string name;

    public Shape(Setting game, int K, int[,] points, int[,]edges, string name){
      this.game = game;
      this.p = generatePoint3D(points);
      r = new Point3D[p.Length];
      this.edges = edges;
      this.K = K;
      this.name = name;
      for(int index = 0; index < p.Length; index++){
        r[index] = p[index];
      }
    }
    private Point3D[] generatePoint3D(int[,] points){
      int x,y,z;
      Point3D[] p = new Point3D[points.GetLength(0)];
      for(int index = 0; index < points.GetLength(0); index++){
        x = points[index,0]; y = points[index,1]; z = points[index,2];
        p[index] = new Point3D(x,y,z);
      }
      return p;
    }
    public void Draw(){
      Point[] p = new Point[2];
      int posx,posy;
      x = game.mouseX;
      y = game.mouseY;
      for(int index = 0; index < edges.GetLength(0); index++){
        for(int pos = 0; pos < 2; pos++){
          posx = (int)(K * r[edges[index,pos]].X)+x;
          posy = (int)(K * r[edges[index,pos]].Y)+y;
          p[pos] = new Point(posx,posy);
          if(displayPoints) game.canvas.DrawString(""+ (char)(65+edges[index,pos]),game.form.Font,Brushes.Black,p[pos]);
        }
        game.canvas.DrawLine(new Pen(Color.Black),p[0],p[1]);
      }

    }
    public void Rotate(double Az, double Ax, double Ay){
        double tmpx, tmpy, tmpz;

        for (int index = 0; index < r.Length; index++){
            tmpx = Math.Cos(Az) * p[index].X + Math.Sin(Az) * p[index].Y + 0 * p[index].Z;
            tmpy = -Math.Sin(Az) * p[index].X + Math.Cos(Az) * p[index].Y + 0 * p[index].Z;
            tmpz = 0 * p[index].X + 0 * p[index].Y + 1 * p[index].Z;
            r[index].X = tmpx; r[index].Y = tmpy; r[index].Z = tmpz;
        }
        for(int index = 0; index < r.Length; index++){
            tmpx = 1 * r[index].X + 0 * r[index].Y * 0 * r[index].Z;
            tmpy = 0 * r[index].X + Math.Cos(Ax) * r[index].Y + Math.Sin(Ax) * r[index].Z;
            tmpz = 0 * r[index].X - Math.Sin(Ax) * r[index].Y + Math.Cos(Ax) * r[index].Z;
            r[index].X = tmpx; r[index].Y = tmpy; r[index].Z = tmpz;
        }
        for(int index = 0; index < r.Length; index++){
          tmpx = Math.Cos(Ay) * r[index].X + 0 * r[index].Y - Math.Sin(Ay) * r[index].Z;
          tmpy = 0 * r[index].X + 1 * r[index].Y + 0 * r[index].Z;
          tmpz = Math.Sin(Ay) * r[index].X + 0 * r[index].Y + Math.Cos(Ay) * r[index].Z;
          r[index].X = tmpx; r[index].Y = tmpy; r[index].Z = tmpz;
        }
    }

    public void Reset(){
      for(int index = 0; index < p.Length; index++){
        r[index] = p[index];
      }
    }
  }
}
