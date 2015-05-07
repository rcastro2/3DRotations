using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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
      shapes = new Shape[]{HourGlass(),Cube(),Diamond(),Spike(),Polygon(100,5),Mesh(20,10),Gyro(),Sphere()};
    }

    public void update(){
      game.canvas.Clear(Color.White);
      Menu();

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
          shapes[currentShape].K += (shapes[currentShape].name.Contains("Polygon")) ? 0.1 : 1;
          break;
        case "M":
        case "OemMinus":
          if(shapes[currentShape].name.Contains("Polygon"))
            shapes[currentShape].K -= 0.1;
          else
            shapes[currentShape].K -= 1;
          break;
        case "F1":
          shapes[currentShape].displayLabels = false;
          break;
        case "F2":
          shapes[currentShape].displayLabels = true;
          break;
        case "F3":
            shapes[currentShape].displayEdges = false;
            break;
        case "F4":
            shapes[currentShape].displayEdges = true;
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
          if(shapes[currentShape].name.Contains("Polygon")){
            Console.Write("Enter Number of Sides: ");
            int sides = int.Parse(Console.ReadLine());
            shapes[currentShape] = Polygon(100,sides);
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
    public Shape Polygon(int size, int sides){
      int[,] points = new int[sides*2,3];
      int[,] edges = new int[sides*3,2];
      double angle = 2 * Math.PI / sides;
      double x,y;
      for(int index = 0; index < sides;index++){
        x = size * Math.Cos(angle * index);
        y = size * Math.Sin(angle * index);
        points[index,0] = (int)x; points[index,1] = (int)y; points[index,2] = -20;
        edges[index,0] = index; edges[index,1] = (index + 1 == sides )? 0 : index + 1;
      }
      for(int index = sides; index < sides*2;index++){
        points[index,0] = points[index-sides,0]; points[index,1] = points[index-sides,1]; points[index,2] = 20;
        edges[index,0] = index; edges[index,1] = (index + 1 == sides * 2 )? sides : index + 1;
        edges[index + sides,0] = index - sides; edges[index + sides,1] = index;
      }
      return new Shape(game,1,points,edges,sides+"-Polygon");
    }
    public Shape Sphere(){
      int size = 100, segments = 20, rings = 10;
      int[,] points = new int[segments * rings*3,3];
      int[,] edges = new int[segments * rings*3,2];
      double angle = 2 * Math.PI / segments;
      double x,y,z;
      for(int phi = 1; phi <= rings; phi++){
        for(int theta = (phi-1)*segments; theta <  segments * phi; theta++){
          x = size * Math.Sin(phi * angle) * Math.Cos(theta * angle);
          y = size * Math.Sin(phi * angle) * Math.Sin(theta * angle);
          z = size * Math.Cos(phi * angle);

          points[theta,0] = (int)x; points[theta,1] = (int)y; points[theta,2] = (int)z;
          edges[theta,0] = theta; edges[theta,1] = ((theta + 1) % segments == 0)? (phi-1)*segments : theta + 1;
          Console.WriteLine(theta + " " + edges[theta,0] + " - " + edges[theta,1] + " " + (theta + 1 % 20 == 0));
        }
      }
      for(int phi = rings + 1; phi <= rings * 2; phi++){
        for(int theta = (phi-1)*segments; theta <  segments * phi; theta++){
          x = size * Math.Sin(phi * angle) * Math.Cos(theta * angle);
          z = size * Math.Sin(phi * angle) * Math.Sin(theta * angle);
          y = size * Math.Cos(phi * angle);

          points[theta,0] = (int)x; points[theta,1] = (int)y; points[theta,2] = (int)z;
          edges[theta,0] = theta; edges[theta,1] = ((theta + 1) % segments == 0)? (phi-1)*segments : theta + 1;
          Console.WriteLine(theta + " " + edges[theta,0] + " - " + edges[theta,1] + " " + (theta + 1 % 20 == 0));
        }
      }
      for(int phi = 2 * rings + 1; phi <= rings * 3; phi++){
        for(int theta = (phi-1)*segments; theta <  segments * phi; theta++){
          z = size * Math.Sin(phi * angle) * Math.Cos(theta * angle);
          y = size * Math.Sin(phi * angle) * Math.Sin(theta * angle);
          x = size * Math.Cos(phi * angle);

          points[theta,0] = (int)x; points[theta,1] = (int)y; points[theta,2] = (int)z;
          edges[theta,0] = theta; edges[theta,1] = ((theta + 1) % segments == 0)? (phi-1)*segments : theta + 1;
          //Console.WriteLine(theta + " " + edges[theta,0] + " - " + edges[theta,1] + " " + (theta + 1 % 20 == 0));
        }
      }
      //Console.WriteLine(ct);
      return new Shape(game,1,points,edges,"Sphere");
    }
    public Shape Gyro(){
      int size = 80, sides = 36;
      int[,] points = new int[sides*6,3];
      int[,] edges = new int[sides*6,2];
      double angle = 2 * Math.PI / sides;
      double x,y,z;
      for(int index = 0; index < sides;index++){
        x = size * Math.Cos(angle * index);
        y = size * Math.Sin(angle * index);
        z = 0;
        points[index,0] = (int)x; points[index,1] = (int)y; points[index,2] = (int)z;
        edges[index,0] = index; edges[index,1] = (index + 1 == sides )? 0 : index + 1;
      }
      for(int index = sides; index < sides*2;index++){
        x = size * Math.Cos(angle * index);
        z = size * Math.Sin(angle * index);
        y = 0;
        points[index,0] = (int)x; points[index,1] = (int)y; points[index,2] = (int)z;
        edges[index,0] = index; edges[index,1] = (index + 1 == sides*2 )? sides : index + 1;
      }
      for(int index = sides*2; index < sides*3;index++){
        y = size * Math.Cos(angle * index);
        z = size * Math.Sin(angle * index);
        x = 0;
        points[index,0] = (int)x; points[index,1] = (int)y; points[index,2] = (int)z;
        edges[index,0] = index; edges[index,1] = (index + 1 == sides*3 )? sides*2 : index + 1;
      }
      for(int index = sides*3; index < sides*4;index++){
        y = size/2 * Math.Cos(angle * index);
        z = size/2 * Math.Sin(angle * index);
        x = 0;
        points[index,0] = (int)x; points[index,1] = (int)y; points[index,2] = (int)z;
        edges[index,0] = index; edges[index,1] = (index + 1 == sides*4 )? sides*3 : index + 1;
      }
      for(int index = sides*4; index < sides*5;index++){
        x = size/2 * Math.Cos(angle * index);
        z = size/2 * Math.Sin(angle * index);
        y = 0;
        points[index,0] = (int)x; points[index,1] = (int)y; points[index,2] = (int)z;
        edges[index,0] = index; edges[index,1] = (index + 1 == sides*5 )? sides*4 : index + 1;
      }
      for(int index = sides*5; index < sides*6;index++){
        x = size/2 * Math.Cos(angle * index);
        y = size/2 * Math.Sin(angle * index);
        z = 0;
        points[index,0] = (int)x; points[index,1] = (int)y; points[index,2] = (int)z;
        edges[index,0] = index; edges[index,1] = (index + 1 == sides*6 )? sides*5 : index + 1;
      }
      return new Shape(game,1,points,edges,"Gyro");
    }
    public Shape Mesh(int size, int separation){
      int[,] points = new int[4 * size * size,3];
      int[,] edges = new int[8 * size * size,2];
      int ct = 0, ct2 = 4 * size * size;
      for(int row = -size; row < size;row++){
        for(int col = -size; col < size;col++){
          points[ct,0] = row; points[ct,1] = col; points[ct,2] = 0;
          edges[ct,0] = ct; edges[ct,1] = ((ct + 1) % (size * 2) == 0)? ct : ct + 1;
          if((ct + 2 * size) < 4 * size * size) {
            edges[ct2,0] = ct; edges[ct2,1] = ct + 2 * size;
          }
          ct++;ct2++;
        }
      }
      return new Shape(game,separation,points,edges,"Mesh");
    }

    public void Menu(){
      game.canvas.DrawString("Current Shape: " + shapes[currentShape].name,game.form.Font,Brushes.Black,5,5);
      game.canvas.DrawString("Key Down: " + game.keyDown,game.form.Font,Brushes.Black,5,20);
      game.canvas.DrawString("_________________________",game.form.Font,Brushes.Black,5,35);
      game.canvas.DrawString("Rotate Z Axis: Left and Right Arrows" + shapes[currentShape].name,game.form.Font,Brushes.Black,5,50);
      game.canvas.DrawString("Rotate X Axis: Up and Down Arrows" + shapes[currentShape].name,game.form.Font,Brushes.Black,5,65);
      game.canvas.DrawString("Rotate Y Axis: A and D Keys" + shapes[currentShape].name,game.form.Font,Brushes.Black,5,80);
      game.canvas.DrawString("_________________________",game.form.Font,Brushes.Black,5,95);
      game.canvas.DrawString("Scale Up or Down: + and - / P and M",game.form.Font,Brushes.Black,5,110);
      game.canvas.DrawString("Labels On or Off: F1 and F2",game.form.Font,Brushes.Black,5,125);
      game.canvas.DrawString("Edges On or Off: F3 and F4",game.form.Font,Brushes.Black,5,140);
      game.canvas.DrawString("_________________________",game.form.Font,Brushes.Black,5,155);
      for(int pos = 0; pos < shapes.Length; pos++)
        game.canvas.DrawString("Shapes " + (pos + 1) + ": " + shapes[pos].name,game.form.Font,Brushes.Black,5,170 + pos * 15);
      game.canvas.DrawString("_________________________",game.form.Font,Brushes.Black,5,170 + shapes.Length * 15);
      game.canvas.DrawString("Reset Points: Space", game.form.Font,Brushes.Black,5,185 + shapes.Length * 15);


    }
  }
}
