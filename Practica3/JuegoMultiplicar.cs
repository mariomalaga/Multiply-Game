/*
* PRÁCTICA.............: Práctica 3.
* NOMBRE y APELLIDOS...: Mario Olivera Castañeda
* CURSO y GRUPO........: 2o Desarrollo de Interfaces
* TÍTULO de la PRÁCTICA: Sentencias de Control. Excepciones.
* FECHA de ENTREGA.....: 05 de Noviembre de 2018
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica3
{
    class JuegoMultiplicar
    {
        #region Variables
        private static int numero1;
        private static int numero2;
        private static int contadorAciertos;
        private static int contadorFallos;
        private static int tiempoMaximo;
        private static int resultado;
        #endregion
        #region Constructor
        public JuegoMultiplicar()
        {
            contadorAciertos = 0;
            contadorFallos = 0;
        }
        #endregion
        #region Metodos
        #region Comprobar
        public bool Comprobar(int resultadoUsuario)
        {
            bool comprobar = false;
            if (resultado == resultadoUsuario)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Respuesta Correcta");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Pulsar 'Enter' para continuar");
                Console.ReadLine();
                comprobar = !comprobar;
                contadorAciertos++;
            }
            else if (resultadoUsuario == 0)
            {
                contadorFallos++;
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Respuesta Incorrecta. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("El resultado es " +resultado);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Pulsar 'Enter' para continuar");
                Console.ReadLine();
                contadorFallos++;
            }
            return comprobar;
        }
        #endregion
        #region GenerandoOperandos
        public void GenerandoOperandos()
        {
            Random random = new Random();
            numero1 = random.Next(1, 9);
            numero2 = random.Next(1, 9);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(numero1+" * "+numero2+ " = ");
            resultado = numero1 * numero2;
        }
        #endregion
        #endregion
        #region Propiedades
        public int Numero1 { get => numero1;}
        public int Numero2 { get => numero2;}
        public int ContadorAciertos { get => contadorAciertos;}
        public int ContadorFallos { get => contadorFallos;}
        public int Resultado { get => resultado;}
        public int TiempoMaximo { get => tiempoMaximo; set => tiempoMaximo = value; }
        #endregion
    }
}
