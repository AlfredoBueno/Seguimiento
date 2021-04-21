using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SeguimientoAlumnosEgresados2.Models;

namespace SeguimientoAlumnosEgresados2.Controllers
{
    public class exalumnoesController : Controller
    {
        private seguimiento2Entities db = new seguimiento2Entities();

        // GET: exalumnoes
        public ActionResult Index()
        {
            var exalumnoes = db.exalumnoes.Include(e => e.carrera);
            return View(exalumnoes.ToList());
        }

        // GET: exalumnoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exalumno exalumno = db.exalumnoes.Find(id);
            if (exalumno == null)
            {
                return HttpNotFound();
            }
            return View(exalumno);
        }

        // GET: exalumnoes/Create
        public ActionResult Create()
        {
            ViewBag.idCarrera = new SelectList(db.carreras, "idCarrera", "nombreCarrera");
            return View();
        }

        // POST: exalumnoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "noControl,nombre,segundoNombre,apellido,segundoApellido,correo,clave,idCarrera,telefono,fechaNacimiento,fechaEgreso,rutaImg")] exalumno exalumno)
        {
            if (ModelState.IsValid)
            {
                db.exalumnoes.Add(exalumno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCarrera = new SelectList(db.carreras, "idCarrera", "nombreCarrera", exalumno.idCarrera);
            return View(exalumno);
        }

        // GET: exalumnoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exalumno exalumno = db.exalumnoes.Find(id);
            if (exalumno == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCarrera = new SelectList(db.carreras, "idCarrera", "nombreCarrera", exalumno.idCarrera);
            return View(exalumno);
        }

        // POST: exalumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "noControl,nombre,segundoNombre,apellido,segundoApellido,correo,clave,idCarrera,telefono,fechaNacimiento,fechaEgreso,rutaImg")] exalumno exalumno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exalumno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCarrera = new SelectList(db.carreras, "idCarrera", "nombreCarrera", exalumno.idCarrera);
            return View(exalumno);
        }

        // GET: exalumnoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exalumno exalumno = db.exalumnoes.Find(id);
            if (exalumno == null)
            {
                return HttpNotFound();
            }
            return View(exalumno);
        }

        // POST: exalumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            exalumno exalumno = db.exalumnoes.Find(id);
            db.exalumnoes.Remove(exalumno);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Historial academico empieza aqui
        string controln;

        public ActionResult IndexH(string id)
        {
            var historialAcademico = db.historialacademicoes.Include(e => e.exalumno);
            //historialAcademico.Where(e => e.noControl == noControl);
            if (id != null)
            {
                controln = id;
            }
            return View(historialAcademico.Where(e => e.noControl == controln).ToList());
        }


        public ActionResult DetailsH(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historialacademico historial = db.historialacademicoes.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            return View(historial);
        }

        public ActionResult CreateH()
        {
            ViewBag.noControl = new SelectList(db.exalumnoes, "noControl", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateH([Bind(Include = "idhistorialacademico,noControl,fechah,tituloh,descripcionh,rutaImg")] historialacademico historial)
        {
            if (ModelState.IsValid)
            {
                db.historialacademicoes.Add(historial);
                db.SaveChanges();
                return RedirectToAction("IndexH");
            }

            ViewBag.NoControl = new SelectList(db.exalumnoes, "noControl", "nombre", historial.noControl);
            return View("IndexH");
        }

        // GET: eventoacademicoes/Edit/5
        public ActionResult EditH(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historialacademico historial = db.historialacademicoes.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            ViewBag.NoControl = new SelectList(db.exalumnoes, "noControl", "nombre", historial.noControl);
            return View(historial);
        }

        // POST: eventoacademicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idhistorialacademico,noControl,fechah,tituloh,descripcionh,rutaImg")] historialacademico historial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexH");
            }
            ViewBag.NoControl = new SelectList(db.exalumnoes, "noControl", "nombre", historial.noControl);
            return View(historial);
        }

        // GET: eventoacademicoes/Delete/5
        public ActionResult DeleteH(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historialacademico historial = db.historialacademicoes.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            return View(historial);
        }

        // POST: eventoacademicoes/Delete/5
        [HttpPost, ActionName("DeleteH")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedH(int id)
        {
            historialacademico historial = db.historialacademicoes.Find(id);
            db.historialacademicoes.Remove(historial);
            db.SaveChanges();
            return RedirectToAction("IndexH");
        }

    }
}
