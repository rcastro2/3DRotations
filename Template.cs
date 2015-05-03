using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CSGame{
  class Template{
    // Modify Line 53 of Game.cs to match the class name above
    Setting game;
    Diamond d;
    Cube c;
    Spikes s;
    Shape h;
    double Da = Math.PI/90,Az,Ax,Ay;
    double Dz,Dx,Dy;
    string shape = "Shape";
    int A=0,B=1,C=2,D=3,E=4,F=5,G=6,H=7,I=8,J=9;
    public Template(Setting game){
      this.game = game;
      d = new Diamond(game,100);
      c = new Cube(game,100);
      s = new Spikes(game,100);
      int[,] points = new int[,]{
        {0,0,0},
        {1,1,1},
        {1,1,-1},
        {-1,1,-1},
        {-1,1,1},
        {1,-1,1},
        {1,-1,-1},
        {-1,-1,-1},
        {-1,-1,1}
      };
      int[,] edges = new int[,] {{B,C},{C,D},{D,E},{E,B},{A,B},{A,C},{A,D},{A,E},{F,G},{G,H},{H,I},{I,F},{A,F},{A,G},{A,H},{A,I}};
      h = new Shape(game,50,points,edges);
    }

    public void update(){
      game.canvas.Clear(Color.White);
      game.canvas.DrawString(game.keyDown,game.form.Font,Brushes.Black,5,20);
      game.canvas.DrawString("Current Shape: " + shape,game.form.Font,Brushes.Black,5,5);
      switch(shape){
        case "Cube":
          c.Draw();
          c.Rotate(Az,Ax,Ay);
          break;
        case "Diamond":
          d.Draw();
          d.Rotate(Az,Ax,Ay);
          break;
        case "Spikes":
          s.Draw();
          s.Rotate(Az,Ax,Ay);
          break;
        case "Shape":
          h.Draw();
          h.Rotate(Az,Ax,Ay);
          break;
      }
      Az += Dz; Ax += Dx; Ay += Dy;
      switch(game.keyDown){
        case "Left":
          Dz = Da;
          break;
        case "Right":
          Dz = -Da;
          break;
        case "Up":
          Dx = Da;
          break;
        case "Down":
          Dx = -Da;
          break;
        case "A":
          Dy = Da;
          break;
        case "D":
          Dy = -Da;
          break;
        case "P":
        case "Oemplus":
          c.K += 1;
          break;
        case "M":
        case "OemMinus":
            c.K -= 1;
            break;
        case "D1":
            shape = "Cube";
            break;
        case "D2":
            shape = "Diamond";
            break;
        case "D3":
            shape = "Spikes";
            break;
        case "D4":
            shape = "Shape";
            break;
        case "Space":
          Az = Ay = Ax = 0;
          c.Reset();
          break;
        }
        if(game.keyUp == "Left" || game.keyUp == "Right") Dz = 0;
        if(game.keyUp == "Up" || game.keyUp == "Down") Dx = 0;
        if(game.keyUp == "A" || game.keyUp == "D") Dy = 0;

    }
  }
}
