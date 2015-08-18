using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryBuilder
{
    /// <summary>
    /// Clase auxiliar para la construcción de querys
    /// </summary>
    public class QueryBuilder
    {
        private StringBuilder Select;
        private StringBuilder Where;
        private List<StringBuilder> Joins;
        private StringBuilder From;

        /// <summary>
        /// Inicializa un query con una lista de campos y una tabla de donde se obtienen
        /// </summary>
        /// <param name="campos">lista de campos</param>
        /// <param name="tabla">tabla de donde se obtienen los campos</param>
        public QueryBuilder(string campos, string tabla) : this()
        {
            Select.Append(campos);
            From.Append(tabla);
        }

        /// <summary>
        /// Inicializa un query vacio
        /// </summary>
        public QueryBuilder()
        {
            Select = new StringBuilder();
            Where = new StringBuilder();
            Joins = new List<StringBuilder>();
            From = new StringBuilder();
        }

        /// <summary>
        /// Agrega un campo a la lista de campos del query7
        /// </summary>
        /// <param name="campo">campo a agregar</param>
        public void agregaCampo(string campo)
        {
            if (Select.Length > 0) Select.Append(",");
            Select.Append(campo);
        }

        /// <summary>
        /// Agrega un join al query
        /// </summary>
        /// <param name="tabla">Tabla a la que se le hace join</param>
        /// <param name="condición"> Condición para realizar el join</param>
        /// <param name="tipo">Tipo de join a realizar</param>
        public void agregaJoin(string tabla, string condición, TipoJoin tipo)
        {
            StringBuilder Join = new StringBuilder();
            Join.Append(tipo).Append(" JOIN ").Append(tabla).Append(" ON ").Append(condición);
            Joins.Add(Join);
        }

        /// <summary>
        /// Establece la condición del WHERE
        /// </summary>
        /// <param name="condición"></param>
        public void setWhere(string condición)
        {
            Where.Clear().Append(condición);
        }

        /// <summary>
        /// Transforma el query construido a string
        /// </summary>
        /// <returns>Retorna el query en string</returns>
        public override string ToString()
        {
            StringBuilder Query = new StringBuilder("SELECT ");
            Query.Append(Select);
            Query.Append(" FROM ").Append(From);
            foreach (StringBuilder join in Joins)
            {
                Query.Append(" ").Append(join);
            }
            if(Where.Length != 0)
            {
                Query.Append(" WHERE ").Append(Where);
            }
            return Query.ToString();
        }
    }

    /// <summary>
    /// Enumeración para el tipo de join
    /// </summary>
    public sealed class TipoJoin
    {
        private readonly string nombre;
        private readonly int valor;

        public static readonly TipoJoin INNER = new TipoJoin(1, "INNER");
        public static readonly TipoJoin LEFT = new TipoJoin(2, "LEFT");
        public static readonly TipoJoin RIGHT = new TipoJoin(3, "RIGHT");
        public static readonly TipoJoin NONE = new TipoJoin(4, string.Empty);

        private TipoJoin(int valor, string nombre)
        {
            this.valor = valor;
            this.nombre = nombre;
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}
    