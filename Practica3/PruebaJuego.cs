/*
* PRÁCTICA.............: Práctica 3.
* NOMBRE y APELLIDOS...: Mario Olivera Castañeda
* CURSO y GRUPO........: 2o Desarrollo de Interfaces
* TÍTULO de la PRÁCTICA: Sentencias de Control. Excepciones.
* FECHA de ENTREGA.....: 05 de Noviembre de 2018
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace Practica3
{
    class PruebaJuego
    {
        #region Variables
        private static Timer timer;
        private static int time;
        private static JuegoMultiplicar juegoMultiplicar;
        #endregion
        #region Main
        static void Main(string[] args)
        {
            Menu();
        }
        #endregion
        #region Metodos
        #region Menu
        static void Menu()
        {
            string casos = "";
            int tiempoLimite = 0;
            int numeroPreguntas = 0;
            while (casos != "4")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Juego de multiplicar");
                Console.WriteLine("1. Establecer tiempo máximo para las respuestas");
                Console.WriteLine("2. Establecer el número de preguntas");
                Console.WriteLine("3. JUGAR");
                Console.WriteLine("4. Salir");
                casos = Console.ReadLine();
                switch (casos)
                {
                    case "1":
                        tiempoLimite = LeerTiempoLimite();
                        break;
                    case "2":
                        numeroPreguntas = LeerNumeroPreguntas();
                        break;
                    case "3":
                        if (tiempoLimite > 0 && numeroPreguntas > 0)
                        {
                            Jugar(tiempoLimite, numeroPreguntas);
                            tiempoLimite = 0;
                            numeroPreguntas = 0;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Debe realizar antes el punto 1 y 2\n");
                        }
                        break;
                    case "4":
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Valor introducido erróneo");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Por favor introduzca un valor correcto \n");
                        break;
                }
            }
        }
        #endregion
        #region LeerTiempoLimite
        static int LeerTiempoLimite()
        {
            bool leerTiempo = false;
            int limiteTiempo = ControlarEntradaInt("Introducir límite de tiempo (Entre 3 y 10)");
            while (!leerTiempo)
            {
                Console.Clear();
                if (limiteTiempo < 3 || limiteTiempo > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error al introducir el tiempo\n");
                    limiteTiempo = ControlarEntradaInt("Introducir límite de tiempo (Entre 3 y 10) \n");
                }
                else
                {
                    leerTiempo = !leerTiempo;
                }
            }
            return limiteTiempo;
        }
        #endregion
        #region LeerNumeroPreguntas
        static int LeerNumeroPreguntas()
        {
            juegoMultiplicar = new JuegoMultiplicar();
            bool leerPreguntas = false;
            int numeroPreguntas = ControlarEntradaInt("Introducir el número de preguntas (Entre 1 y 10)");
            while (!leerPreguntas)
            {
                Console.Clear();
                if (numeroPreguntas < 1 || numeroPreguntas > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error al introducir el número de preguntas\n");
                    numeroPreguntas = ControlarEntradaInt("Introducir el número de preguntas (Entre 1 y 10) \n");
                }
                else
                {
                    leerPreguntas = !leerPreguntas;
                }
            }
            return numeroPreguntas;
        }
        #endregion
        #region Jugar
        static void Jugar(int tiempo, int preguntas)
        {
            juegoMultiplicar.TiempoMaximo = tiempo;
            for (int i = 0; i < preguntas; i++)
            {
                juegoMultiplicar.GenerandoOperandos();
                time = tiempo;
                int respuestaUsuario = ResponderIntervalo();
                juegoMultiplicar.Comprobar(respuestaUsuario);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("El número de acierto son: " + juegoMultiplicar.ContadorAciertos);
            Console.WriteLine("El número de fallos son: " + juegoMultiplicar.ContadorFallos);
            float nota = juegoMultiplicar.ContadorAciertos * 100 / preguntas;
            Console.WriteLine("Nota: "+ nota +"/100 \n");
        }
        #endregion
        #region ResponderIntervalo
        static int ResponderIntervalo()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(TiempoTranscurrido);
            timer.Enabled = true;
            Console.ForegroundColor = ConsoleColor.White;
            string respuesta = Console.ReadLine();
            if (time == 0)
            {
                respuesta = "0";
            }
            else if (respuesta.Equals(""))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nRespuesta Incorrecta.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("El resultado es " + juegoMultiplicar.Resultado);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Pulsar 'Enter' para continuar");
                respuesta = "0";
                timer.Close();
                timer.Dispose();
            }

            else
            {
                timer.Close();
                timer.Dispose();
            }
            if (!int.TryParse(respuesta, out int numero))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Respuesta Incorrecta.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("El resultado es " + juegoMultiplicar.Resultado);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Pulsar 'Enter' para continuar");
                respuesta = "0";
            }
            return int.Parse(respuesta);
        }
        #endregion
        #region TiempoTranscurrido
        private static void TiempoTranscurrido(object sender, ElapsedEventArgs e)
        {
            time--;
            if (time == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nSe acabó el tiempo");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("El resultado es " + juegoMultiplicar.Resultado);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Pulsar 'Enter' para continuar");
                timer.Close();
                timer.Dispose();
            }
        }
        #endregion
        #region ControlarEntradaInt
        static int ControlarEntradaInt(string texto)
        {
            int valor;
            bool esNumero;
            string variable;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(texto + "\n");
                variable = Console.ReadLine();
                esNumero = int.TryParse(variable, out valor);
                if (!esNumero)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No es un número entero \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Por favor vuelva a introducir");
                }
            }
            while (!esNumero);
            return int.Parse(variable);
        }
        #endregion
        #endregion
    }
}
