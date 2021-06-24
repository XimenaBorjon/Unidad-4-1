using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SQLite;

namespace PresionArter.Model
{
    public class Presiones
    {
        public SQLiteConnection Conexion { get; set; }

        string rutadb = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/PresionA.db3";

        public Presiones()
        {
            if (!File.Exists(rutadb))
            {
                Assembly a = Assembly.GetExecutingAssembly();
                var stream = a.GetManifestResourceStream("PresionArter.Data.PresionA.db3");
                FileStream file = File.Create(rutadb);
                stream.CopyTo(file);
                stream.Close();
                file.Close();
            }
            Conexion = new SQLiteConnection(rutadb);
        }

        public void Insert(Presion p)
        {
            if (p.Pulso.ToString() == "0")
            {
                throw new ArgumentException("El pulso esta vacio");
            }
            if (p.Diastolica.ToString() == "0")
            {
                throw new ArgumentException("El Diastolica esta vacio");
            }
            if (p.Sistolica.ToString() == "0")
            {
                throw new ArgumentException("La Sistolica esta vacia esta vacia");
            }

            Conexion.Insert(p);
        }

        public IEnumerable<Presion> GetAll()
        {
            return Conexion.Table<Presion>();
        }

        public Presion Get(int id)
        {
            return Conexion.Get<Presion>(id);
        }

        public void Update(Presion p)
        {
            if (p.Pulso.ToString() == "0")
            {
                throw new ArgumentException("El pulso esta vacio");
            }
            if (p.Diastolica.ToString() == "0")
            {
                throw new ArgumentException("El Diastolica esta vacio");
            }
            if (p.Sistolica.ToString() == "0")
            {
                throw new ArgumentException("La Sistolica esta vacia esta vacia");
            }

            var citabd = Get(p.Id);
            citabd.Pulso = p.Pulso;
            citabd.Diastolica = p.Diastolica;
            citabd.Sistolica = p.Sistolica;
            citabd.Fecha = p.Fecha;

            Conexion.Update(p);

        }

        public void Delete(Presion p)
        {
            Conexion.Delete(p);
        }
    }
}
