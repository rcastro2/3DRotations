using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace CSGame{
  class Cube{
    // Modify Line 53 of Game.cs to match the class name above
    Setting game;
    Point3D[] p = new Point3D[8];
    Point3D[] r = new Point3D[8];
    int x, y;
    public int K;
    int A=0,B=1,C=2,D=3,E=4,F=5,G=6,H=7;
    //double Ax,Ay,Az;

    public Cube(Setting game, int K){
      this.game = game;
      p[A] = new Point3D(-1,-1,-1);
      p[B] = new Point3D(-1,-1,1);
      p[C] = new Point3D(1,-1,1);
      p[D] = new Point3D(1,-1,-1);
      p[E] = new Point3D(-1,1,-1);
      p[F] = new Point3D(-1,1,1);
      p[G] = new Point3D(1,1,1);
      p[H] = new Point3D(1,1,-1);
      this.K = K;
      for(int index = 0; index < p.Length; index++){
        r[index] = p[index];
      }
    }

    public void Draw(){
      x = game.mouseX;
      y = game.mouseY;
      double tmp;
      double [] zIndex = new double[3];
      double [] order = {3,2,1};
      zIndex[0] =  min(r[G].Z, r[F].Z, r[B].Z, r[C].Z); //Plane3
      zIndex[1] =  min(r[G].Z, r[H].Z, r[D].Z, r[C].Z); //Plane2
      zIndex[2] =  min(r[E].Z, r[H].Z, r[D].Z, r[A].Z); //Plane1

      for(int pos = 0; pos < order.Length; pos++)
        for(int pos2 = 0; pos2 < order.Length; pos2++)
          if(zIndex[pos] < zIndex[pos2]){
            tmp = order[pos];
            order[pos] = order[pos2];
            order[pos2] = tmp;
            tmp = zIndex[pos];
            zIndex[pos] = zIndex[pos2];
            zIndex[pos2] = tmp;
          }

      for(int index = 0; index < order.Length; index++){
        if(order[index]==3) Plane3();
        if(order[index]==2) Plane2();
        if(order[index]==1) Plane1();
      }
    }

    public double min(double n1, double n2, double n3, double n4){
      double lowest = n1;
      if(n2<lowest) lowest = n2;
      if(n3<lowest) lowest = n3;
      if(n4<lowest) lowest = n4;
      return lowest;
    }
    public void Plane3(){
      //GFBC
      Point[] points = new Point[4];
      points[0] = new Point((int)(K * r[G].X)+x,(int)(K * r[G].Y)+y);
      points[1] = new Point((int)(K * r[F].X)+x,(int)(K * r[F].Y)+y);
      points[2] = new Point((int)(K * r[B].X)+x,(int)(K * r[B].Y)+y);
      points[3] = new Point((int)(K * r[C].X)+x,(int)(K * r[C].Y)+y);
      game.canvas.DrawPolygon(new Pen(Color.Black), points);
      //game.canvas.FillPolygon(new SolidBrush(Color.Blue),points);

    }
    public void Plane2(){
      //GHDC
      Point[] points = new Point[4];
      points[0] = new Point((int)(K * r[G].X)+x,(int)(K * r[G].Y)+y);
      points[1] = new Point((int)(K * r[H].X)+x,(int)(K * r[H].Y)+y);
      points[2] = new Point((int)(K * r[D].X)+x,(int)(K * r[D].Y)+y);
      points[3] = new Point((int)(K * r[C].X)+x,(int)(K * r[C].Y)+y);
      game.canvas.DrawPolygon(new Pen(Color.Black),points);
      //game.canvas.FillPolygon(new SolidBrush(Color.Red),points);
    }
    public void Plane1(){
      //EHDA
      Point[] points = new Point[4];
      points[0] = new Point((int)(K * r[E].X)+x,(int)(K * r[E].Y)+y);
      points[1] = new Point((int)(K * r[H].X)+x,(int)(K * r[H].Y)+y);
      points[2] = new Point((int)(K * r[D].X)+x,(int)(K * r[D].Y)+y);
      points[3] = new Point((int)(K * r[A].X)+x,(int)(K * r[A].Y)+y);
      game.canvas.DrawPolygon(new Pen(Color.Black),points);
      //game.canvas.FillPolygon(new SolidBrush(Color.Gray),points);
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
  struct Point3D{
    public double X;
    public double Y;
    public double Z;
    public Point3D(int X, int Y, int Z){
      this.X = X; this.Y = Y; this.Z = Z;
    }
  }
}
