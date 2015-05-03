using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace CSGame{
  class Spikes{
    // Modify Line 53 of Game.cs to match the class name above
    Setting game;
    Point3D[] p = new Point3D[7];
    Point3D[] r = new Point3D[7];
    int x, y;
    public int K;
    public bool displayPoints = true;
    int A=0,B=1,C=2,D=3,E=4,F=5,G=6;//,H=7;

    public Spikes(Setting game, int K){
      this.game = game;
      p[A] = new Point3D(0,0,0);
      p[B] = new Point3D(-1,0,0);
      p[C] = new Point3D(1,0,0);
      p[D] = new Point3D(0,-1,0);
      p[E] = new Point3D(0,1,0);
      p[F] = new Point3D(0,0,-1);
      p[G] = new Point3D(0,0,1);

      this.K = K;
      for(int index = 0; index < p.Length; index++){
        r[index] = p[index];
      }
    }

    public void Draw(){
      Point[] p = new Point[2];
      int posx,posy;
      x = game.mouseX;
      y = game.mouseY;
      int[,] edges = new int[,] {{A,B},{A,C},{A,D},{A,E},{A,F},{A,G}};
      for(int index = 0; index < edges.GetLength(0); index++){
        for(int pos = 0; pos < 2; pos++){
          posx = (int)(K * r[edges[index,pos]].X)+x;
          posy = (int)(K * r[edges[index,pos]].Y)+y;
          p[pos] = new Point(posx,posy);
          if(displayPoints) game.canvas.DrawString(""+ edges[index,pos],game.form.Font,Brushes.Black,p[pos]);
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
