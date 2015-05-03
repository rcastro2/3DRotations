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
    double A = Math.PI/90,Az,Ax,Ay;
    double Dz,Dx,Dy;
    string shape = "Cube";
    public Template(Setting game){
      this.game = game;
      d = new Diamond(game,100);
      c = new Cube(game,100);
      s = new Spikes(game,100);
    }

    public void update(){
      game.canvas.Clear(Color.White);
      game.canvas.DrawString(game.keyDown,game.form.Font,Brushes.Black,5,5);
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
      }
      Az += Dz; Ax += Dx; Ay += Dy;
      switch(game.keyDown){
        case "Left":
          Dz = A;
          break;
        case "Right":
          Dz = -A;
          break;
        case "Up":
          Dx = A;
          break;
        case "Down":
          Dx = -A;
          break;
        case "A":
          Dy = A;
          break;
        case "D":
          Dy = -A;
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
