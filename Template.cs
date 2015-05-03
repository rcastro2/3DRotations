using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CSGame{
  class Template{
    // Modify Line 53 of Game.cs to match the class name above
    Setting game;

    Shape[] shapes;
    double Da = Math.PI/90,Az,Ax,Ay;
    double Dz,Dx,Dy;
    int currentShape = 0;
    int A=0,B=1,C=2,D=3,E=4,F=5,G=6,H=7,I=8,J=9;
    public Template(Setting game){
      this.game = game;
      shapes = new Shape[]{HourGlass(),Cube(),Diamond(),Spike()};
    }

    public void update(){
      game.canvas.Clear(Color.White);
      game.canvas.DrawString("Current Shape: " + shapes[currentShape].name,game.form.Font,Brushes.Black,5,5);
      game.canvas.DrawString("Key Down: " + game.keyDown,game.form.Font,Brushes.Black,5,20);

      shapes[currentShape].Draw();
      shapes[currentShape].Rotate(Az,Ax,Ay);

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
          shapes[currentShape].K += 1;
          break;
        case "M":
        case "OemMinus":
          shapes[currentShape].K -= 1;
          break;
        case "F1":
          shapes[currentShape].displayPoints = false;
          break;
        case "F2":
          shapes[currentShape].displayPoints = true;
          break;
        case "Space":
          Az = Ay = Ax = 0;
          shapes[currentShape].Reset();
          break;
        }

        if(game.keyDown.StartsWith("D") && game.keyDown.Length == 2){
          int number = int.Parse(game.keyDown.Substring(1,1));
          if(number >=1 && number <= shapes.Length){
            currentShape = number-1;
          }
        }
        if(game.keyUp == "Left" || game.keyUp == "Right") Dz = 0;
        if(game.keyUp == "Up" || game.keyUp == "Down") Dx = 0;
        if(game.keyUp == "A" || game.keyUp == "D") Dy = 0;

    }

    public Shape HourGlass(){
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
      return new Shape(game,50,points,edges,"Hour Glass");
    }
    public Shape Cube(){
      int[,] points = new int[,]{
        {-1,-1,-1},
        {-1,-1,1},
        {1,-1,1},
        {1,-1,-1},
        {-1,1,-1},
        {-1,1,1},
        {1,1,1},
        {1,1,-1}
      };
      int[,] edges = new int[,] {{C,D},{D,H},{H,G},{G,C},{G,F},{F,B},{B,C},{B,A},{E,A},{A,D},{E,H},{F,E}};
      return new Shape(game,50,points,edges,"Cube");
    }
    public Shape Diamond(){
      int[,] points = new int[,]{
        {0,1,0},
        {-1,0,1},
        {1,0,1},
        {1,0,-1},
        {-1,0,-1},
        {0,-1,0}
      };
      int[,] edges = new int[,] {{B,C},{C,D},{D,E},{E,B},{A,B},{A,C},{A,D},{A,E},{F,B},{F,C},{F,D},{F,E}};
      return new Shape(game,50,points,edges,"Diamond");
    }
    public Shape Spike(){
      int[,] points = new int[,]{
        {0,0,0},
        {-1,0,0},
        {1,0,0},
        {0,-1,0},
        {0,1,0},
        {0,0,-1},
        {0,0,1}
      };
      int[,] edges = new int[,] {{A,B},{A,C},{A,D},{A,E},{A,F},{A,G}};
      return new Shape(game,50,points,edges,"Spike");
    }
  }
}
