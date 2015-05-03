using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CSGame{
  class Template{
    // Modify Line 53 of Game.cs to match the class name above
    Setting game;
    Cube c;
    double A = Math.PI/90,Az,Ax,Ay;
    double Dz = Math.PI/90,Dx = Math.PI/90,Dy=Math.PI/90;
    public Template(Setting game){
      this.game = game;
      c = new Cube(game,100);
    }

    public void update(){
      game.canvas.Clear(Color.White);
      c.Draw();
      c.Rotate(Az,Ax,Ay);
      Az += Dz;
      Ax += Dx;
      Ay += Dy;
      Console.WriteLine(game.keyDown);
      switch(game.keyDown){
        case "Left":
          //Az += A;
          Dz = A;
          break;
        case "Right":
          //Az -= A
          Dz = -A;
          break;
        case "Up":
          Dx = A;
          //Ax += A;
          break;
        case "Down":
          Dx = -A;
          //Ax -= A;
          break;
        case "A":
          Dy = A;
          //Ay += A;
          break;
        case "D":
          Dy = -A;
          //Ay -= A;
          break;
        case "P":
        case "Oemplus":
          c.K += 1;
          break;
        case "M":
        case "OemMinus":
            c.K -= 1;
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
