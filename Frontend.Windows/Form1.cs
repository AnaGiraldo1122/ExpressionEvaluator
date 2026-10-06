
using Backend;

namespace Frontend.Windows

{
    public partial class Form1 : Form
    {
        Label pantalla = new Label();
        bool resultadoMostrado = false;
        public Form1()
        {
            InitializeComponent();
            CrearCalculadora();

        }
        private void CrearCalculadora()
        {
            Text = "Funciones Evaluator";
            ClientSize = new Size(400, 310);
            BackColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            pantalla.Location = new Point(10, 10);
            pantalla.BackColor = Color.Green;
            pantalla.ForeColor = Color.White;
            pantalla.Font = new Font("Arial", 18);
            pantalla.TextAlign = ContentAlignment.MiddleLeft;
            pantalla.Size = new Size(380, 50);
            Controls.Add(pantalla);

            Color blanco = Color.White;
            Color naranja = Color.FromArgb(255, 128, 0);

            CrearBoton("7", 0, 0, 1, blanco);
            CrearBoton("8", 1, 0, 1, blanco);
            CrearBoton("9", 2, 0, 1, blanco);
            CrearBoton("(", 3, 0, 1, naranja);
            CrearBoton(")", 4, 0, 1, naranja);
            CrearBoton("Delete", 5, 0, 2, naranja);

            CrearBoton("4", 0, 1, 1, blanco);
            CrearBoton("5", 1, 1, 1, blanco);
            CrearBoton("6", 2, 1, 1, blanco);
            CrearBoton("*", 3, 1, 1, naranja);
            CrearBoton("/", 4, 1, 1, naranja);
            CrearBoton("Clear", 5, 1, 2, naranja);

            CrearBoton("1", 0, 2, 1, blanco);
            CrearBoton("2", 1, 2, 1, blanco);
            CrearBoton("3", 2, 2, 1, blanco);
            CrearBoton("+", 3, 2, 1, naranja);
            CrearBoton("-", 4, 2, 1, naranja);
            CrearBoton("^", 5, 2, 2, naranja);

            CrearBoton("0", 0, 3, 2, blanco);
            CrearBoton(".", 2, 3, 1, blanco);
            CrearBoton("=", 3, 3, 4, naranja);
        }

        private void CrearBoton(string texto, int columna, int fila, int ancho, Color color)
        {
            Button boton = new Button();
            boton.Text = texto;
            boton.Size = new Size(ancho * 55 - 5, 50);
            boton.Location = new Point(10 + columna * 55, 80 + fila * 55);
            boton.BackColor = color;
            boton.FlatStyle = FlatStyle.Flat;
            boton.Font = new Font("Arial", 14);
            boton.Click += Boton_Click;
            Controls.Add(boton);
        }
        private void Boton_Click(object? sender, EventArgs e)
        {
            Button boton = (Button)sender!;
            string tecla = boton.Text;

            if (resultadoMostrado && tecla != "=")
            {
                pantalla.Text = "";
                resultadoMostrado = false;

            }
            if (tecla == "Clear")
            {
                pantalla.Text = "";
            }
            else if (tecla=="Delete")
            {
              if (pantalla.Text.Length > 0)
                {
                    pantalla.Text = pantalla.Text.Substring(0, pantalla.Text.Length - 1);
                }
                
            }
            else if (tecla == "=")
            {
                if (!resultadoMostrado)
                {
                    Calcular();
                }
            }
            else
            {
                pantalla.Text = pantalla.Text + tecla;
            }
        }

        private void Calcular()
        {
            string expresion = pantalla.Text;


            if (expresion== "")
            {
                return;
            }
            try
            {
                double resultado = ExpressionEvaluator.Evalute(expresion);

                if (double.IsInfinity(resultado) || double.IsNaN(resultado))
                {
                    pantalla.Text = "Error";
                }
                else
                {
                    pantalla.Text = expresion + "=" + resultado.ToString().Replace(',', '.');
                }
            }
            catch
            {
                pantalla.Text = "Error";
            }

            resultadoMostrado = true;
        }
    }
}