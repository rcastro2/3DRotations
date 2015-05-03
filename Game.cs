using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CSGame{
	public class Setting{
		public Graphics canvas;

		public Form form;
		public int interval = 10;
		public int width = 800;
		public int height = 600;
		public bool pause = false;

		//Formerly part of the Input Class
		public string keyDown = "";
		public string keyUp = "";
		public int mouseX;
		public int mouseY;
		public bool leftButton;
		public bool rightButton;
	}
	public struct Point3D{
		public double X;
		public double Y;
		public double Z;
		public Point3D(int X, int Y, int Z){
			this.X = X; this.Y = Y; this.Z = Z;
		}
	}
	public class Game : System.Windows.Forms.Form{

		//bm is "Canvas" in memory where we can draw
		Bitmap bm = new Bitmap(2000, 2000);
		//bmg is the Graphics we will use in order to draw on the "Canvas"
		Graphics canvas;

		//Support objects for game
		Setting game;

		//Object for game logic
		dynamic main;

		//Main starts the program which creates and runs the Form
		static void Main(){
			Application.Run(new Game());
		}

		//Game Constructor
		public Game(){
			InitializeComponent();
		}

		//Any initial processing goes here such as loading arrays or generating initial random values
		private void Game_Load(object sender, EventArgs e){
			//Following line connects canvas to bm so that we can draw on it
			canvas = Graphics.FromImage(bm);
			game = new Setting();
			game.width = this.Width;
			game.height = this.Height;
			game.canvas = canvas;
			game.form = this;

			//main must be set to an instance of the Class holding the game logic
			main = new Template(game);

			//Enable the timer for create animation
		  tmrGame.Enabled = true;
		}

		//Paints to the Form1
		private void Game_Paint(object sender,PaintEventArgs e){
			//Take the image stored on the Canvas and draw it to the form once
			e.Graphics.DrawImage(bm, 0, 0);
		}

		//Track Key Pressed in Input Class
		private void Game_KeyDown(object sender,KeyEventArgs e){
			game.keyUp = "";
			game.keyDown = e.KeyCode.ToString();
		}

		private void Game_KeyUp(object sender,KeyEventArgs e){
			game.keyDown = "";
			game.keyUp = e.KeyCode.ToString();
		}

		//Track Mouse Position in Input Class
		private void Game_MouseMove(object sender, MouseEventArgs e)
		{
			game.mouseX = e.X;
			game.mouseY = e.Y;
		}

		//Track Mouse Buttons in Input Class
		private void Game_MouseDown(object sender, MouseEventArgs e)
		{
			game.leftButton = e.Button == MouseButtons.Left;
			game.rightButton = e.Button == MouseButtons.Right;
		}

		private void Game_MouseUp(object sender, MouseEventArgs e)
		{
			game.leftButton = false;
			game.rightButton = false;
		}

		//Timer controls the game
		private void tmrGame_Tick(object sender, EventArgs e){
			//Call update method for the Template Class
			if(!game.pause)
				main.update();

			//Modify Game variables from Settings
			tmrGame.Interval = game.interval;
			if(this.Width != game.width || this.Height != game.height){
				this.Width = game.width;
				this.Height = game.height;
			}

			//Force the form to repaint
			this.Invalidate();
		}


		//Used to initialize the Form and Add a Timer called tmrGame
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Timer tmrGame;

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
			this.tmrGame = new System.Windows.Forms.Timer(this.components);

			this.tmrGame.Interval = 10;
			this.tmrGame.Tick += new EventHandler(this.tmrGame_Tick);

			this.Width = 800;
			this.Height = 600;
			this.DoubleBuffered = true;
			this.StartPosition = FormStartPosition.CenterScreen;

			//Optional Settings for ScreenSaver
			this.BackColor = Color.White;
			//this.TransparencyKey = Color.White;
			//this.Opacity = 0.50;
			//this.FormBorderStyle = FormBorderStyle.None;
			this.MaximizeBox = false;
			//this.WindowState = FormWindowState.Maximized;

			this.Load += new EventHandler(this.Game_Load);
			this.KeyDown += new KeyEventHandler(this.Game_KeyDown);
			this.KeyUp += new KeyEventHandler(this.Game_KeyUp);
			this.MouseMove += new MouseEventHandler(this.Game_MouseMove);
			this.MouseDown += new MouseEventHandler(this.Game_MouseDown);
			this.MouseUp += new MouseEventHandler(this.Game_MouseUp);
			this.Paint += new PaintEventHandler(this.Game_Paint);

		}
	}
}
